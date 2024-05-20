using System;
using System.Linq;
using AnimFlex.Core.Proxy;
using AnimFlex.Editor.Preview;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor
{
    [CustomEditor(typeof(SequenceAnim))]
    public class SequenceAnimEditor : UnityEditor.Editor
    {
        internal static SequenceAnimEditor Current;
        internal SequenceAnim sequenceAnim;
        internal Sequence sequence;

        internal SerializedProperty sequenceProp;
        internal SerializedProperty playOnStartProp;
        internal SerializedProperty dontWaitInQueueToPlayProp;
        internal SerializedProperty resetOnPlayProp;
        internal SerializedProperty clipNodesProp;
        internal SerializedProperty variablesProp;
        internal SerializedProperty useProxyAsCoreProp;
        internal SerializedProperty coreProxyProp;
        internal SerializedProperty useDefaultCoreProxyProp;
        internal SerializedProperty defaultCoreProxyProp;
        internal SerializedProperty activateNextClipAsapProp;

        internal TypeSelectorMenu<Variable> variableTypeSelectionMenu;
        internal TypeSelectorMenu<Clip> clipTypeSelectionMenu;

        private ReorderableList _variableList;
        private ReorderableList _nodeClipList;
        private GUIContent[] _coreProxyTypeOptions = null;
        private static readonly GUIContent _addCoreProxyGuiContent = new("Add", "Add a new Core Proxy Component to this Game Object");
        private static readonly GUIContent _xButtonGuiContent = new("X", "Remove Element");
        private static readonly GUIContent _variablesListGuiContent = new("Variables", "Values that can be changed at runtime");
        private static readonly GUIContent _clipNodeListGuiContent = new("Clips", "Clips to play in this Sequencer");

        private void OnEnable()
        {
            sequenceAnim = target as SequenceAnim;
            sequence = sequenceAnim.sequence;
            GetProperties();

            const int START_LEN = 17; // "AnimFlexCoreProxy";
            _coreProxyTypeOptions = AnimFlexCoreProxyHelper.AllCoreProxyTypes
                .Select(t => new GUIContent(t.Name[START_LEN..], defaultCoreProxyProp.tooltip)).ToArray();

            SetupNodeListDrawer();
            SetupVariableDrawer();
        }

        public override void OnInspectorGUI()
        {
            Current = this;
            serializedObject.Update();

            DrawPlayback();
            using (new EditorGUI.DisabledScope(Application.isPlaying))
            {
                GUILayout.Space(AFStyles.VerticalSpace);
                DrawVariables();
                GUILayout.Space(AFStyles.VerticalSpace);
                DrawClipNodes();
                GUILayout.Space(AFStyles.VerticalSpace);
                DrawAdvancedOptions();
            }

            serializedObject.ApplyModifiedProperties();
            Current = null;
        }

        private void DrawAdvancedOptions()
        {
            playOnStartProp.isExpanded = EditorGUILayout.Foldout(playOnStartProp.isExpanded, "Advanced Options", true);
            if (playOnStartProp.isExpanded)
            {

                using (new GUILayout.HorizontalScope())
                {
                    using (new AFStyles.EditorLabelWidth(90))
                        EditorGUILayout.PropertyField(playOnStartProp, GUILayout.Width(110));
                    using (new AFStyles.EditorLabelWidth(155))
                        EditorGUILayout.PropertyField(dontWaitInQueueToPlayProp);
                }
                using (new GUILayout.HorizontalScope())
                {
                    using (new AFStyles.EditorLabelWidth(90))
                        EditorGUILayout.PropertyField(resetOnPlayProp, GUILayout.Width(110));
                    using (new AFStyles.EditorLabelWidth(155))
                        EditorGUILayout.PropertyField(activateNextClipAsapProp);
                }

                using (new GUILayout.HorizontalScope())
                {
                    using (new AFStyles.EditorLabelWidth(110))
                        EditorGUILayout.PropertyField(useProxyAsCoreProp, GUILayout.Width(130));

                    if (useProxyAsCoreProp.boolValue)
                    {
                        using (new AFStyles.EditorLabelWidth(140))
                            EditorGUILayout.PropertyField(useDefaultCoreProxyProp, GUILayout.Width(160));

                        if (useDefaultCoreProxyProp.boolValue)
                        {
                            var result = EditorGUILayout.Popup(AnimFlexCoreProxyHelper.AllCoreProxyTypeNames.IndexOf(
                                defaultCoreProxyProp.stringValue), _coreProxyTypeOptions);
                            if (result != -1)
                            {
                                defaultCoreProxyProp.stringValue = AnimFlexCoreProxyHelper.AllCoreProxyTypeNames[result];
                            }
                        }
                    }
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (useProxyAsCoreProp.boolValue && !useDefaultCoreProxyProp.boolValue)
                    {
                        using (new AFStyles.EditorLabelWidth(80))
                        {
                            EditorGUILayout.PropertyField(coreProxyProp);
                        }

                        if (coreProxyProp.objectReferenceValue == null)
                        {
                            if (GUILayout.Button(_addCoreProxyGuiContent, GUILayout.Width(60)))
                            {
                                // add core proxy component
                                AFEditorUtils.GetTypeInstanceFromHierarchy<AnimflexCoreProxy>(type =>
                                {
                                    serializedObject.ApplyModifiedProperties();
                                    if (sequenceAnim.TryGetComponent(type, out var comp))
                                    {
                                        sequenceAnim.coreProxy = (AnimflexCoreProxy)comp;
                                    }
                                    else
                                    {
                                        sequenceAnim.coreProxy =
                                            (AnimflexCoreProxy)sequenceAnim.gameObject.AddComponent(type);
                                    }

                                    serializedObject.Update();
                                });
                            }
                        }
                    }
                }
            }
        }

        private void DrawPlayback()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new AFStyles.GuiForceActive(true))
                {
                    using (new EditorGUI.DisabledScope(Application.isPlaying))
                    {
                        var text = AFPreviewUtils.IsActive ? "Stop Preview" : "Preview Sequence";
                        if (GUILayout.Button(text, AFStyles.BigButton,
                                GUILayout.Height(AFStyles.BigHeight), GUILayout.Width(200)))
                        {
                            if (AFPreviewUtils.IsActive)
                                AFPreviewUtils.StopPreviewMode();
                            else
                                AFPreviewUtils.PreviewSequence(sequenceAnim);
                        }
                    }
                }

                GUILayout.FlexibleSpace();
            }
        }

        private void DrawClipNodes()
        {
            using var _ = new AFStyles.GuiColor(AFStyles.BoxColor);
            using var __ = new AFStyles.GuiBackgroundColor(AFEditorSettings.Instance.backgroundBoxCol);
            clipNodesProp.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(clipNodesProp.isExpanded, _clipNodeListGuiContent);
            EditorGUILayout.EndFoldoutHeaderGroup();
            if (clipNodesProp.isExpanded)
            {
                _nodeClipList.DoLayoutList();
            }
        }

        private void DrawVariables()
        {
            using var _ = new AFStyles.GuiColor(AFStyles.BoxColor);
            using var __ = new AFStyles.GuiBackgroundColor(AFEditorSettings.Instance.backgroundBoxCol);
            variablesProp.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(variablesProp.isExpanded, _variablesListGuiContent);
            EditorGUILayout.EndFoldoutHeaderGroup();
            if (variablesProp.isExpanded)
            {
                _variableList.DoLayoutList();
            }
        }

        private void SetupNodeListDrawer()
        {
            _nodeClipList = new ReorderableList(serializedObject, clipNodesProp, draggable: true,
                displayHeader: true, displayAddButton: true, displayRemoveButton: true)
            {
                drawElementCallback = (rect, index, _, _) =>
                {
                    if (clipNodesProp == null)
                        return;
                    rect.width -= 20;
                    if (Event.current.type != EventType.Used)
                        EditorGUI.PropertyField(rect, clipNodesProp.GetArrayElementAtIndex(index), GUIContent.none, true);

                    // draw X button
                    rect.x += rect.width; rect.width = 20;
                    rect.height = AFStyles.Height;
                    if (GUI.Button(rect, _xButtonGuiContent, AFStyles.ClearButton))
                    {
                        clipNodesProp.DeleteArrayElementAtIndex(index);
                        serializedObject.ApplyModifiedProperties();
                    }
                },
                elementHeightCallback = index =>
                {
                    var nodeProp = clipNodesProp.GetArrayElementAtIndex(index);
                    return EditorGUI.GetPropertyHeight(nodeProp, true);
                },
                headerHeight = 0,
                footerHeight = AFStyles.Height,
                onAddDropdownCallback = (rect, List) =>
                {
                    clipTypeSelectionMenu.Show(rect);
                }
            };

            clipTypeSelectionMenu = new(clip =>
            {
                RecordUndo();
                sequence.AddNewClipNode(clip);
                serializedObject.Update();
            });
        }

        private void SetupVariableDrawer()
        {
            _variableList = new ReorderableList(serializedObject, elements: variablesProp, draggable: true,
                displayHeader: false, displayAddButton: true, displayRemoveButton: true)
            {
                drawElementCallback = (rect, index, _, _) =>
                {
                    rect.width -= 20;
                    if (Event.current.type != EventType.Used)
                    {
                        string label = $"{index}    {(sequence.variables.Length > index - 1 ? sequence.variables[index].Type.Name : string.Empty)}";
                        EditorGUI.PropertyField(rect, variablesProp.GetArrayElementAtIndex(index), new(label), true);
                    }

                    // draw X button
                    rect.x += rect.width; rect.width = 20;
                    rect.height = AFStyles.Height;
                    if (GUI.Button(rect, _xButtonGuiContent, AFStyles.ClearButton))
                    {
                        variablesProp.DeleteArrayElementAtIndex(index);
                        serializedObject.ApplyModifiedProperties();
                    }
                },
                elementHeightCallback = index =>
                {
                    return EditorGUI.GetPropertyHeight(variablesProp.GetArrayElementAtIndex(index), true);
                },
                headerHeight = 0,
                footerHeight = AFStyles.Height,
                onAddDropdownCallback = (rect, List) =>
                {
                    variableTypeSelectionMenu.Show(rect);
                }
            };

            variableTypeSelectionMenu = new(variable =>
            {
                RecordUndo();
                Array.Resize(ref sequence.variables, sequence.variables.Length + 1);
                sequence.variables[^1] = variable;
                serializedObject.Update();
            });
        }

        private void RecordUndo()
        {
            Undo.RecordObject(sequenceAnim, "Sequence component modified");
        }

        private void GetProperties()
        {
            sequenceProp = serializedObject.FindProperty(nameof(SequenceAnim.sequence));
            playOnStartProp = serializedObject.FindProperty(nameof(SequenceAnim.playOnStart));
            dontWaitInQueueToPlayProp = serializedObject.FindProperty(nameof(SequenceAnim.dontWaitInQueueToPlay));
            defaultCoreProxyProp = serializedObject.FindProperty(nameof(SequenceAnim.defaultCoreProxy));
            useDefaultCoreProxyProp = serializedObject.FindProperty(nameof(SequenceAnim.useDefaultCoreProxy));
            useProxyAsCoreProp = serializedObject.FindProperty(nameof(SequenceAnim.useProxyAsCore));
            resetOnPlayProp = serializedObject.FindProperty(nameof(SequenceAnim.resetOnPlay));
            coreProxyProp = serializedObject.FindProperty(nameof(SequenceAnim.coreProxy));
            activateNextClipAsapProp = serializedObject.FindProperty(nameof(SequenceAnim.activateNextClipsASAP));

            clipNodesProp = sequenceProp.FindPropertyRelative(nameof(Sequence.nodes));
            variablesProp = sequenceProp.FindPropertyRelative(nameof(Sequence.variables));
        }
    }
}
