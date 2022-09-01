using System;
using System.Linq;
using AnimFlex.Tweener;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomPropertyDrawer(typeof(AFSelection), true)]
    public class MultiTweenerSelectionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var objectRefProp = property.FindPropertyRelative(nameof(SampleAFSelection.objectRef));
            var typeProp = property.FindPropertyRelative(nameof(SampleAFSelection.type));

            position.height = AFStyles.Height;
            position.width -= 90;
            EditorGUI.PropertyField(position, objectRefProp, GUIContent.none);
            
            position.x += position.width;
            position.width = 90;

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if(typeProp.enumValueIndex is < 0 or >= 3)
                    typeProp.enumValueIndex = 0;
                var type = (AFSelection.SelectionType)typeProp.enumValueIndex;
                using (new AFStyles.EditorLabelWidth(1))
                {
                    type = (AFSelection.SelectionType)EditorGUI.Popup(
                        position,
                        typeProp.enumValueIndex,
                        typeProp.enumDisplayNames
                            .Select(label => new GUIContent(label, typeProp.tooltip)).ToArray(),
                        AFStyles.Popup);
                }

                if (check.changed)
                    typeProp.enumValueIndex = (int)type;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return AFStyles.Height + AFStyles.VerticalSpace;
        }
    }
}