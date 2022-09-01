using AnimFlex.Tweener;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomPropertyDrawer(typeof(MultiTweenerGenerator), true)]
    public class MultiTweenerGeneratorEditor : TweenerGeneratorEditor
    {
        protected override void DrawFrom(Rect position)
        {
            var fromProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.fromObject));
            
            var pos = new Rect(position);
            
            // pos.width = position.width - 80;
            // using (new AFStyles.EditorLabelWidth(80))
                EditorGUI.PropertyField(pos, fromProp, new GUIContent("For :", fromProp.tooltip));

            // null warning
            if (fromProp.isArray && fromProp.arraySize == 0 || !fromProp.isArray && fromProp.objectReferenceValue == null)
            {
                pos.x = position.x;
                pos.y += EditorGUI.GetPropertyHeight(fromProp) + AFStyles.VerticalSpace;
                pos.width = position.width;
                pos.height = AFStyles.BigHeight;
                AFStyles.DrawHelpBox(pos, "The \"From\" reference is empty!", MessageType.Warning);
            }
        }

        protected override float DrawTiming_Height()
        {
            return base.DrawTiming_Height();
        }

        protected override void DrawTiming(Rect position)
        {
            var durationProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.duration));
            var delayProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.delay));
            var multiDelayProp = property.FindPropertyRelative(nameof(MultiTweenerGeneratorPosition.multiDelay));
            var pingPongProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.pingPong));
            
            
            var pos = new Rect(position);
            pos.width = (position.width - 80) * 0.35f;
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