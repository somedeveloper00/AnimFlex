using System;
using AnimFlex.Sequencer;
using AnimFlex.Sequencer.UserEnd;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer
{
    [CustomEditor(typeof(SequenceAnim))]
    public class ClipSequenceEditor : UnityEditor.Editor
    {
        private SequenceAnim _sequenceAnim;
        private Sequence _sequence;
        
        private SerializedProperty _sequenceProp;
        private SerializedProperty _playOnStartProp;
        private SerializedProperty _clipNodesProp;

        private ReorderableList _nodeClipList;

        private void OnEnable()
        {
            _sequenceAnim = target as SequenceAnim;
            _sequence = _sequenceAnim.sequence;
            GetProperties();

            SetupNodeListDrawer();
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_playOnStartProp);

            DrawClipNodes();
            DrawAddButton();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawClipNodes()
        {
            _nodeClipList.DoLayoutList();
            for (int i = 0; i < _clipNodesProp.arraySize; i++)
            {
                // DrawClipBody(_clipNodesProp.GetArrayElementAtIndex(i), i);
            }
        }

        private void SetupNodeListDrawer()
        {
            _nodeClipList = new ReorderableList(serializedObject, elements: _clipNodesProp, draggable: true,
                displayHeader: false, displayAddButton: false, displayRemoveButton: false);
            _nodeClipList.multiSelect = false;
            _nodeClipList.drawElementCallback = (rect, index, active, focused) =>
            {
                var nodeProp = _clipNodesProp.GetArrayElementAtIndex(index);
                // DrawClipBody(nodeProp, index);
                EditorGUI.PropertyField(rect, nodeProp, GUIContent.none, true);
            };
            _nodeClipList.elementHeightCallback = index =>
            {
                var nodeProp = _clipNodesProp.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(nodeProp, true);
            };
            _nodeClipList.onSelectCallback = list =>
            {
                _nodeClipList.ClearSelection();
            };
            // _nodeClipList.
        }

        private void DrawAddButton()
        {
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
            {
                GUILayout.Space(20);
                if (GUILayout.Button(
                        new GUIContent("Add Clip", "Adds a new clip node to the list of nodes"),
                        AFStyles.BigButton, GUILayout.ExpandWidth(true)))
                {
                    AFEditorUtils.CreateTypeInstanceFromHierarchy<Clip>(clip =>
                    {
                        RecordUndo();
                        _sequence.AddNewClipNode(clip);
                        serializedObject.Update();
                    });
                }

                GUILayout.Space(20);
            }
        }

        private void DrawClipBody(SerializedProperty clipNode, int index)
        {
            using (new AFStyles.GuiColor(StyleSettings.Instance.tweeerBoxCol))
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    var clip = clipNode.FindPropertyRelative(nameof(ClipNode.clip));
                    
                    // label
                    GUILayout.BeginHorizontal();
                    
                    // DrawColorNodePicker(clipNode);
                    GUILayout.FlexibleSpace();
                    // DrawClipDropdownLabel(index);
                    GUILayout.EndHorizontal();
                    
                    EditorGUILayout.PropertyField(clip, GUIContent.none, true);
                    
                }
            }
        }

        private void RecordUndo()
        {
            Undo.RecordObject(_sequenceAnim, "Sequence component modified");
        }
        private void GetProperties()
        {
            _sequenceProp = serializedObject.FindProperty(nameof(SequenceAnim.sequence));
            _playOnStartProp = serializedObject.FindProperty("playOnStart");
            _clipNodesProp = _sequenceProp.FindPropertyRelative(nameof(Sequence.nodes));
        }
    }
}