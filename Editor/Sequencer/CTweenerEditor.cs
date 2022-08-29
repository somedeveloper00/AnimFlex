using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer
{
    [CustomPropertyDrawer(typeof(CTweener))]
    public class CTweenerEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position.y += AFStyles.Height;
            EditorGUI.PropertyField(position, property.FindPropertyRelative(nameof(CTweener.data)), GUIContent.none);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return AFStyles.Height + EditorGUI.GetPropertyHeight(property.FindPropertyRelative(nameof(CTweener.data)), GUIContent.none);
        }
    }
}