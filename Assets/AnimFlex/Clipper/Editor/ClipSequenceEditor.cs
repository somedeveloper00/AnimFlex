using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace AnimFlex.Clipper.Editor
{
    [CustomEditor(typeof(ClipSequence))]
    public class ClipSequenceEditor : UnityEditor.Editor
    {
        // key: serializedProperty's path
        static private Dictionary<string, bool> _isExtended = new Dictionary<string, bool>();
        
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
                ClipEditorsUtility.CreateTypeInstanceFromHierarchy<Clip>((value) =>
                {
                    nodesList.arraySize++;
                    var clip = nodesList.GetArrayElementAtIndex(nodesList.arraySize - 1)
                        .FindPropertyRelative("clip");
                    clip.managedReferenceValue = value;
                    serializedObject.ApplyModifiedProperties();
                });
            }

            serializedObject.ApplyModifiedProperties();
            RevertCustomEditorStyles();
        }


        private void DrawClipNodes(SerializedProperty nodesList)
        {
            for (var i = 0; i < nodesList.arraySize; i++)
            {
                var oldColor = GUI.color;
                var oldBackCol = GUI.backgroundColor;
                GUI.color = GetColorOfClipNode(nodesList.GetArrayElementAtIndex(i));
                GUI.backgroundColor = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeBackgroundColor;
                
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                GUILayout.BeginVertical();
                EditorGUI.indentLevel++;
                
                SerializedProperty clipNode = nodesList.GetArrayElementAtIndex(i);
                DrawNodeLabel(clipNode, out bool isExtended);
                if (isExtended)
                {
                    GUI.color = oldColor;
                    GUI.backgroundColor = oldBackCol;
                    DrawClipBody(clipNode);
                    GUI.color = GetColorOfClipNode(nodesList.GetArrayElementAtIndex(i));
                    GUI.backgroundColor = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeBackgroundColor;
                }
                
                DrawNextNodesGui(clipNode);
                
                EditorGUI.indentLevel--;
                GUILayout.EndVertical();
                
                DrawNodeTools(nodesList, i);

                GUILayout.EndHorizontal();
                GUILayout.Space(5);
                
                GUI.color = oldColor;
                GUI.backgroundColor = oldBackCol;
            }
        }

        private Color GetColorOfClipNode(SerializedProperty clipNode)
        {
            var color = clipNode.FindPropertyRelative("inspectorColor").colorValue;
            color.a = 1;
            return color != Color.black ? color : ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeColor;
        }

        private void DrawNodeTools(SerializedProperty nodesList, int i)
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("X", GUILayout.Width(30)))
            {
                nodesList.DeleteArrayElementAtIndex(i);
                // resolve connected nodes
                for (int j = 0; j < nodesList.arraySize; j++)
                {
                    var nextIndicesProp = nodesList.GetArrayElementAtIndex(j).FindPropertyRelative("nextIndices");
                    for (int k = 0; k < nextIndicesProp.arraySize; k++)
                    {
                        if (nextIndicesProp.GetArrayElementAtIndex(k).intValue == i)
                            nextIndicesProp.DeleteArrayElementAtIndex(k);
                        else if (nextIndicesProp.GetArrayElementAtIndex(k).intValue >= i)
                            nextIndicesProp.GetArrayElementAtIndex(k).intValue--;
                    }
                }
                serializedObject.ApplyModifiedProperties();
                // Repaint();
            }

            GUILayout.Space(5);
            
            GUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUI.BeginDisabledGroup(i == 0);
            if (GUILayout.Button("▲", GUILayout.Width(25)))
            {
                for (int j = 0; j < nodesList.arraySize; j++)
                {
                    var nextIndicesProp = nodesList.GetArrayElementAtIndex(j).FindPropertyRelative("nextIndices");
                    for (int k = 0; k < nextIndicesProp.arraySize; k++)
                    {
                        if (nextIndicesProp.GetArrayElementAtIndex(k).intValue == i)
                            nextIndicesProp.GetArrayElementAtIndex(k).intValue--;
                        else if (nextIndicesProp.GetArrayElementAtIndex(k).intValue == i - 1)
                            nextIndicesProp.GetArrayElementAtIndex(k).intValue++;
                    }
                }
                // nodesList.MoveArrayElement(i, i - 1);
                serializedObject.ApplyModifiedProperties();
                (_clipSequence.nodes[i], _clipSequence.nodes[i - 1]) = (_clipSequence.nodes[i - 1], _clipSequence.nodes[i]);
                serializedObject.Update();
                Repaint();
            }
            EditorGUI.EndDisabledGroup();
            
            EditorGUI.BeginDisabledGroup(i == nodesList.arraySize - 1);
            if (GUILayout.Button("▼", GUILayout.Width(25)))
            {
                for (int j = 0; j < nodesList.arraySize; j++)
                {
                    var nextIndicesProp = nodesList.GetArrayElementAtIndex(j).FindPropertyRelative("nextIndices");
                    for (int k = 0; k < nextIndicesProp.arraySize; k++)
                    {
                        if(nextIndicesProp.GetArrayElementAtIndex(k).intValue == i)
                            nextIndicesProp.GetArrayElementAtIndex(k).intValue ++;
                        else if(nextIndicesProp.GetArrayElementAtIndex(k).intValue == i + 1)
                            nextIndicesProp.GetArrayElementAtIndex(k).intValue --;
                    }
                }
                nodesList.MoveArrayElement(i, i + 1);
                serializedObject.ApplyModifiedProperties();
                Repaint();
            }
            EditorGUI.EndDisabledGroup();
            
            GUILayout.EndVertical();
            
            GUILayout.EndVertical();
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

        private void DrawClipBody(SerializedProperty clipNode)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            var clip = clipNode.FindPropertyRelative("clip");
            
            // label
            GUILayout.BeginHorizontal();
            
            DrawColorNodePicker(clipNode);
            GUILayout.FlexibleSpace();
            DrawClipDropdownLabel(clipData);
            GUILayout.EndHorizontal();
            
            EditorGUILayout.PropertyField(clip, new GUIContent("Clip Parameters"), true);
            GUILayout.EndVertical();
        }

        private void DrawColorNodePicker(SerializedProperty clip)
        {
            EditorGUILayout.PropertyField(clip.FindPropertyRelative("inspectorColor"),
                new GUIContent("", "Set the color of this clipData node"), 
                GUILayout.MaxWidth(80));
        }

        private void DrawClipDropdownLabel(SerializedProperty clip)
        {
            var currentType = ClipEditorsUtility.FindType(clip.managedReferenceFullTypename.Split(' ').Last());
            var typeButtonLabel = ClipEditorsUtility.GetTypeName(currentType);
            if (GUILayout.Button($"  {typeButtonLabel} ", EditorStyles.toolbarDropDown, GUILayout.ExpandWidth(false)))
            {
                ClipEditorsUtility.CreateTypeInstanceFromHierarchy<Clip>((value) =>
                {
                    clip.managedReferenceValue = value;
                    serializedObject.ApplyModifiedProperties();
                });
            }
        }
        private void DrawNodeLabel(SerializedProperty clipNode, out bool isExtended)
        {
            GUILayout.BeginHorizontal();
            isExtended = _isExtended.ContainsKey(clipNode.propertyPath) && _isExtended[clipNode.propertyPath];
            var label = isExtended ? "↓" : "→";
            if (GUILayout.Button(label, GUILayout.Width(20)))
            {
                isExtended = !isExtended;
                _isExtended[clipNode.propertyPath] = isExtended;
            }

            DrawNodeName();

            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 60;
            EditorGUILayout.PropertyField(clipNode.FindPropertyRelative("delay"), GUILayout.Width(100));
            EditorGUIUtility.labelWidth = oldLabelWidth;
            
            GUILayout.EndHorizontal();

            void DrawNodeName(params GUILayoutOption[] options)
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
                GUILayout.BeginVertical();
                clipNameProp.stringValue = EditorGUILayout.TextField(clipNameProp.stringValue, options);
                GUILayout.Space(5);
                GUILayout.EndVertical();

                // reverting the editor styles old states
                EditorStyles.textField.fontSize = oldFontSize;
                EditorStyles.textField.fontStyle = oldFontStyle;
                EditorStyles.textField.fixedHeight = oldHeight;
                EditorGUIUtility.labelWidth = oldLabelWidth;
                GUI.backgroundColor = oldBackgroundColor;
            }
            

        }

        private void DrawNextNodesGui(SerializedProperty clipNode)
        {
            var nodesProp = serializedObject.FindProperty("nodes");
            var nextIndicesProp = clipNode.FindPropertyRelative("nextIndices");
            var playNextAfterFinishProp = clipNode.FindPropertyRelative("playNextAfterFinish");


            if (playNextAfterFinishProp.boolValue)
            {
                if (nextIndicesProp.arraySize > 0) nextIndicesProp.arraySize = 0;
            }
            
            GUILayout.BeginVertical(EditorStyles.helpBox);
            
            
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Next Clip Nodes", EditorStyles.boldLabel, GUILayout.MaxWidth(90));

            GUILayout.FlexibleSpace();
            
            // draw "play next" toggle
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 80;
            EditorGUILayout.PropertyField(playNextAfterFinishProp, new GUIContent("Play Next", "Plays next node whe finished"));
            EditorGUIUtility.labelWidth = oldLabelWidth;
            
            GUILayout.EndHorizontal();

            if (!playNextAfterFinishProp.boolValue)
            {
                DrawNextIndices();
            }
            
            GUILayout.EndVertical();


            void DrawNextIndices()
            {
                var currentLayoutWidth = EditorGUIUtility.currentViewWidth;
                var widthLeft = currentLayoutWidth;

                var oldCol = GUI.color;

                GUILayout.BeginHorizontal();
                for (var i = 0; i < nextIndicesProp.arraySize; i++)
                {
                    var nextNodeIndex = nextIndicesProp.GetArrayElementAtIndex(i).intValue;
                    if (nodesProp.arraySize <= nextNodeIndex) continue;
                    

                    var node = nodesProp.GetArrayElementAtIndex(nextNodeIndex);
                    var nextIndexNodeName = node.FindPropertyRelative("name").stringValue;

                    var estimatedWidth = EditorStyles.textField.CalcSize(new GUIContent(nextIndexNodeName)).x;
                    if (estimatedWidth >= widthLeft - 40)
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        widthLeft = currentLayoutWidth;
                    }

                    // actual button
                    GUI.color = GetColorOfClipNode(node);
                    
                    if (GUILayout.Button(new GUIContent(nextIndexNodeName, "Click to remove"),
                        GUILayout.ExpandWidth(false))) nextIndicesProp.DeleteArrayElementAtIndex(i);
                    
                    GUI.color = oldCol;
                    widthLeft -= estimatedWidth;
                }

                // draw add button now
                var estimatedAddButtonRect = EditorStyles.textField.CalcSize(new GUIContent("Add Next      ")).x;
                if (estimatedAddButtonRect >= widthLeft - 40)
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
                        var clipNodeName = serializedObject.FindProperty("nodes")
                            .GetArrayElementAtIndex(i)
                            .FindPropertyRelative("name")
                            .stringValue;
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

            }
        }
    }
}