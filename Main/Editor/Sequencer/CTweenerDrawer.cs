using UnityEngine;
using UnityEditor;
using AnimFlex.Sequencer.Clips;
using AnimFlex.Editor;

[CustomPropertyDrawer(typeof(CTweener))]
public sealed class CTweenerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var playNextOnStartProp = property.FindPropertyRelative(nameof(CTweener.playNextOnStart));
        var generatorProp = property.FindPropertyRelative(nameof(CTweenerPosition.tweenerGenerator));
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUI.PropertyField(position, playNextOnStartProp);
            position.y += AFStyles.Height + AFStyles.VerticalSpace;
            EditorGUI.PropertyField(position, generatorProp);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var playNextOnStartProp = property.FindPropertyRelative(nameof(CTweener.playNextOnStart));
        var generatorProp = property.FindPropertyRelative(nameof(CTweenerPosition.tweenerGenerator));
        return EditorGUI.GetPropertyHeight(playNextOnStartProp) + EditorGUI.GetPropertyHeight(generatorProp);
    }
}