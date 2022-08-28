using AnimFlex.Editor;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Tweener.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TweenerAnim))]
    public class TweenerAnimEditor : UnityEditor.Editor
    {
        private TweenerAnim _tweenerAnim;
        private SerializedProperty _generatorDataProp;
        private SerializedProperty _playOnStartProp;
        
        private void OnEnable()
        {
            _tweenerAnim = target as TweenerAnim;
            _generatorDataProp = serializedObject.FindProperty(nameof(TweenerAnim.generatorData));
            _playOnStartProp = serializedObject.FindProperty(nameof(TweenerAnim.playOnStart));
            
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.color = Styles.TweenerBoxColor;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUI.color = Color.white;
                EditorGUILayout.PropertyField(_playOnStartProp);
                EditorGUILayout.PropertyField(_generatorDataProp);
            }

            if(!PreviewUtils.isActive)
                serializedObject.ApplyModifiedProperties();
        }


        
    }
}