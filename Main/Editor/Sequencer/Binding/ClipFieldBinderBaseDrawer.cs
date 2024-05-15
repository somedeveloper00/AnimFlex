using AnimFlex.Sequencer;
using AnimFlex.Sequencer.BindingSystem;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(SequenceBinder), true)]
    public class SequenceBinderBaseDrawer : PropertyDrawer
    {
        private static GUIContent _valueGuiContent;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sequenceAnimProp = property.FindPropertyRelative(nameof(SequenceBinder.sequenceAnim));
            var variableIndexProp = property.FindPropertyRelative(nameof(SequenceBinder.variableIndex));
            var valueProp = property.FindPropertyRelative(nameof(SequenceBinder_Int.value));

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                // draw containing box
                EditorGUI.DrawRect(position, AFEditorSettings.Instance.BoxColOutline);
                position.x += AFStyles.VerticalSpace / 2;
                position.width -= AFStyles.VerticalSpace;
                position.y += AFStyles.VerticalSpace / 2;
                position.height -= AFStyles.VerticalSpace;
                EditorGUI.DrawRect(position, AFEditorSettings.Instance.BoxColDarker);
                position.x -= AFStyles.VerticalSpace / 2;
                position.width += AFStyles.VerticalSpace;
                position.y -= AFStyles.VerticalSpace / 2;
                position.height += AFStyles.VerticalSpace;

                position.x += 15; position.width -= 30;
                position.y += AFStyles.VerticalSpace;
                position.height = AFStyles.Height;

                drawHeader();
                if (property.isExpanded)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        drawBodyTop();
                    }
                }
            }

            void drawHeader()
            {
                using (new GUILayout.HorizontalScope())
                {
                    var oldX = position.x;
                    var oldWidth = position.width;
                    position.width = EditorGUIUtility.labelWidth; // dropdown and sequencer field clickable
                    property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
                    position.x += EditorGUIUtility.labelWidth - 15;
                    position.width = oldWidth - EditorGUIUtility.labelWidth - 70;
                    EditorGUI.PropertyField(position, sequenceAnimProp, GUIContent.none);
                    position.x += position.width;
                    position.width = 10;
                    GUI.Label(position, ":");
                    position.x += position.width;
                    position.width = 60 + 15;
                    var sequence = sequenceAnimProp.objectReferenceValue as SequenceAnim;
                    if (sequence != null && sequence.sequence.variables.Length > 0)
                    {
                        if (sequence.sequence.variables.Length <= variableIndexProp.intValue)
                        {
                            variableIndexProp.intValue = 0;
                        }
                        var guiContent = new GUIContent(sequence.sequence.variables[variableIndexProp.intValue].name);
                        if (EditorGUI.DropdownButton(position, guiContent, FocusType.Keyboard))
                        {
                            valueProp.GetValue(out var type);
                            if (sequence != null)
                            {
                                var menu = new GenericMenu();
                                for (int i = 0; i < sequence.sequence.variables.Length; i++)
                                {
                                    if (sequence.sequence.variables[i].Type == type)
                                    {
                                        int ind = i;
                                        menu.AddItem(new GUIContent(sequence.sequence.variables[i].name), i == variableIndexProp.intValue, () =>
                                        {
                                            variableIndexProp.intValue = ind;
                                            property.serializedObject.ApplyModifiedProperties();
                                        });
                                    }
                                }
                                menu.ShowAsContext();
                            }
                        }
                    }
                    else
                    {
                        EditorGUI.PropertyField(position, variableIndexProp, GUIContent.none);
                    }
                    position.width = oldWidth;
                    position.x = oldX;
                }
                position.y += Mathf.Max(EditorGUI.GetPropertyHeight(valueProp), AFStyles.Height) + AFStyles.VerticalSpace;
            }

            void drawBodyTop()
            {
                EditorGUI.PropertyField(position, valueProp, _valueGuiContent);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProp = property.FindPropertyRelative(nameof(SequenceBinder_Int.value));
            var h = Mathf.Max(AFStyles.Height, EditorGUI.GetPropertyHeight(valueProp)) + AFStyles.VerticalSpace * 2;

            if (property.isExpanded)
            {
                h += EditorGUI.GetPropertyHeight(valueProp);
            }

            return h;
        }
    }
}