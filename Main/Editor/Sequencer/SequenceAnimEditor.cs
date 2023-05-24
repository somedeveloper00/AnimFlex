using System.Linq;
using AnimFlex.Core.Proxy;
using AnimFlex.Editor.Preview;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor {
    [CustomEditor( typeof(SequenceAnim) )]
    public class SequenceAnimEditor : UnityEditor.Editor {
        SequenceAnim _sequenceAnim;
        Sequence _sequence;

        SerializedProperty _sequenceProp;
        SerializedProperty _playOnStartProp;
        SerializedProperty _dontWaitInQueueToPlayProp;
        SerializedProperty _resetOnPlayProp;
        SerializedProperty _clipNodesProp;
        SerializedProperty _useProxyAsCoreProp;
        SerializedProperty _coreProxyProp;
        SerializedProperty _useDefaultCoreProxyProp;
        SerializedProperty _defaultCoreProxyProp;
        SerializedProperty _activateNextClipAsapProp;

        ReorderableList _nodeClipList;

        GUIContent[] _coreProxyTypeOptions = null;

        bool _showAdvanced = false;
        
        static readonly GUIContent _addCoreProxyGuiContent = new GUIContent( "Add", "Add a new Core Proxy Component to this Game Object" );
        static readonly GUIContent _addClipButtonGuiContent = new GUIContent( "+ Add Clip", "Adds a new clip node to the list of nodes" );
        static readonly GUIContent _xButtonGuiContent = new GUIContent( "X", "Remove clip" );


        void OnEnable() {
            _sequenceAnim = target as SequenceAnim;
            _sequence = _sequenceAnim.sequence;
            GetProperties();

            _coreProxyTypeOptions = AnimFlexCoreProxyHelper.AllCoreProxyTypeNiceNames
                .Select( n => new GUIContent( n, _defaultCoreProxyProp.tooltip ) ).ToArray();

            SetupNodeListDrawer();
        }
        
        public override void OnInspectorGUI() {
            serializedObject.Update();

            _showAdvanced = EditorGUILayout.Foldout( _showAdvanced, "Advanced Options", true );
            if ( _showAdvanced ) drawAdvancedOptions();

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
                using (new AFStyles.EditorLabelWidth( 90 ))
                    EditorGUILayout.PropertyField( _playOnStartProp, GUILayout.Width( 110 ) );
                using (new AFStyles.EditorLabelWidth( 155 ))
                    EditorGUILayout.PropertyField( _dontWaitInQueueToPlayProp );
            }
            using (new GUILayout.HorizontalScope()) {
                using (new AFStyles.EditorLabelWidth( 90 ))
                    EditorGUILayout.PropertyField( _resetOnPlayProp, GUILayout.Width( 110 ) );
                using (new AFStyles.EditorLabelWidth( 155 ))
                    EditorGUILayout.PropertyField( _activateNextClipAsapProp );
            }

            using (new GUILayout.HorizontalScope()) {
                using (new AFStyles.EditorLabelWidth( 110 ))
                    EditorGUILayout.PropertyField( _useProxyAsCoreProp, GUILayout.Width( 130 ) );

                if (_useProxyAsCoreProp.boolValue) {
                    using (new AFStyles.EditorLabelWidth( 140 ))
                        EditorGUILayout.PropertyField( _useDefaultCoreProxyProp, GUILayout.Width( 160 ) );
                    
                    if (_useDefaultCoreProxyProp.boolValue) {
                        var result = EditorGUILayout.Popup( AnimFlexCoreProxyHelper.AllCoreProxyTypeNiceNames.IndexOf(
                            _defaultCoreProxyProp.stringValue ), _coreProxyTypeOptions );
                        if (result != -1) {
                            _defaultCoreProxyProp.stringValue = AnimFlexCoreProxyHelper.AllCoreProxyTypeNames[result];
                        }
                    }
                }
            }

            using (new GUILayout.HorizontalScope()) {
                if (_useProxyAsCoreProp.boolValue && !_useDefaultCoreProxyProp.boolValue) {
                    using (new AFStyles.EditorLabelWidth( 80 )) {
                        EditorGUILayout.PropertyField( _coreProxyProp );
                    }

                    if (_coreProxyProp.objectReferenceValue == null) {
                        if (GUILayout.Button( _addCoreProxyGuiContent, GUILayout.Width( 60 ) )) {
                            // add core proxy component
                            AFEditorUtils.GetTypeInstanceFromHierarchy<AnimflexCoreProxy>( type => {
                                serializedObject.ApplyModifiedProperties();
                                if (_sequenceAnim.TryGetComponent( type, out var comp )) {
                                    _sequenceAnim.coreProxy = (AnimflexCoreProxy)comp;
                                }
                                else {
                                    _sequenceAnim.coreProxy =
                                        (AnimflexCoreProxy)_sequenceAnim.gameObject.AddComponent( type );
                                }

                                serializedObject.Update();
                            } );
                        }
                    }
                }
            }
        }

        void DrawPlayback() {
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
                                AFPreviewUtils.PreviewSequence( _sequenceAnim );
                        }
                    }
                }

                GUILayout.FlexibleSpace();
            }
        }

        void DrawClipNodes() {
            using var scope = new EditorGUILayout.VerticalScope();
            
            // outline
            var rect = new Rect(scope.rect);
            rect.x -= AFStyles.VerticalSpace / 2;
            rect.y -= AFStyles.VerticalSpace / 2;
            rect.width += AFStyles.VerticalSpace;
            rect.height += AFStyles.VerticalSpace;
                
            EditorGUI.DrawRect( rect, AFEditorSettings.Instance.BoxColOutline );
            using (new AFStyles.GuiBackgroundColor( AFEditorSettings.Instance.backgroundBoxCol ))
                _nodeClipList.DoLayoutList();
        }

        void SetupNodeListDrawer() {
            _nodeClipList = new ReorderableList( serializedObject, elements: _clipNodesProp, draggable: true,
                displayHeader: false, displayAddButton: false, displayRemoveButton: false );
            _nodeClipList.drawElementCallback = (rect, index, _, _) => {
                    rect.width -= 20;
                    if (Event.current.type != EventType.Used)
                        EditorGUI.PropertyField( rect, _clipNodesProp.GetArrayElementAtIndex( index ), GUIContent.none, true );

                    // draw X button
                    rect.x += rect.width; rect.width = 20;
                    rect.height = AFStyles.Height;
                    if (GUI.Button( rect, _xButtonGuiContent, AFStyles.ClearButton )) {
                        _clipNodesProp.DeleteArrayElementAtIndex( index );
                        serializedObject.ApplyModifiedProperties();
                    }
            };
            _nodeClipList.elementHeightCallback = index => {
                var nodeProp = _clipNodesProp.GetArrayElementAtIndex( index );
                return EditorGUI.GetPropertyHeight( nodeProp, true );
            };
            _nodeClipList.footerHeight = 0;
            _nodeClipList.headerHeight = 0;
        }

        void DrawAddButton() {
            using (new GUILayout.HorizontalScope( GUILayout.ExpandWidth( true ) )) {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(
                        _addClipButtonGuiContent,
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

        void RecordUndo() {
            Undo.RecordObject( _sequenceAnim, "Sequence component modified" );
        }

        void GetProperties() {
            _sequenceProp = serializedObject.FindProperty( nameof(SequenceAnim.sequence) );
            _playOnStartProp = serializedObject.FindProperty( nameof(SequenceAnim.playOnStart) );
            _dontWaitInQueueToPlayProp = serializedObject.FindProperty( nameof(SequenceAnim.dontWaitInQueueToPlay) );
            _defaultCoreProxyProp = serializedObject.FindProperty( nameof(SequenceAnim.defaultCoreProxy) );
            _useDefaultCoreProxyProp = serializedObject.FindProperty( nameof(SequenceAnim.useDefaultCoreProxy) );
            _useProxyAsCoreProp = serializedObject.FindProperty( nameof(SequenceAnim.useProxyAsCore) );
            _resetOnPlayProp = serializedObject.FindProperty( nameof(SequenceAnim.resetOnPlay) );
            _coreProxyProp = serializedObject.FindProperty( nameof(SequenceAnim.coreProxy) );
            _activateNextClipAsapProp = serializedObject.FindProperty( nameof(SequenceAnim.activateNextClipsASAP) );
            
            _clipNodesProp = _sequenceProp.FindPropertyRelative( nameof(Sequence.nodes) );
        }
    }
}
