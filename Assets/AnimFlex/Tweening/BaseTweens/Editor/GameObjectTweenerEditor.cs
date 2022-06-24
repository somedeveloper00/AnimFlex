using System;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Tweening.Editor
{
    [CustomEditor(typeof(GameObjectTweener))]
    public class GameObjectTweenerEditor : UnityEditor.Editor
    {
        GameObjectTweener _tweener;

        private void OnEnable()
        {
            _tweener = target as GameObjectTweener;
        }

        private void OnDisable()
        {
            TweeningUpdater.EndEditorPlay();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Play"))
            {
                _tweener.Play();
            }
            if (GUILayout.Button("Stop"))
            {
                TweeningUpdater.EndEditorPlay();
            }
            GUILayout.EndHorizontal();
        }
    }
}