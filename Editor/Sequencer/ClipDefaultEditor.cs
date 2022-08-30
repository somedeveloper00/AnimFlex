using System.Collections;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer
{
    [CustomPropertyDrawer(typeof(Clip), true)]
    public class ClipDefaultEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int depth = property.depth;
            if (property.Next(true))
            {
                do
                {
                    if (property.depth <= depth) break;
                    EditorGUI.PropertyField(position, property, new GUIContent(property.displayName), true);
                    position.y += EditorGUI.GetPropertyHeight(property);
                } while (property.Next(false));
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 0;
            var depth = property.depth;
            if (property.Next(true))
            {
                do
                {
                    if(property.depth <= depth) break;
                    height += EditorGUI.GetPropertyHeight(property);
                } while (property.Next (false));
            }

            return height;
        }
    }
}