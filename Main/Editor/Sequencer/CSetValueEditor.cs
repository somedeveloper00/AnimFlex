using System;
using AnimFlex.Editor;
using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Sequencer.Editor
{
    public class CSetValueEditorUtils {
        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label, Type type) {
            var componentProp = property.FindPropertyRelative( nameof(CSetValue.component) );
            var valueNameProp = property.FindPropertyRelative( nameof(CSetValue.fieldName) );
            var newValueProp = property.FindPropertyRelative( nameof(CSetValue<int>.value) );

            EditorGUI.BeginProperty( position, label, property );

            var pos = new Rect( position );
            pos.height = AFStyles.Height;

            using (var check = new EditorGUI.ChangeCheckScope()) {
                EditorGUI.PropertyField( pos, componentProp );
                if (check.changed)
                    AFEditorUtils.OpenComponentReferenceSelectionMenu( componentProp );
            }

            pos.y += AFStyles.Height + AFStyles.VerticalSpace;

            AFEditorUtils.DrawFieldNameSelectionPopup( type, componentProp, pos, valueNameProp );

            pos.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField( pos, newValueProp, true );

            EditorGUI.EndProperty();
        }

        public static void OnPropertyGUI(Rect position, SerializedProperty property, GUIContent label, Type type) {
            var componentProp = property.FindPropertyRelative( nameof(CSetPropertyValue.component) );
            var valueNameProp = property.FindPropertyRelative( nameof(CSetPropertyValue.propertyName) );
            var newValueProp = property.FindPropertyRelative( nameof(CSetPropertyValue<int>.value) );

            EditorGUI.BeginProperty( position, label, property );

            var pos = new Rect( position );
            pos.height = AFStyles.Height;

            using (var check = new EditorGUI.ChangeCheckScope()) {
                EditorGUI.PropertyField( pos, componentProp );
                if (check.changed)
                    AFEditorUtils.OpenComponentReferenceSelectionMenu( componentProp );
            }

            pos.y += AFStyles.Height + AFStyles.VerticalSpace;

            AFEditorUtils.DrawPropertyNameSelectionPopup( type, componentProp, pos, valueNameProp );

            pos.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField( pos, newValueProp, true );

            EditorGUI.EndProperty();
        }

        public static float GetPropertyHeight(SerializedProperty property) =>
            AFStyles.Height * 2 + AFStyles.VerticalSpace * 3 + 
            EditorGUI.GetPropertyHeight( property.FindPropertyRelative( "value" ) );
    }

    [CustomPropertyDrawer( typeof(CSetValue), true )]
    public class CSetValueEditor : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            CSetValueEditorUtils.OnGUI( position, property, label, ( (CSetValue)property.GetValue() ).GetValueType() );

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            CSetValueEditorUtils.GetPropertyHeight( property );
    }

    [CustomPropertyDrawer( typeof(CSetPropertyValue), true )]
    public class CSetPropertyValueEditor : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            CSetValueEditorUtils.OnPropertyGUI( position, property, label, ( (CSetPropertyValue)property.GetValue() ).GetValueType() );

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            CSetValueEditorUtils.GetPropertyHeight( property );
    }


}