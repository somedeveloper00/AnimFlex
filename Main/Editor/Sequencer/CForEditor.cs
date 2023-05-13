using AnimFlex.Sequencer;
using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor {
    
    [CustomPropertyDrawer(typeof(CFor))]
    public class CForEditor : PropertyDrawer
    {
        private Sequence _sequence;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var indexProp = property.FindPropertyRelative(nameof(CFor.index));
            var countProp = property.FindPropertyRelative(nameof(CFor.count));
            var inbetweenDelayProp = property.FindPropertyRelative(nameof(CFor.inbetweenDelay));
            _sequence ??= ((SequenceAnim)property.serializedObject.targetObject).sequence;
            
            EditorGUI.BeginProperty(position, label, property);

            AFEditorUtils.DrawNodeSelectionPopup(position, indexProp, new GUIContent("Target :", indexProp.tooltip), _sequence);
            position.height = AFStyles.Height;
            position.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField( position, countProp );
            position.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField( position, inbetweenDelayProp );

            EditorGUI.EndProperty();
        }

       

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 3 * ( AFStyles.Height + AFStyles.VerticalSpace );
        }
    }
}