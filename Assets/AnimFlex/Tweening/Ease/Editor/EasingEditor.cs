using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Tweening.Editor
{
    [CustomPropertyDrawer(typeof(Easing))]
    public class EasingEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var idProp = property.FindPropertyRelative("easingIdentifier");

            var options = EasingUtilities.GetOrCreateAllEasingIDs();
            int currentIndex = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].ID == idProp.intValue)
                {
                    currentIndex = i;
                    break;
                }
            }
            
            // draw as enum
            var enumRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            
            var enumValue = EditorGUI.Popup(enumRect, label, currentIndex, options.Select(op => new GUIContent(op.DisplayName)).ToArray());
            if (enumValue != currentIndex)
            {
                idProp.intValue = options[enumValue].ID;
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}