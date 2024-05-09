using UnityEngine;
using UnityEditor;
using AnimFlex.Sequencer;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(Variable), useForChildren: true)]
    public class VariableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProp = property.FindPropertyRelative(nameof(Variable<int>.Value));
            EditorGUI.PropertyField(position, valueProp, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProp = property.FindPropertyRelative(nameof(Variable<int>.Value));
            return EditorGUI.GetPropertyHeight(valueProp);
        }
    }
}