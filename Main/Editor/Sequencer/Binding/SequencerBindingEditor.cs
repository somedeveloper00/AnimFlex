using AnimFlex.Sequencer.Binding;
using AnimFlex.Sequencer.UserEnd;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer.Binding {
    [CustomEditor( typeof(SequencerBinding) )]
    public class SequencerBindingEditor : UnityEditor.Editor {
        
        SequencerBinding _sequencerBinding;
        SequenceAnim _sequenceAnim;
        
        SerializedProperty _clipFieldBindersProp;

        void OnEnable() {
            _sequencerBinding = (SequencerBinding)target;
            _sequenceAnim = _sequencerBinding.GetComponent<SequenceAnim>();
        }

        public override void OnInspectorGUI() {
            getProperties();
            serializedObject.Update();
            using (new AFStyles.StyledGuiScope( this )) {
                EditorGUILayout.PropertyField( _clipFieldBindersProp, true );
                if (GUILayout.Button( "Bind" )) {
                    _sequencerBinding.Bind();
                }

                if (GUILayout.Button( "Add" )) {
                    _clipFieldBindersProp.arraySize++;
                    _clipFieldBindersProp.GetArrayElementAtIndex( _clipFieldBindersProp.arraySize - 1 )
                        .managedReferenceValue = new ClipFieldBinderTransform();
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        void getProperties() {
            _clipFieldBindersProp ??= serializedObject.FindProperty( nameof(SequencerBinding.clipFieldBinders) );
        }
    }
}