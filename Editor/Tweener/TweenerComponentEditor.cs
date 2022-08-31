using AnimFlex.Tweener;
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
            
            using (new AFStyles.GuiColor(StyleSettings.Instance.backgroundBoxColDarker))
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    using (new AFStyles.GuiColor(Color.white))
                    {
                        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(TweenerPosition.generator)));
                    }
                }
                
            }
                
            serializedObject.ApplyModifiedProperties();
            
            if(StyleSettings.Instance.repaintEveryFrame)
                Repaint();
        }
    }
}