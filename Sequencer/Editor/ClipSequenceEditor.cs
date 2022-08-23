using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace AnimFlex.Sequencer.Editor
{
    [CustomEditor(typeof(AnimFlexSequence))]
    public class ClipSequenceEditor : UnityEditor.Editor
    {
        // key: serializedProperty's path
        static private Dictionary<string, bool> _isExtended = new Dictionary<string, bool>();
        
        private AnimFlexSequence _clipSequence;
        private GUIStyle _biggerButtons;

        private void OnEnable()
        {
            _clipSequence = target as AnimFlexSequence;
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
                    serializedObject.ApplyModifiedProperties();
                    Undo.RecordObject(_clipSequence, "Add Clip");
                    _clipSequence.AddNewClipNode(value);
                    serializedObject.Update();
                });
            }


            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            GUILayout.Space(15);
            DrawPlaybackTools();
            EditorGUI.EndDisabledGroup();
            RevertCustomEditorStyles();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPlaybackTools()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if(GUILayout.Button("Play", EditorStyles.toolbarButton))
            {
                _clipSequence.Play();
            }
            if (GUILayout.Button("Resume", EditorStyles.toolbarButton))
            {
                _clipSequence.Resume();
            }
            if(GUILayout.Button("Pause", EditorStyles.toolbarButton))
            {
                _clipSequence.Pause();
            }
            if(GUILayout.Button("Stop", EditorStyles.toolbarButton))
            {
                _clipSequence.StopAndDeleteComponent();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

 
        private void DrawClipNodes(SerializedProperty nodesList)
        {
            var oldColor = GUI.color;
            var oldBackCol = GUI.backgroundColor;
            
            string lastGroupName = String.Empty;
            
            
            for (var i = 0; i < nodesList.arraySize; i++)
            {
                SerializedProperty clipNode = nodesList.GetArrayElementAtIndex(i);
                
                // start of group check
                var groupNameProp = clipNode.FindPropertyRelative("groupName");
                bool canAddGroup = false;
                bool isFirstElementInGroup = false;
                bool isLastElementInGroup = false;
                bool isGroupExtended = _isExtended.ContainsKey(groupNameProp.stringValue) && _isExtended[groupNameProp.stringValue];
                

                GroupTopBar(groupNameProp, i, isGroupExtended, ref isFirstElementInGroup, ref canAddGroup, ref isLastElementInGroup);


                if (groupNameProp.stringValue == String.Empty || isGroupExtended)
                {
                    GUI.color = GetColorOfClipNode(nodesList.GetArrayElementAtIndex(i));
                    GUI.backgroundColor = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeBackgroundColor;
                    
                    
                    GUILayout.BeginHorizontal();
                    DrawGroupToolbar(canAddGroup, groupNameProp, i, isFirstElementInGroup, isLastElementInGroup);

                    GUILayout.Space(10);
                    
                    GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandHeight(false));
                    
                    
                    GUILayout.BeginVertical();
                    EditorGUI.indentLevel++;
                    
                    DrawNodeLabel(clipNode, out bool isExtended);
                    if (isExtended)
                    {
                        GUI.color = oldColor;
                        GUI.backgroundColor = oldBackCol;
                        DrawClipBody(clipNode, i);
                        GUI.color = GetColorOfClipNode(nodesList.GetArrayElementAtIndex(i));
                        GUI.backgroundColor = ClipSequencerEditorPrefs.GetOrCreatePrefs().clipNodeBackgroundColor;
                    }
                    
                    DrawNextNodesGui(clipNode);
                    
                    EditorGUI.indentLevel--;
                    GUILayout.EndVertical();
                    
                    DrawNodeTools(nodesList, i);

                    GUILayout.EndHorizontal();
                    
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    
                    GUI.color = oldColor;
                    GUI.backgroundColor = oldBackCol;
                }
                
                
                if(isLastElementInGroup) 
                    GUILayout.EndVertical();
            }

            void GroupTopBar(SerializedProperty groupNameProp, int i, bool isGroupExtended, ref bool isFirstElementInGroup, ref bool canAddGroup, ref bool isLastElementInGroup)
            {
                if (lastGroupName == String.Empty)
                {
                    if (groupNameProp.stringValue != string.Empty)
                    {
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        lastGroupName = groupNameProp.stringValue;

                        EditorGUI.BeginChangeCheck();

                        EditorGUILayout.BeginHorizontal();
                        DrawGroupLabel();
                        GUILayout.EndHorizontal();

                        if (EditorGUI.EndChangeCheck())
                        {
                            if (lastGroupName != groupNameProp.stringValue)
                            {
                                // emoty string is a specially invalid case
                                if (groupNameProp.stringValue == String.Empty)
                                    groupNameProp.stringValue = lastGroupName;
                                
                                // syncing the groupName of the rest of the group
                                for (int k = i + 1; k < nodesList.arraySize; k++)
                                {
                                    var elementGroupNameProp = nodesList.GetArrayElementAtIndex(k).FindPropertyRelative("groupName");
                                    if (elementGroupNameProp.stringValue == lastGroupName)
                                        elementGroupNameProp.stringValue = groupNameProp.stringValue;
                                }

                                _isExtended[groupNameProp.stringValue] = isGroupExtended;
                            }
                        }

                        isFirstElementInGroup = true;
                    }
                    else
                    {
                        canAddGroup = true;
                    }
                }

                // check if last element in group
                if (groupNameProp.stringValue != String.Empty)
                {
                    if (i == nodesList.arraySize - 1 ||
                        nodesList.GetArrayElementAtIndex(i + 1).FindPropertyRelative("groupName").stringValue != groupNameProp.stringValue)
                    {
                        lastGroupName = String.Empty;
                        isLastElementInGroup = true;
                    }
                }
                void DrawGroupLabel()
                {
                    BigLabel("Group Name: ", FontStyle.Normal, GUILayout.MaxWidth(100));
                    BigTextField(groupNameProp, GUILayout.ExpandWidth(true));
                    if (GUILayout.Button(new GUIContent("/", "Show/Hide group"), GUILayout.Width(100)))
                    {
                        _isExtended[groupNameProp.stringValue] = !isGroupExtended;
                    }
                }
            }

            void DrawGroupToolbar(bool canAddGroup, SerializedProperty groupNameProp, int i, bool isFirstElementInGroup, bool isLastElementInGroup)
            {
                GUILayout.BeginVertical(GUILayout.Width(20), GUILayout.ExpandHeight(true));
                if (canAddGroup)
                {
                    // add group button
                    if (GUILayout.Button(new GUIContent("+", "Add a new group here"), _biggerButtons, GUILayout.Width(20),
                            GUILayout.ExpandHeight(true)))
                    {
                        groupNameProp.stringValue = $"Group ({(i + 1)})";
                        _isExtended[groupNameProp.stringValue] = true;
                    }
                }
                else
                {
                    if (isFirstElementInGroup)
                    {
                        // expand button
                        EditorGUI.BeginDisabledGroup(i == 0);
                        if (GUILayout.Button(new GUIContent("+", "Add the below cip to this group"), _biggerButtons, GUILayout.Width(20)))
                        {
                            nodesList.GetArrayElementAtIndex(i - 1).FindPropertyRelative("groupName").stringValue =
                                groupNameProp.stringValue;
                        }

                        EditorGUI.EndDisabledGroup();

                        if (!isLastElementInGroup)
                        {
                            if (GUILayout.Button(new GUIContent("-", "Remove Top-most cip from this group"), _biggerButtons, GUILayout.Width(20)))
                            {
                                groupNameProp.stringValue = string.Empty;
                            }
                        }
                    }

                    if (isFirstElementInGroup && isLastElementInGroup)
                    {
                        // delete button
                        if (GUILayout.Button(new GUIContent("X", "Delete group"), _biggerButtons, GUILayout.Width(20),
                                GUILayout.ExpandHeight(true)))
                        {
                            groupNameProp.stringValue = string.Empty;
                        }
                    }
                    else
                    {
                        GUILayout.FlexibleSpace();
                    }

                    if (isLastElementInGroup)
                    {
                        if (!isFirstElementInGroup)
                        {
                            // shrink button
                            if (GUILayout.Button(new GUIContent("-", "Remove Bottom-most cip from this group"),
                                    GUILayout.Width(20)))
                            {
                                groupNameProp.stringValue = String.Empty;
                            }
                        }

                        // expand group
                        EditorGUI.BeginDisabledGroup(i == nodesList.arraySize - 1);
                        if (GUILayout.Button(new GUIContent("+", "Add the below cip to this group"), _biggerButtons, GUILayout.Width(20)))
                        {
                            nodesList.GetArrayElementAtIndex(i + 1).FindPropertyRelative("groupName").stringValue =
                                groupNameProp.stringValue;
                        }

                        EditorGUI.EndDisabledGroup();
                    }
                }

                GUILayout.EndVertical();
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
                serializedObject.ApplyModifiedProperties();
                _clipSequence.RemoveClipNodeAtIndex(i);
                serializedObject.Update();
            }

            GUILayout.Space(5);
            
            GUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUI.BeginDisabledGroup(i == 0);
            if (GUILayout.Button("▲", GUILayout.Width(25)))
            {
                Undo.RecordObject(_clipSequence, "Move Clip Up");
                serializedObject.ApplyModifiedProperties();
                _clipSequence.MoveClipNode(i, i - 1);
                serializedObject.Update();
            }
            EditorGUI.EndDisabledGroup();
            
            EditorGUI.BeginDisabledGroup(i == nodesList.arraySize - 1);
            if (GUILayout.Button("▼", GUILayout.Width(25)))
            {
                Undo.RecordObject(_clipSequence, "Move Clip Down");
                serializedObject.ApplyModifiedProperties();
                _clipSequence.MoveClipNode(i, i + 1);
                serializedObject.Update();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndVertical();
            
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("+↯", "Insert a new clip below"), _biggerButtons, GUILayout.Width(30)))
            {
                ClipEditorsUtility.CreateTypeInstanceFromHierarchy<Clip>((clip) =>
                {
                    serializedObject.ApplyModifiedProperties();
                    Undo.RecordObject(_clipSequence, "Insert Clip");
                    _clipSequence.InsertNewClipAt(clip, i + 1);
                    serializedObject.Update();
                });
            }
            
            
            GUILayout.EndVertical();
        }

        private void SetupCustomEditorStyles()
        {
            _biggerButtons = new GUIStyle(GUI.skin.button)
            {
                fontSize = 18
            };
            // centering alignments for editor fields
            EditorStyles.textField.alignment = TextAnchor.MiddleCenter;
            EditorStyles.label.alignment = TextAnchor.MiddleCenter;
            EditorStyles.numberField.alignment = TextAnchor.MiddleCenter;
        }
        private void RevertCustomEditorStyles()
        {
            // revert alignments for editor fields
            EditorStyles.textField.alignment = TextAnchor.MiddleLeft;
            EditorStyles.label.alignment = TextAnchor.MiddleLeft;
            EditorStyles.numberField.alignment = TextAnchor.MiddleLeft;
        }

        private void DrawClipBody(SerializedProperty clipNode, int index)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            var clip = clipNode.FindPropertyRelative("clip");
            
            // label
            GUILayout.BeginHorizontal();
            
            DrawColorNodePicker(clipNode);
            GUILayout.FlexibleSpace();
            DrawClipDropdownLabel(index);
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

        private void DrawClipDropdownLabel(int index)
        {
            var currentType = ClipEditorsUtility.FindType(_clipSequence.nodes[index].clip.GetType().FullName);
            var typeButtonLabel = ClipEditorsUtility.GetTypeName(currentType);
            if (GUILayout.Button($"  {typeButtonLabel} ", EditorStyles.toolbarDropDown, GUILayout.ExpandWidth(false)))
            {
                ClipEditorsUtility.CreateTypeInstanceFromHierarchy<Clip>((clip) =>
                {
                    Undo.RecordObject(_clipSequence, "Change Clip Type");
                    serializedObject.ApplyModifiedProperties();
                    _clipSequence.nodes[index].clip = clip;
                    serializedObject.Update();
                });
            }
        }
        private void DrawNodeLabel(SerializedProperty clipNode, out bool isExtended)
        {
            GUILayout.BeginHorizontal();
            isExtended = !_isExtended.ContainsKey(clipNode.propertyPath) || _isExtended[clipNode.propertyPath];
            var label = isExtended ? "↓" : "→";
            if (GUILayout.Button(label, GUILayout.Width(20)))
            {
                isExtended = !isExtended;
                _isExtended[clipNode.propertyPath] = isExtended;
            }

            BigTextField(clipNode.FindPropertyRelative("name"));

            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 60;
            EditorGUILayout.PropertyField(clipNode.FindPropertyRelative("delay"), GUILayout.Width(100));
            EditorGUIUtility.labelWidth = oldLabelWidth;
            
            GUILayout.EndHorizontal();
        }

        private void BigTextField(SerializedProperty stringProperty, params GUILayoutOption[] options)
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

            GUILayout.BeginVertical();
            stringProperty.stringValue = EditorGUILayout.TextField(stringProperty.stringValue, options);
            GUILayout.Space(5);
            GUILayout.EndVertical();

            // reverting the editor styles old states
            EditorStyles.textField.fontSize = oldFontSize;
            EditorStyles.textField.fontStyle = oldFontStyle;
            EditorStyles.textField.fixedHeight = oldHeight;
            EditorGUIUtility.labelWidth = oldLabelWidth;
            GUI.backgroundColor = oldBackgroundColor;
        }
        
        private void BigLabel(string label, FontStyle style, params GUILayoutOption[] options)
        {
            // changing editor styles and keeping their old states
            var oldFontSize = EditorStyles.textField.fontSize;
            EditorStyles.label.fontSize = 14;
            var oldFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = style;
            EditorStyles.label.alignment = TextAnchor.MiddleCenter;
            var oldHeight = EditorStyles.label.fixedHeight;
            EditorStyles.label.fixedHeight = 20;
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 0;
            var oldBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.clear;

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField(label, options);
            GUILayout.Space(5);
            GUILayout.EndVertical();

            // reverting the editor styles old states
            EditorStyles.label.fontSize = oldFontSize;
            EditorStyles.label.fontStyle = oldFontStyle;
            EditorStyles.label.fixedHeight = oldHeight;
            EditorGUIUtility.labelWidth = oldLabelWidth;
            GUI.backgroundColor = oldBackgroundColor;
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