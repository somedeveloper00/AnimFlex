#if UNITY_EDITOR
using System.Linq;
using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Sequencer.Editor
{
    [CustomPropertyDrawer(typeof(CSetValue))]
    public class CSetValueEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var componentProp = property.FindPropertyRelative("component");
            var valueNameProp = property.FindPropertyRelative("valueName");
            var newValueProp = property.FindPropertyRelative("value");

            EditorGUI.BeginProperty(position, label, property);

            var pos = new Rect(position);
            pos.x += 10; pos.width -= 20;
            pos.height = EditorGUIUtility.singleLineHeight;

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                EditorGUI.PropertyField(pos, componentProp);
                pos.y += EditorGUI.GetPropertyHeight(componentProp) + EditorGUIUtility.standardVerticalSpacing;
                if (check.changed) 
                    ClipEditorsUtility.OpenComponentReferenceSelectionMenu(componentProp);
            }
            
            EditorGUI.PropertyField(pos, valueNameProp);
            pos.y += EditorGUI.GetPropertyHeight(valueNameProp) + EditorGUIUtility.standardVerticalSpacing;
            
            EditorGUI.PropertyField(pos, newValueProp, true);
            pos.height = EditorGUI.GetPropertyHeight(newValueProp, true);
            pos.y += pos.height + EditorGUIUtility.standardVerticalSpacing;
            
            position.height = pos.y + pos.height - position.y;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
        }
    }
}
#endif