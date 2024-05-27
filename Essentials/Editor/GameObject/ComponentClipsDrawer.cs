using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(ComponentDestroy), true)]
    public sealed class ComponentDestroyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var componentProp = property.FindPropertyRelative(nameof(ComponentDestroy.component));
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position.height = AFStyles.Height;
                using var check = new EditorGUI.ChangeCheckScope();
                EditorGUI.PropertyField(position, componentProp);
                if (check.changed)
                    AFEditorUtils.OpenComponentReferenceSelectionMenu(componentProp);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => AFStyles.Height + AFStyles.VerticalSpace;
    }
    [CustomPropertyDrawer(typeof(ComponentSetActive), true)]
    public sealed class ComponentSetActiveDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var componentProp = property.FindPropertyRelative(nameof(ComponentSetActive.component));
            var activeProp = property.FindPropertyRelative(nameof(ComponentSetActive.active));
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position.height = AFStyles.Height;
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUI.PropertyField(position, componentProp);
                    if (check.changed)
                        AFEditorUtils.OpenComponentReferenceSelectionMenu(componentProp);
                }

                position.y += AFStyles.Height + AFStyles.VerticalSpace;
                EditorGUI.PropertyField(position, activeProp);
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            2 * (AFStyles.Height + AFStyles.VerticalSpace);
    }
}