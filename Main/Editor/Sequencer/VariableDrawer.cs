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
            var nameProp = property.FindPropertyRelative(nameof(Variable.name));
            var w = position.width;
            position.width = EditorGUIUtility.labelWidth - 5;
            if (GUI.enabled)
            {
                EditorGUI.PropertyField(position, nameProp, GUIContent.none);
            }
            else
            {
                GUI.Label(position, nameProp.stringValue);
            }
            position.x += position.width + 5;
            position.width = w - position.width;
            EditorGUI.PropertyField(position, valueProp, GUIContent.none, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProp = property.FindPropertyRelative(nameof(Variable<int>.Value));
            return EditorGUI.GetPropertyHeight(valueProp);
        }
    }
}