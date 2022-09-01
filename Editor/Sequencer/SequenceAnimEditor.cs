using AnimFlex.Sequencer;
using AnimFlex.Sequencer.UserEnd;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer
{
    [CustomEditor(typeof(SequenceAnim))]
    public class SequenceAnimEditor : UnityEditor.Editor
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
            DrawPlayback();

            serializedObject.ApplyModifiedProperties();
            
            if(StyleSettings.Instance.repaintEveryFrame)
                Repaint();
        }

        private void DrawPlayback()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new AFStyles.GuiForceActive())
                {
                    if (GUILayout.Button(
                            text: PreviewUtils.isActive ? "Stop" : "Play", style: AFStyles.BigButton, 
                            GUILayout.Height(height: AFStyles.BigHeight), GUILayout.Width(200)))
                    {
                        if (PreviewUtils.isActive)
                            PreviewUtils.StopPreviewMode();
                        else
                            PreviewUtils.PreviewSequence(sequence: _sequence);
                    }
                }

                GUILayout.FlexibleSpace();
            }
        }

        private void DrawClipNodes()
        {
            using var _ = new AFStyles.GuiColor(AFStyles.BoxColor);
            using (new AFStyles.GuiBackgroundColor(StyleSettings.Instance.backgroundBoxCol))
                _nodeClipList.DoLayoutList();
        }

        private void SetupNodeListDrawer()
        {
            _nodeClipList = new ReorderableList(serializedObject, elements: _clipNodesProp, draggable: true,
                displayHeader: false, displayAddButton: false, displayRemoveButton: false);
            _nodeClipList.multiSelect = false;
            _nodeClipList.drawElementCallback = (rect, index, active, focused) =>
            {
                using (new AFStyles.StyledGuiScope())
                {
                    var nodeProp = _clipNodesProp.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, nodeProp, GUIContent.none, true);

                    // draw X button
                    using (new AFStyles.GuiBackgroundColor(Color.clear))
                    {
                        if (GUI.Button(
                                new Rect(rect.x + rect.width - 20 + 5, rect.y, 20,
                                    AFStyles.BigHeight + AFStyles.VerticalSpace * 2)
                                , new GUIContent("X", "Remove clip"), AFStyles.ClearButton))
                        {
                            EditorApplication.delayCall += () =>
                            {
                                serializedObject.Update();
                                _clipNodesProp.DeleteArrayElementAtIndex(index);
                                serializedObject.ApplyModifiedProperties();
                                Repaint();
                            };
                        }
                    }
                }
            };
            _nodeClipList.elementHeightCallback = index =>
            {
                var nodeProp = _clipNodesProp.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(nodeProp, true);
            };
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