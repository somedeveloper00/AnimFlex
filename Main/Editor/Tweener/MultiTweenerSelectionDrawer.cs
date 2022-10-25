using System.Linq;
using AnimFlex.Tweening;
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

            var objectRefProp = property.FindPropertyRelative(nameof(AFSelection.transform));
            var typeProp = property.FindPropertyRelative(nameof(AFSelection.type));

            var pos = new Rect(position);

            pos.height = AFStyles.Height;
            pos.width -= 120;
            EditorGUI.PropertyField(pos, objectRefProp, GUIContent.none);

            pos.x += pos.width;
            pos.width = 120;

            if(typeProp.enumValueIndex < 0 || typeProp.enumValueIndex >= 4)
                typeProp.enumValueIndex = 0;
            var type = (AFSelection.SelectionType)typeProp.enumValueIndex;

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                using (new AFStyles.EditorLabelWidth(1))
                {
                    type = (AFSelection.SelectionType)EditorGUI.Popup(
                        pos,
                        typeProp.enumValueIndex,
                        typeProp.enumDisplayNames
                            .Select(label => new GUIContent(label, typeProp.tooltip)).ToArray(),
                        AFStyles.Popup);
                }

                if (check.changed)
                    typeProp.enumValueIndex = (int)type;
            }

            // error check
            pos.x = position.x;
            pos.width = position.width;
            pos.y += AFStyles.Height + AFStyles.VerticalSpace;

            if (property.GetValue() is AFSelection target)
            {
                var targetType = target.GetValueType();

                if (objectRefProp.objectReferenceValue == null)
                {
                    AFStyles.DrawHelpBox(pos, "Reference field is empty", MessageType.Warning);
                }
                else if (type == AFSelection.SelectionType.Direct && objectRefProp.objectReferenceValue.GetType() != targetType)
                {
                    AFStyles.DrawHelpBox(pos, "Reference type is not correct!", MessageType.Error);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = AFStyles.Height + AFStyles.VerticalSpace;

            var objectRefProp = property.FindPropertyRelative(nameof(AFSelection.transform));
            var typeProp = property.FindPropertyRelative(nameof(AFSelection.type));

            if (property.GetValue() is AFSelection target)
            {
                var targetType = target.GetValueType();
                var type = (AFSelection.SelectionType)typeProp.enumValueIndex;

                if (objectRefProp.objectReferenceValue == null)
                {
                    height += AFStyles.Height + AFStyles.VerticalSpace;
                }
                else if (type == AFSelection.SelectionType.Direct &&
                         objectRefProp.objectReferenceValue.GetType() != targetType)
                {
                    height += AFStyles.Height + AFStyles.VerticalSpace;
                }
            }

            return height;
        }
    }
}
