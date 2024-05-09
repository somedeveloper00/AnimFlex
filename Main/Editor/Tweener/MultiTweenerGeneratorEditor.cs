using AnimFlex.Tweening;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomPropertyDrawer(typeof(MultiTweenerGenerator), true)]
    public class MultiTweenerGeneratorEditor : TweenerGeneratorDrawer
    {
        private static GUIContent s_selectionGuiContent;
        private static GUIContent s_reverseGuiContent;

        protected override float DrawFrom_Height()
        {
            var selectionsProp = property.FindPropertyRelative(nameof(MultiTweenerGeneratorPosition.selections));
            var height = Mathf.Max(AFStyles.Height, EditorGUI.GetPropertyHeight(selectionsProp));
            height += AFStyles.Height + AFStyles.VerticalSpace;

            if (selectionsProp.arraySize == 0)
                height += AFStyles.Height + AFStyles.VerticalSpace;
            return height;
        }

        protected override void DrawFrom(Rect position)
        {
            var selectionsProp = property.FindPropertyRelative(nameof(MultiTweenerGeneratorPosition.selections));
            var reverseProp = property.FindPropertyRelative(nameof(MultiTweenerGeneratorPosition.reverseOrder));

            var pos = new Rect(position);

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                s_selectionGuiContent ??= new GUIContent("Select :", selectionsProp.tooltip);
                s_reverseGuiContent ??= new GUIContent("Reverse Selection : ", reverseProp.tooltip);

                EditorGUI.PropertyField(pos, selectionsProp, s_selectionGuiContent);
                pos.y += EditorGUI.GetPropertyHeight(selectionsProp) + AFStyles.VerticalSpace;
                EditorGUI.PropertyField(pos, reverseProp, s_reverseGuiContent);
            }

            // null warning
            if (selectionsProp.isArray && selectionsProp.arraySize == 0 ||
                !selectionsProp.isArray && selectionsProp.objectReferenceValue == null)
            {
                pos.x = position.x;
                pos.y += pos.height + AFStyles.VerticalSpace;
                pos.width = position.width;
                pos.height = AFStyles.BigHeight;
                AFStyles.DrawHelpBox(pos, "The \"From\" reference is empty!", MessageType.Warning);
            }
        }

        protected override void DrawTiming(Rect position)
        {
            var durationProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.duration));
            var delayProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.delay));
            var multiDelayProp = property.FindPropertyRelative(nameof(MultiTweenerGeneratorPosition.multiDelay));
            var pingPongProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.pingPong));

            var pos = new Rect(position)
            {
                width = (position.width - 80) * 0.35f
            };
            using (new AFStyles.EditorLabelWidth(80))
            using (new AFStyles.EditorFieldMinWidth(pos, 40))
                EditorGUI.PropertyField(pos, durationProp, new GUIContent("Duration :", durationProp.tooltip));

            pos.x += pos.width;
            pos.width = (position.width - 80) * 0.3f;
            using (new AFStyles.EditorLabelWidth(45))
            using (new AFStyles.EditorFieldMinWidth(pos, 40))
                EditorGUI.PropertyField(pos, delayProp, new GUIContent("Delay :", delayProp.tooltip));

            pos.x += pos.width;
            pos.width = (position.width - 80) * 0.35f;
            using (new AFStyles.EditorLabelWidth(80))
            using (new AFStyles.EditorFieldMinWidth(pos, 40))
                EditorGUI.PropertyField(pos, multiDelayProp, new GUIContent("Multi-Delay :", multiDelayProp.tooltip));


            pos.x += pos.width;
            pos.width = 80;
            AFStyles.DrawBooleanEnum(pos, "Ping-Pong", "Straight", pingPongProp);
        }
    }
}
