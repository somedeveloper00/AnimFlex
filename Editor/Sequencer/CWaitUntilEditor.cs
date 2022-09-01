using System;
using System.Linq;
using System.Reflection;
using AnimFlex.Editor;
using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Sequencer.Editor
{
    public class CWaitUntilUtil
    {
        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label, Type type)
        {
              var componentProp = property.FindPropertyRelative(nameof(CWaitUntil.component));
            var valueNameProp = property.FindPropertyRelative(nameof(CWaitUntil.valueName));
            var checkEveryProp = property.FindPropertyRelative(nameof(CWaitUntil.checkEvery));
            var newValueProp = property.FindPropertyRelative(nameof(CWaitUntilBool.value));

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
            
            pos.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField(pos, checkEveryProp, true);
            
            pos.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField(pos, newValueProp, true);

            EditorGUI.EndProperty();
        }

        public static float GetPropertyHeight(SerializedProperty property) =>
            AFStyles.Height * 3 + AFStyles.VerticalSpace * 4 +
            EditorGUI.GetPropertyHeight(property.FindPropertyRelative(nameof(CWaitUntilBool.value)));
    }

    [CustomPropertyDrawer(typeof(CWaitUntil), true)]
    public class CWaitUntilEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            CWaitUntilUtil.OnGUI(position, property, label, ((CWaitUntil)property.GetValue()).GetValueType());

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            CWaitUntilUtil.GetPropertyHeight(property);
    }
}