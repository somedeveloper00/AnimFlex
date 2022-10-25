using AnimFlex.Tweening;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomEditor(typeof(TweenerComponent), true)]
    public class TweenerComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(TweenerPosition.playOnStart)));

            using (new AFStyles.GuiColor(AFStyles.BoxColorDarker))
            {
	            using (new AFStyles.GuiColor(Color.white))
	            {
		            if (!AFPreviewUtils.isActive)
		            {
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(TweenerPosition.generator)));
		            }
	            }
            }

            serializedObject.ApplyModifiedProperties();

            if(AFEditorSettings.Instance.repaintEveryFrame)
                Repaint();
        }
    }
}
