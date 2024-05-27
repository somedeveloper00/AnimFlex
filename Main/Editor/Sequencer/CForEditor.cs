using AnimFlex.Sequencer;
using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(NodeSelection))]
    public sealed class NodeSelectionDrawer : PropertyDrawer
    {
        private Sequence _sequence;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var indexProp = property.FindPropertyRelative(nameof(NodeSelection.index));
            _sequence ??= SequenceAnimEditor.Current.sequence;
            using (new EditorGUI.PropertyScope(position, label, property))
                AFEditorUtils.DrawNodeSelectionPopup(position, indexProp, label, _sequence);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return AFStyles.Height + AFStyles.VerticalSpace;
        }
    }
}