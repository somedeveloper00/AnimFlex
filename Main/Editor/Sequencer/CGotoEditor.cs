﻿using AnimFlex.Sequencer;
using AnimFlex.Sequencer.Clips;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(CGoto))]
    public class CGotoEditor : PropertyDrawer
    {
        private SerializedProperty _indexProp;
        private Sequence _sequence;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _indexProp = property.FindPropertyRelative(nameof(CGoto.index));
            _sequence ??= ((SequenceAnim)property.serializedObject.targetObject).sequence;
            
            EditorGUI.BeginProperty(position, label, property);

            AFEditorUtils.DrawNodeSelectionPopup(position, _indexProp, new GUIContent("Next :", _indexProp.tooltip), _sequence);

            EditorGUI.EndProperty();
        }

       

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return AFStyles.Height + AFStyles.VerticalSpace;
        }
    }
}