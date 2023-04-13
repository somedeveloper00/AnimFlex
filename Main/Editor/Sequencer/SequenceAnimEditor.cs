using System;
using System.Linq;
using AnimFlex.Core.Proxy;
using AnimFlex.Sequencer;
using AnimFlex.Sequencer.UserEnd;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer {
    [CustomEditor( typeof(SequenceAnim) )]
    public class SequenceAnimEditor : UnityEditor.Editor {
        private SequenceAnim _sequenceAnim;
        private Sequence _sequence;

        private SerializedProperty _sequenceProp;
        private SerializedProperty _playOnStartProp;
        private SerializedProperty _resetOnPlayProp;
        private SerializedProperty _clipNodesProp;
        private SerializedProperty _useProxyAsCoreProp;
        private SerializedProperty _coreProxyProp;

        private ReorderableList _nodeClipList;

        private Vector2 _lastMousePos = Vector2.zero;

        bool _showAdvanced = false;
        
        GUIContent _addCoreProxyGuiContent = new GUIContent( "Add", "Add a new Core Proxy Component to this Game Object" );

        private void OnEnable() {
            _sequenceAnim = target as SequenceAnim;
            _sequence = _sequenceAnim.sequence;
            GetProperties();

            SetupNodeListDrawer();
        }


        public override void OnInspectorGUI() {
            serializedObject.Update();

            if (( _showAdvanced = EditorGUILayout.Foldout( _showAdvanced, "Advanced Options", true ) )) {
                drawAdvancedOptions();
            }

            using (new EditorGUI.DisabledScope( Application.isPlaying )) {
                GUILayout.Space( 10 );
                DrawPlayback();
                if (!AFPreviewUtils.isActive) {
                    DrawClipNodes();
                    DrawAddButton();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        void drawAdvancedOptions() {
            using (new GUILayout.HorizontalScope()) {
                using (new AFStyles.EditorLabelWidth( 110 ))
                    EditorGUILayout.PropertyField( _playOnStartProp, GUILayout.Width( 130 ) );
                using (new AFStyles.EditorLabelWidth( 90 ))
                    EditorGUILayout.PropertyField( _resetOnPlayProp );
            }

            using (new GUILayout.HorizontalScope()) {
                using (new AFStyles.EditorLabelWidth( 110 ))
                    EditorGUILayout.PropertyField( _useProxyAsCoreProp, GUILayout.Width( 130 ) );
                if (_useProxyAsCoreProp.boolValue) {
                    using (new AFStyles.EditorLabelWidth( 80 )) {
                        EditorGUILayout.PropertyField( _coreProxyProp );
                    }

                    if (_coreProxyProp.objectReferenceValue == null) {
                        if (GUILayout.Button( _addCoreProxyGuiContent, GUILayout.Width( 60 ) )) {
                            // add core proxy component
                            AFEditorUtils.GetTypeInstanceFromHierarchy<AnimflexCoreProxyBase>( type => {
                                serializedObject.ApplyModifiedProperties();
                                if (_sequenceAnim.TryGetComponent( type, out var comp )) {
                                    _sequenceAnim.coreProxy = (AnimflexCoreProxyBase)comp;
                                }
                                else {
                                    _sequenceAnim.coreProxy = (AnimflexCoreProxyBase)_sequenceAnim.gameObject.AddComponent( type );
                                }
                                serializedObject.Update();
                            } );
                        }
                    }
                }
            }
        }

        private void DrawPlayback() {
            using (new GUILayout.HorizontalScope()) {
                GUILayout.FlexibleSpace();

                using (new AFStyles.GuiForceActive()) {
                    using (new EditorGUI.DisabledScope( Application.isPlaying )) {
                        var text = AFPreviewUtils.isActive ? "Stop Preview" : "Preview Sequence";
                        if (GUILayout.Button( text, AFStyles.BigButton,
                                GUILayout.Height( AFStyles.BigHeight ), GUILayout.Width( 200 ) )) {
                            if (AFPreviewUtils.isActive)
                                AFPreviewUtils.StopPreviewMode();
                            else
                                AFPreviewUtils.PreviewSequence( _sequence );
                        }
                    }
                }

                GUILayout.FlexibleSpace();
            }
        }

        private void DrawClipNodes() {
            using var _ = new AFStyles.GuiColor( AFStyles.BoxColor );
            using (new AFStyles.GuiBackgroundColor( AFEditorSettings.Instance.backgroundBoxCol ))
                _nodeClipList.DoLayoutList();
        }

        private void SetupNodeListDrawer() {
            _nodeClipList = new ReorderableList( serializedObject, elements: _clipNodesProp, draggable: true,
                displayHeader: false, displayAddButton: false, displayRemoveButton: false );
            _nodeClipList.drawElementCallback = (rect, index, active, focused) => {
                using (new AFStyles.StyledGuiScope( this )) {
                    var nodeProp = _clipNodesProp.GetArrayElementAtIndex( index );
                    EditorGUI.PropertyField( rect, nodeProp, GUIContent.none, true );

                    // draw X button
                    using (new AFStyles.GuiBackgroundColor( Color.clear )) {
                        if (GUI.Button(
                                new Rect( rect.x + rect.width - 20 + 5, rect.y, 30,
                                    AFStyles.BigHeight + AFStyles.VerticalSpace * 2 )
                                , new GUIContent( "X", "Remove clip" ), AFStyles.ClearButton )) {
                            EditorApplication.delayCall += () => {
                                serializedObject.Update();
                                _clipNodesProp.DeleteArrayElementAtIndex( index );
                                serializedObject.ApplyModifiedProperties();
                                Repaint();
                            };
                        }
                    }
                }
            };
            _nodeClipList.elementHeightCallback = index => {
                var nodeProp = _clipNodesProp.GetArrayElementAtIndex( index );
                return EditorGUI.GetPropertyHeight( nodeProp, true );
            };
            _nodeClipList.footerHeight = 0;
            _nodeClipList.headerHeight = 0;
        }

        private void DrawAddButton() {
            using (new GUILayout.HorizontalScope( GUILayout.ExpandWidth( true ) )) {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(
                        new GUIContent( "+ Add Clip", "Adds a new clip node to the list of nodes" ),
                        AFStyles.BigButton, GUILayout.Width( 150 ) )) {
                    AFEditorUtils.CreateTypeInstanceFromHierarchy<Clip>( clip => {
                        RecordUndo();
                        _sequence.AddNewClipNode( clip );
                        serializedObject.Update();
                    } );
                }

                GUILayout.FlexibleSpace();
            }
        }

        private void RecordUndo() {
            Undo.RecordObject( _sequenceAnim, "Sequence component modified" );
        }

        private void GetProperties() {
            _sequenceProp = serializedObject.FindProperty( nameof(SequenceAnim.sequence) );
            _playOnStartProp = serializedObject.FindProperty( nameof(_sequenceAnim.playOnEnable) );
            _resetOnPlayProp = serializedObject.FindProperty( nameof(_sequenceAnim.resetOnPlay) );
            _useProxyAsCoreProp = serializedObject.FindProperty( nameof(SequenceAnim.useProxyAsCore) );
            _coreProxyProp = serializedObject.FindProperty( nameof(SequenceAnim.coreProxy) );
            
            _clipNodesProp = _sequenceProp.FindPropertyRelative( nameof(Sequence.nodes) );
        }
    }
}
