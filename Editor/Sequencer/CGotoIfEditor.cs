using System;
using System.Linq;
using System.Reflection;
using AnimFlex.Sequencer;
using AnimFlex.Sequencer.Clips;
using AnimFlex.Sequencer.UserEnd;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer
{
    public class CGotoIfEditorUtil
    {
        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label, Type type)
        {
            var componentProp = property.FindPropertyRelative(nameof(CGotoIf.component));
            var valueNameProp = property.FindPropertyRelative(nameof(CGotoIf.valueName));
            var valueProp = property.FindPropertyRelative(nameof(CGotoIfBool.value));
            var indexIfProp = property.FindPropertyRelative(nameof(CGotoIf.indexIf));
            var indexElseProp = property.FindPropertyRelative(nameof(CGotoIf.indexElse));

            EditorGUI.BeginProperty(position, label, property);

            var pos = new Rect(position);
            pos.height = AFStyles.Height;

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                EditorGUI.PropertyField(pos, componentProp);
                if (check.changed)
                    AFEditorUtils.OpenComponentReferenceSelectionMenu(componentProp);
            }
            
            pos.y += AFStyles.Height + AFStyles.VerticalSpace;
            AFEditorUtils.DrawFieldNameSelectionPopup(type, componentProp, pos, valueNameProp);

            var sequence = ((SequenceAnim)property.serializedObject.targetObject).sequence;
            
            pos.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField(pos, valueProp, new GUIContent("Is ", valueProp.tooltip));
            
            pos.y += EditorGUI.GetPropertyHeight(valueProp) + AFStyles.VerticalSpace;
            AFEditorUtils.DrawNodeSelectionPopup(pos, indexIfProp, new GUIContent("If True :", indexIfProp.tooltip), sequence);
            
            pos.y += AFStyles.Height + AFStyles.VerticalSpace;
            AFEditorUtils.DrawNodeSelectionPopup(pos, indexElseProp, new GUIContent("Else :", indexElseProp.tooltip), sequence);

            
            EditorGUI.EndProperty();
        }


        public static float GetPropertyHeight(SerializedProperty property) =>
            AFStyles.Height * 4 + AFStyles.VerticalSpace * 5 +
            EditorGUI.GetPropertyHeight(property.FindPropertyRelative(nameof(CGotoIfBool.value)));
    }

    [CustomPropertyDrawer(typeof(CGotoIf), true)]
    public class CGotoIfEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CGotoIfEditorUtil.OnGUI(position, property, label, ((CGotoIf)property.GetValue()).GetValueType());
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return CGotoIfEditorUtil.GetPropertyHeight(property);
        }
    }
}