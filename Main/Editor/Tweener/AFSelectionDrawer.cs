using System;
using System.Linq;
using AnimFlex.Tweening;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomPropertyDrawer(typeof(AFSelection), true)]
    public class AfSelectionDrawer : PropertyDrawer
    {
        GUIContent[] _displayedOptions = null;
        static readonly int[] _optionValues = Enum.GetValues( typeof(AFSelection.SelectionType) ).Cast<int>().ToArray();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            using (new EditorGUI.PropertyScope( position, label, property )) {
                var objectRefProp = property.FindPropertyRelative( nameof(AFSelection.transform) );
                var typeProp = property.FindPropertyRelative( nameof(AFSelection.type) );
                
                _displayedOptions ??= Enum.GetNames( typeof(AFSelection.SelectionType) )
                    .Select( n => new GUIContent( n, typeProp.tooltip ) ).ToArray();

                var pos = new Rect( position );

                pos.height = AFStyles.Height;
                pos.width -= 120;
                EditorGUI.PropertyField( pos, objectRefProp, GUIContent.none );

                pos.x += pos.width;
                pos.width = 120;

                EditorGUI.IntPopup( pos, typeProp, _displayedOptions, _optionValues, GUIContent.none );
                
                // error check
                pos.x = position.x;
                pos.width = position.width;
                pos.y += AFStyles.Height + AFStyles.VerticalSpace;

                var type = (AFSelection.SelectionType)typeProp.enumValueIndex;
                if (property.GetValue() is AFSelection target) {
                    var targetType = target.GetValueType();

                    if (objectRefProp.objectReferenceValue == null) {
                        AFStyles.DrawHelpBox( pos, "Reference field is empty", MessageType.Warning );
                    }
                    else if (type == AFSelection.SelectionType.Direct && 
                             target.transform && !target.transform.GetComponent( targetType )) {
                        AFStyles.DrawHelpBox( pos, "Reference type is not correct!", MessageType.Error );
                    }
                }
            }
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
                         target.transform && !target.transform.GetComponent( targetType ))
                {
                    height += AFStyles.Height + AFStyles.VerticalSpace;
                }
            }

            return height;
        }
    }
}
