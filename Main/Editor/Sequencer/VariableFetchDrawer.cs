using UnityEngine;
using UnityEditor;
using AnimFlex.Sequencer;

namespace AnimFlex.Editor
{
    [CustomPropertyDrawer(typeof(VariableFetch<>), useForChildren: true)]
    public sealed class VariableFetchDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (SequenceAnimEditor.Current == null)
            {
                return;
            }
            var indexProp = property.FindPropertyRelative(nameof(VariableFetch<int>._index));
            var valueProp = property.FindPropertyRelative(nameof(VariableFetch<int>.value));
            AFEditorUtils.GetValue(valueProp, out var type);
            var rect = new Rect(position) { width = 20 };

            if (GUI.Button(rect, "", AFStyles.Popup))
            {
                Variable[] variables = SequenceAnimEditor.Current.sequence.variables;

                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Constant"), indexProp.intValue == 0, () =>
                {
                    property.serializedObject.Update();
                    indexProp.intValue = 0;
                    property.serializedObject.ApplyModifiedProperties();
                });

                for (int i = 0; i < variables.Length; i++)
                {
                    int index = i;
                    if (variables[i].Type == type)
                    {
                        menu.AddItem(new GUIContent("Variables/" + i), indexProp.intValue == i + 1, () =>
                        {
                            for (int i = 0; i < variables.Length; i++)
                            {
                                if (variables[i].Type == type)
                                {
                                    property.serializedObject.Update();
                                    indexProp.intValue = index + 1;
                                    property.serializedObject.ApplyModifiedProperties();
                                    return;
                                }
                            }
                        });
                    }
                    else
                    {
                        menu.AddDisabledItem(new GUIContent("Variables/" + i), false);
                    }
                }
                menu.ShowAsContext();
            }
            position.x += rect.width + 5;
            position.width -= rect.width + 5;
            if (indexProp.intValue != 0)
            {
                if (SequenceAnimEditor.Current.variablesProp.arraySize < indexProp.intValue)
                {
                    AFStyles.DrawHelpBox(position, "index out of bounds!", MessageType.Error);
                }
                else if (SequenceAnimEditor.Current.sequence.variables[indexProp.intValue - 1].Type != type)
                {
                    AFStyles.DrawHelpBox(position, "wrong type!", MessageType.Error);
                }
                else
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUI.PropertyField(position, SequenceAnimEditor.Current.variablesProp.GetArrayElementAtIndex(indexProp.intValue - 1),
                            new(label) { text = label.text + $" ({indexProp.intValue - 1})" }, true);
                    }
                }
            }
            else
            {
                EditorGUI.PropertyField(position, valueProp, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var indexProp = property.FindPropertyRelative(nameof(VariableFetch<int>._index));
            var valueProp = property.FindPropertyRelative(nameof(VariableFetch<int>.value));
            return indexProp.intValue == 0
                ? AFStyles.Height
                : EditorGUI.GetPropertyHeight(valueProp);

        }
    }
}