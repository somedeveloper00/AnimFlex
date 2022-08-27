using AnimFlex.Editor;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Tweener.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TweenerAnim))]
    public class TweenerAnimEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Play"))
            {
                PreviewUtils.PreviewTweener(target as TweenerAnim);
            }

            if (GUILayout.Button("Stop"))
            {
                PreviewUtils.StopPreviewMode();
            }
        }
    }
}