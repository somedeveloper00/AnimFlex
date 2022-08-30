using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer
{
    [CustomPropertyDrawer(typeof(TYPE))]
    public class CGotoEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}