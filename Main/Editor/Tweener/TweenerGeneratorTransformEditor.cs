using AnimFlex.Tweening;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener {
    [CustomPropertyDrawer(typeof(TweenerGeneratorTransform))]
    public class TweenerGeneratorTransformEditor : TweenerGeneratorEditor {
        protected override void DrawValue(Rect position) {
            base.DrawValue( position );
            
            var positionProp = property.FindPropertyRelative( nameof(TweenerGeneratorTransform.position) );
            var rotationProp = property.FindPropertyRelative( nameof(TweenerGeneratorTransform.rotation) );
            var scaleProp = property.FindPropertyRelative( nameof(TweenerGeneratorTransform.scale) );
            
            position.y += AFStyles.Height + AFStyles.VerticalSpace;
            position.width /= 3f;
            using (new AFStyles.EditorLabelWidth( 60 )) {
                EditorGUI.PropertyField( position, positionProp );
                position.x += position.width;
                EditorGUI.PropertyField( position, rotationProp );
                position.x += position.width;
                EditorGUI.PropertyField( position, scaleProp );
            }
        }

        protected override float DrawValue_Height() {
            return base.DrawValue_Height() + AFStyles.Height + AFStyles.VerticalSpace;
        }
    }
}