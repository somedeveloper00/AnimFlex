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
        private SerializedProperty _resetOnPlayProp;
        private SerializedProperty _clipNodesProp;

        private ReorderableList _nodeClipList;

        private Vector2 _lastMousePos = Vector2.zero;

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

            using (new GUILayout.HorizontalScope())
            {
				EditorGUILayout.PropertyField(_playOnStartProp);
				EditorGUILayout.PropertyField(_resetOnPlayProp);
            }

            GUILayout.Space(10);
            DrawPlayback();
            if (!AFPreviewUtils.isActive)
            {
	            DrawClipNodes();
				DrawAddButton();
            }

            serializedObject.ApplyModifiedProperties();
        }

        public override bool RequiresConstantRepaint() => AFEditorSettings.Instance.repaintEveryFrame;

        private void DrawPlayback()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new AFStyles.GuiForceActive())
                {
	                using (new EditorGUI.DisabledScope(Application.isPlaying))
	                {
		                var text = AFPreviewUtils.isActive ? "Stop Preview" : "Preview Sequence";
		                if (GUILayout.Button(text, AFStyles.BigButton,
			                    GUILayout.Height(AFStyles.BigHeight), GUILayout.Width(200)))
		                {
			                if (AFPreviewUtils.isActive)
				                AFPreviewUtils.StopPreviewMode();
			                else
				                AFPreviewUtils.PreviewSequence(_sequence);
		                }
	                }
                }

                GUILayout.FlexibleSpace();
            }
        }

        private void DrawClipNodes()
        {
            using var _ = new AFStyles.GuiColor(AFStyles.BoxColor);
            using (new AFStyles.GuiBackgroundColor(AFEditorSettings.Instance.backgroundBoxCol))
                _nodeClipList.DoLayoutList();
        }

        private void SetupNodeListDrawer()
        {
            _nodeClipList = new ReorderableList(serializedObject, elements: _clipNodesProp, draggable: true,
                displayHeader: false, displayAddButton: false, displayRemoveButton: false);
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
                                new Rect(rect.x + rect.width - 20 + 5, rect.y, 30,
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
            _nodeClipList.footerHeight = 0;
            _nodeClipList.headerHeight = 0;
        }

        private void DrawAddButton()
        {
	        using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
            {
	            GUILayout.FlexibleSpace();
                if (GUILayout.Button(
                        new GUIContent("+ Add Clip", "Adds a new clip node to the list of nodes"),
                        AFStyles.BigButton, GUILayout.Width(150)))
                {
                    AFEditorUtils.CreateTypeInstanceFromHierarchy<Clip>(clip =>
                    {
                        RecordUndo();
                        _sequence.AddNewClipNode(clip);
                        serializedObject.Update();
                    });
                }
                GUILayout.FlexibleSpace();
            }
        }

        private void RecordUndo()
        {
            Undo.RecordObject(_sequenceAnim, "Sequence component modified");
        }
        private void GetProperties()
        {
            _sequenceProp = serializedObject.FindProperty(nameof(SequenceAnim.sequence));
            _playOnStartProp = serializedObject.FindProperty(nameof(_sequenceAnim.playOnEnable));
            _resetOnPlayProp = serializedObject.FindProperty(nameof(_sequenceAnim.resetOnPlay));
            _clipNodesProp = _sequenceProp.FindPropertyRelative(nameof(Sequence.nodes));
        }
    }
}
