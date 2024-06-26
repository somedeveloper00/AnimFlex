﻿using AnimFlex.Sequencer;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(ClipNode))]
    public class ClipNodeDrawer : PropertyDrawer
    {
        public static ClipNodeDrawer Current;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Current = this;
            var nameProp = property.FindPropertyRelative(nameof(ClipNode.name));
            var delayProp = property.FindPropertyRelative(nameof(ClipNode.delay));
            var clipProp = property.FindPropertyRelative(nameof(ClipNode.clip));

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position.height = AFStyles.Height;
                DrawHeader();
                if (property.isExpanded)
                {
                    position.y += AFStyles.BigHeight + AFStyles.VerticalSpace;
                    DrawClip();
                }
            }

            Current = null;

            void DrawClip()
            {
                EditorGUI.GetPropertyHeight(clipProp, GUIContent.none, true);
                EditorGUI.PropertyField(position, clipProp, GUIContent.none, true);
            }

            void DrawHeader()
            {
                var rect = new Rect(position)
                {
                    height = AFStyles.BigHeight
                };
                rect.x += 10;
                rect.width = 10;
                property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, string.Empty, true);

                rect.x += rect.width;
                rect.width = position.width - 10 - 10 - 85;

                // label
                rect.width *= 0.5f;
                using (new AFStyles.GuiBackgroundColor(Color.clear))
                {
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        var r = EditorGUI.TextField(rect, nameProp.stringValue, AFStyles.BigTextField);
                        if (check.changed) nameProp.stringValue = r;
                    }
                }

                rect.x += rect.width;

                // display clip type

                // check if type is available
                var value = clipProp.GetValue(out var type);
                if (value is null)
                {
                    AFStyles.DrawHelpBox(rect, "Type Not Found!", MessageType.Error);
                    return;
                }
                if (GUI.Button(rect, AFEditorUtils.GetTypeName(type), AFStyles.Popup))
                {
                    new TypeSelectorMenu<Clip>(clip =>
                    {
                        Undo.RecordObject(property.serializedObject.targetObject, "clip type modified");
                        property.serializedObject.ApplyModifiedProperties();
                        ((ClipNode)property.GetValue()).clip = clip;
                        property.serializedObject.Update();
                    }).Show(rect);
                }

                // delay field
                rect.x += rect.width + 5;
                rect.width = 85 - 5;
                using (new AFStyles.EditorLabelWidth(40))
                    EditorGUI.PropertyField(rect, delayProp, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var clipProp = property.FindPropertyRelative(nameof(ClipNode.clip));
            var h = AFStyles.BigHeight + AFStyles.VerticalSpace * 2;
            if (property.isExpanded)
            {
                h += EditorGUI.GetPropertyHeight(clipProp, GUIContent.none, true);
                h += AFStyles.VerticalSpace;
            }
            return h;
        }
    }
}
