using System;
using System.ComponentModel;
using System.Linq;
using AnimFlex.Clipper.Internal;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Clipper.Editor
{
    [CustomEditor(typeof(ClipSequence))]
    public class ClipSequenceEditor : UnityEditor.Editor
    {
        private ClipSequence _clipSequence;

        private void OnEnable()
        {
            _clipSequence = target as ClipSequence;
        }

        public override void OnInspectorGUI()
        {
            SetupCustomEditorStyles();

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("playOnStart"));

            var nodesList = serializedObject.FindProperty("nodes");

            DrawClipNodes(nodesList);

            if (GUILayout.Button("+ Add", EditorStyles.toolbarButton))
            {
                var classTypes =
                    from assemblyDomain in AppDomain.CurrentDomain.GetAssemblies()
                    from type in assemblyDomain.GetTypes()
                    where type.IsSubclassOf(typeof(Clip)) && !type.IsAbstract
                    select type;

                var menu = new GenericMenu();
                foreach (var type in classTypes)
                    menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(GetTypeName(type))), false, () =>
                    {
                        nodesList.arraySize++;
                        var clip = nodesList.GetArrayElementAtIndex(nodesList.arraySize - 1)
                            .FindPropertyRelative("clip");
                        clip.managedReferenceValue = Activator.CreateInstance(type);
                        serializedObject.ApplyModifiedProperties();
                    });
                menu.ShowAsContext();
            }

            serializedObject.ApplyModifiedProperties();
            RevertCustomEditorStyles();
        }

   

        private void DrawClipNodes(SerializedProperty nodesList)
        {
            for (var i = 0; i < nodesList.arraySize; i++)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                GUILayout.BeginVertical();
                EditorGUI.indentLevel++;
                DrawClipNodeProperty(nodesList.GetArrayElementAtIndex(i));
                EditorGUI.indentLevel--;
                GUILayout.EndVertical();
                if (GUILayout.Button("X", GUILayout.Width(30))) nodesList.DeleteArrayElementAtIndex(i);

                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
        }

        private void SetupCustomEditorStyles()
        {
            // centering alignments for editor fields
            EditorStyles.textField.alignment = TextAnchor.MiddleCenter;
            EditorStyles.numberField.alignment = TextAnchor.MiddleCenter;
        }
        private void RevertCustomEditorStyles()
        {
            // revert alignments for editor fields
            EditorStyles.textField.alignment = TextAnchor.MiddleLeft;
            EditorStyles.numberField.alignment = TextAnchor.MiddleLeft;
        }

        private void DrawClipNodeProperty(SerializedProperty clipNode)
        {
            var oldColor = GUI.color;
            GUI.color = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeColor;
            GUI.backgroundColor = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeBackgroundColor;

            // draw name of the node
            DrawNodeName();

            EditorGUILayout.PropertyField(clipNode.FindPropertyRelative("delay"));

            // draw clip property
            GUILayout.BeginVertical(EditorStyles.helpBox);
            var clip = clipNode.FindPropertyRelative("clip");
            GUI.color = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipColor;
            GUI.backgroundColor = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipBackgroundColor;
            EditorGUILayout.PropertyField(clip, new GUIContent("Clip Parameters"), true);
            GUILayout.EndVertical();

            GUI.color = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeColor;
            GUI.backgroundColor = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeBackgroundColor;

            // draw next indices options
            var nextIndicesProp = clipNode.FindPropertyRelative("nextIndices");
            var playNextAfterFinishProp = clipNode.FindPropertyRelative("playNextAfterFinish");

            EditorGUILayout.PropertyField(playNextAfterFinishProp);

            if (playNextAfterFinishProp.boolValue)
            {
                if (nextIndicesProp.arraySize > 0) nextIndicesProp.arraySize = 0;
            }
            else
            {
                DrawNextIndices();
            }

            GUI.color = oldColor;

            void DrawNodeName()
            {
                // changing editor styles and keeping their old states
                var oldFontSize = EditorStyles.textField.fontSize;
                EditorStyles.textField.fontSize = 14;
                var oldFontStyle = EditorStyles.textField.fontStyle;
                EditorStyles.textField.fontStyle = FontStyle.BoldAndItalic;
                EditorStyles.textField.alignment = TextAnchor.MiddleCenter;
                var oldHeight = EditorStyles.textField.fixedHeight;
                EditorStyles.textField.fixedHeight = 20;
                var oldLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 0;
                var oldBackgroundColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.clear;

                var clipNameProp = clipNode.FindPropertyRelative("name");
                clipNameProp.stringValue = EditorGUILayout.TextField(clipNameProp.stringValue);
                GUILayout.Space(5);

                // reverting the editor styles old states
                EditorStyles.textField.fontSize = oldFontSize;
                EditorStyles.textField.fontStyle = oldFontStyle;
                EditorStyles.textField.fixedHeight = oldHeight;
                EditorGUIUtility.labelWidth = oldLabelWidth;
                GUI.backgroundColor = oldBackgroundColor;
            }

            void DrawNextIndices()
            {
                var currentLayoutWidth = EditorGUIUtility.currentViewWidth;
                var widthLeft = currentLayoutWidth;

                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Next Clip Nodes", EditorStyles.boldLabel);

                GUILayout.BeginHorizontal();
                for (var i = 0; i < nextIndicesProp.arraySize; i++)
                {
                    if (serializedObject.FindProperty("nodes").arraySize <=
                        nextIndicesProp.GetArrayElementAtIndex(i).intValue) continue;

                    var nextIndexNodeName =
                        serializedObject.FindProperty("nodes")
                            .GetArrayElementAtIndex(nextIndicesProp.GetArrayElementAtIndex(i).intValue)
                            .FindPropertyRelative("name").stringValue;

                    var estimatedWidth = EditorStyles.textField.CalcSize(new GUIContent(nextIndexNodeName)).x;
                    if (estimatedWidth >= widthLeft - 20)
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        widthLeft = currentLayoutWidth;
                    }

                    // actual button
                    if (GUILayout.Button(new GUIContent(nextIndexNodeName, "Click to remove"),
                            GUILayout.ExpandWidth(false))) nextIndicesProp.DeleteArrayElementAtIndex(i);
                    widthLeft -= estimatedWidth;
                }

                // draw add button now
                var estimatedAddButtonRect = EditorStyles.textField.CalcSize(new GUIContent("Add Next      ")).x;
                if (estimatedAddButtonRect >= widthLeft - 20)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    widthLeft = currentLayoutWidth;
                }

                if (GUILayout.Button(new GUIContent("Add Next      ", "Click to add"), EditorStyles.toolbarDropDown,
                        GUILayout.ExpandWidth(false)))
                {
                    var menu = new GenericMenu();
                    for (var i = 0; i < serializedObject.FindProperty("nodes").arraySize; i++)
                    {
                        var clipNodeName = serializedObject.FindProperty("nodes").GetArrayElementAtIndex(i)
                            .FindPropertyRelative("name").stringValue;
                        var currentIndex = i; // copying the index to a local variable to avoid the closure issue
                        menu.AddItem(new GUIContent(clipNodeName), false, () =>
                        {
                            nextIndicesProp.arraySize++;
                            nextIndicesProp.GetArrayElementAtIndex(nextIndicesProp.arraySize - 1).intValue =
                                currentIndex;
                            serializedObject.ApplyModifiedProperties();
                        });
                    }

                    menu.ShowAsContext();
                }

                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
        }

        private static string GetTypeName(Type type)
        {
            return type.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() is DisplayNameAttribute
                displayNameAttr
                ? displayNameAttr.DisplayName
                : type.Name;
        }
    }
}