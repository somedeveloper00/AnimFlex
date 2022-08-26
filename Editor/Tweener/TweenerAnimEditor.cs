using UnityEditor;
using UnityEngine;

namespace AnimFlex.Tweener.Editor
{
    [CustomEditor(typeof(TweenerAnim))]
    public class TweenerAnimEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Play"))
            {
                
            }

            if (GUILayout.Button("Stop"))
            {
                
            }
        }
    }
}