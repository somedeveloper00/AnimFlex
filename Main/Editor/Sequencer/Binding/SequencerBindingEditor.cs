using System;
using AnimFlex.Sequencer;
using AnimFlex.Sequencer.Binding;
using AnimFlex.Sequencer.BindingSystem;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor {
    [CustomEditor( typeof(SequencerBinding) )]
    internal class SequencerBindingEditor : UnityEditor.Editor {
        
        SequencerBinding _sequencerBinding;
        SequenceAnim _sequenceAnim;
        
        SerializedProperty _clipFieldBindersProp;
        SerializedProperty _bindOnPlayProp;
        SerializedProperty _rebindOnEveryPlayProp;
        GUIContent _bindBtnContent = new GUIContent( "Bind", "Bind to SequenceAnim Component" );
        ReorderableList _clipFieldBindersList;
        bool _advancedOptionsExpanded = false;

        public static SequenceAnim CurrentTargetingSequenceAnim;
        

        public override void OnInspectorGUI() {
            EnsureSetup();
            CurrentTargetingSequenceAnim = _sequenceAnim;
            serializedObject.Update();
            drawAdvancedOptions();
            using (new AFStyles.StyledGuiScope( this )) {
                darwBindButton();
                drawBindersList();
            }
            serializedObject.ApplyModifiedProperties();
        }

        void drawAdvancedOptions() {
            _advancedOptionsExpanded = EditorGUILayout.Foldout( _advancedOptionsExpanded, "Advanced Options", true, AFStyles.Foldout );
            if (_advancedOptionsExpanded) {
                using (new GUILayout.HorizontalScope()) {
                    using (new AFStyles.EditorLabelWidth( 80 ))
                        EditorGUILayout.PropertyField( _bindOnPlayProp, GUILayout.ExpandWidth( false ) );
                    if (_bindOnPlayProp.boolValue) {
                        using (new AFStyles.EditorLabelWidth( 130 ))
                            EditorGUILayout.PropertyField( _rebindOnEveryPlayProp, GUILayout.ExpandWidth( false ) );
                    }
                }
            }
        }

        void darwBindButton() {
            using (new GUILayout.HorizontalScope()) {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button( _bindBtnContent, AFStyles.Button, 
                        GUILayout.Height( AFStyles.Height ), GUILayout.Width( 80 ) )) 
                {
                    _sequencerBinding.Bind();
                    EditorUtility.SetDirty( _sequenceAnim );
                }
                GUILayout.FlexibleSpace();
            }
        }

        void drawBindersList() {
            using var _ = new AFStyles.GuiColor( AFStyles.BoxColor );
            using (new AFStyles.GuiBackgroundColor( AFEditorSettings.Instance.backgroundBoxCol ))
                _clipFieldBindersList.DoLayoutList();
        }

        void AddNewClipField() {
            AFEditorUtils.GetTypeInstanceFromHierarchy<ClipFieldBinder>( (type) => {
                _clipFieldBindersProp.arraySize++;
                _clipFieldBindersProp.GetArrayElementAtIndex( _clipFieldBindersProp.arraySize - 1 )
                    .managedReferenceValue = Activator.CreateInstance( type );
                serializedObject.ApplyModifiedProperties();
            } );
        }

        void EnsureSetup() {
            _sequencerBinding ??= (SequencerBinding)target;
            _sequenceAnim ??= _sequencerBinding.GetComponent<SequenceAnim>();
            _clipFieldBindersProp ??= serializedObject.FindProperty( nameof(SequencerBinding.clipFieldBinders) );
            _bindOnPlayProp ??= serializedObject.FindProperty( nameof(SequencerBinding.bindOnPlay) );
            _rebindOnEveryPlayProp ??= serializedObject.FindProperty( nameof(SequencerBinding.rebindOnEveryPlay) );
            
            // setup reorderable list
            if (_clipFieldBindersList == null) {
                _clipFieldBindersList =
                    new ReorderableList( serializedObject, _clipFieldBindersProp, true, false, true, true );
                _clipFieldBindersList.multiSelect = true;
                _clipFieldBindersList.onAddDropdownCallback += (_, _) => AddNewClipField();
                _clipFieldBindersList.drawElementCallback += (rect, index, _, _) => {
                    rect.width -= 30;
                    if (Event.current.type != EventType.Used)
                        EditorGUI.PropertyField( rect, _clipFieldBindersProp.GetArrayElementAtIndex( index ), true );
                    rect.x += rect.width; rect.width = 30;
                    rect.height = AFStyles.Height;
                    if (GUI.Button( rect, "X", AFStyles.ClearButton )) {
                        // check layout phase
                        _clipFieldBindersProp.DeleteArrayElementAtIndex( index );
                        serializedObject.ApplyModifiedProperties();
                    }
                };
                _clipFieldBindersList.elementHeightCallback += (index) =>
                    EditorGUI.GetPropertyHeight( _clipFieldBindersProp.GetArrayElementAtIndex( index ), true );
            }
        }
    }
}