using AnimFlex.Tweening;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomEditor(typeof(TweenerComponent), true)]
    [CanEditMultipleObjects]
    public class TweenerComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(TweenerPosition.playOnStart)));

            using (new AFStyles.StyledGuiScope( this ))
	        using (new AFStyles.GuiColor(AFStyles.BoxColorDarker))
            using (new EditorGUI.DisabledScope( Application.isPlaying ))
		    EditorGUILayout.PropertyField( serializedObject.FindProperty( nameof(TweenerPosition.generator) ) );

            serializedObject.ApplyModifiedProperties();
        }
    }
}
