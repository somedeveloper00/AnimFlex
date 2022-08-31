using System;
using AnimFlex.Tweener;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomPropertyDrawer(typeof(TweenerGenerator), true)]
    public class TweenerGeneratorEditor : PropertyDrawer
    {
        private SerializedProperty property;
        private TweenerGenerator target;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var enteredEditorStyles = new AFStyles.CenteredEditorStyles();
            this.property = property;
            target = (TweenerGenerator)property.GetValue();

            using (new AFStyles.GuiColor(Color.white))
            {
                EditorGUI.BeginProperty(position, label, property);
                position.height = AFStyles.Height;
                
                var height = DrawFrom(position);
                
                position.y += height + AFStyles.VerticalSpace;
                height = DrawValue(position);
                
                position.y += height + AFStyles.VerticalSpace;
                DrawCurve(position);
                
                position.y += height + AFStyles.VerticalSpace;
                DrawTiming(position);

                position.y += height + AFStyles.VerticalSpace;
                DrawLoop(position);
                
                EditorGUI.EndProperty();
            }
        }

        private void DrawLoop(Rect position)
        {
            var loopsProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.loops));
            var loopDelayProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.loopDelay));

            var pos = new Rect(position);

            pos.width = 80;
            GUI.Label(pos, new GUIContent("Loop :", loopsProp.tooltip), AFStyles.SpecialLabel);

            pos.x += pos.width;
            pos.width = (position.width - 80) / 2 - 80;
            using (new AFStyles.EditorLabelWidth())
                EditorGUI.PropertyField(pos.PadX(5), loopsProp, GUIContent.none);

            using (new EditorGUI.DisabledGroupScope(loopsProp.intValue != 0))
            {
                pos.x += pos.width;
                pos.width = 80;
                GUI.Label(pos, new GUIContent("Loop-Delay", loopsProp.tooltip), AFStyles.SpecialLabel);
            }

            pos.x += pos.width;
            pos.width = (position.width - 80) / 2 - 80;
            using (new AFStyles.EditorLabelWidth())
                EditorGUI.PropertyField(pos.PadX(5), loopDelayProp, GUIContent.none);

            pos.x += pos.width;
            pos.width = 80;
            loopsProp.intValue = Mathf.Max(loopsProp.intValue, -1);
            if (GUI.Button(pos, new GUIContent(loopsProp.intValue < 0 ? "Infinite" : "Finite",
                        "Infinite loop means the tween will never end " +
                        ", while in Finite loop, you can specify a limited number of times to repeat the tween"),
                    AFStyles.YellowButton))
            {
                if (loopsProp.intValue < 0) loopsProp.intValue = loopsProp.intValue = 0;
                else loopsProp.intValue = -1;
            }
        } 

        private void DrawTiming(Rect position)
        {
            var durationProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.duration));
            var delayProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.delay));
            var pingPongProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.pingPong));
            
            
            var pos = new Rect(position);
            pos.width = 80;
            GUI.Label(pos, new GUIContent("Duration :" , durationProp.tooltip), AFStyles.SpecialLabel);

            pos.x += pos.width;
            pos.width = (position.width - 80) / 2 - 80;
            using (new AFStyles.EditorLabelWidth())
                EditorGUI.PropertyField(pos.PadX(5), durationProp, new GUIContent("   "));

            pos.x += pos.width;
            pos.width = 80;
            GUI.Label(pos, new GUIContent("Delay :", delayProp.tooltip), AFStyles.SpecialLabel);

            pos.x += pos.width;
            pos.width = (position.width - 80) / 2 - 80;
            using (new AFStyles.EditorLabelWidth())
                EditorGUI.PropertyField(pos.PadX(5), delayProp, new GUIContent("   "));

            pos.x += pos.width;
            pos.width = 80;
            if (GUI.Button(pos, new GUIContent(pingPongProp.boolValue ? "Ping-Pong" : "Straight", pingPongProp.tooltip),
                    AFStyles.YellowButton))
            {
                pingPongProp.boolValue = !pingPongProp.boolValue;
            }
        }

        protected virtual void DrawCurve(Rect position)
        {
            var easeProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.ease));
            var useCustom = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.useCurve));
            var customCurve = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.customCurve));
            
            var pos = new Rect(position);
            pos.height = AFStyles.Height;

            pos.width = 80;
            GUI.Label(pos, new GUIContent("Ease :", easeProp.tooltip), AFStyles.SpecialLabel);

            pos.x += pos.width;
            pos.width = position.width - 80 - 80;
            if (useCustom.boolValue)
            {
                EditorGUI.PropertyField(pos.PadX(5), customCurve, GUIContent.none);
            }
            else
            {
                var ease = (Ease)easeProp.enumValueIndex;
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    ease = (Ease)EditorGUI.EnumPopup(pos.PadX(5), ease, AFStyles.Popup);
                    if (check.changed)
                        easeProp.enumValueIndex = (int)ease;
                }
            }

            pos.x += pos.width;
            pos.width = 80;
            if (GUI.Button(pos, new GUIContent(useCustom.boolValue ? "Custom" : "Built-in", useCustom.tooltip),
                    AFStyles.YellowButton))
            {
                useCustom.boolValue = !useCustom.boolValue;
            }

        }

        protected virtual float DrawValue(Rect position)
        {
            var fromProp = property.FindPropertyRelative(nameof(TweenerGenerator.@from));
            var targetProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.target));
            var relativeProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.relative));
                
            float height = 0;
            var pos = new Rect(position);
            pos.height = AFStyles.Height;

            pos.width = 80;
            if (GUI.Button(pos, new GUIContent(fromProp.boolValue ? "From" : "To", fromProp.tooltip),
                    AFStyles.YellowButton))
            {
                fromProp.boolValue = !fromProp.boolValue;
            }

            pos.x += pos.width;
            pos.width = position.width - 80 - 80;
            EditorGUI.PropertyField(pos.PadX(5), targetProp, GUIContent.none);
            height += EditorGUI.GetPropertyHeight(targetProp);

            pos.x += pos.width;
            pos.width = 80;
            if (GUI.Button(pos, new GUIContent(relativeProp.boolValue ? "Relative" : "Absolute", relativeProp.tooltip),
                    AFStyles.YellowButton))
            {
                relativeProp.boolValue = !relativeProp.boolValue;
            }
            return height;
        }

        protected virtual float DrawFrom(Rect position)
        {
            float height = 0;
            
            var fromProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.fromObject));
            
            var pos = new Rect(position);
            
            pos.width = 80;
            GUI.Label(pos, new GUIContent("For :", fromProp.tooltip), AFStyles.SpecialLabel);
            
            pos.x += pos.width;
            pos.width = position.width - 80 - 80;
            EditorGUI.PropertyField(pos.PadX(5), fromProp, GUIContent.none);
            height += Mathf.Max(AFStyles.Height, EditorGUI.GetPropertyHeight(fromProp));

            pos.x += pos.width;
            pos.width = 80;
            if (GUI.Button(pos, 
                    new GUIContent("Self", "Sets the From Object to the game object this is attached to"),
                    AFStyles.Button))
            {
                fromProp.objectReferenceValue =
                    ((Component)property.serializedObject.targetObject)
                    .GetComponent(target.GetFromValueType());
            }

            // null warning
            if (fromProp.objectReferenceValue == null)
            {
                pos.x = position.x;
                pos.width = position.width;
                pos.y += height;
                AFStyles.DrawHelpBox(pos, "The \"From\" reference is empty!", MessageType.Warning);
                height += AFStyles.Height;
            }
                

            return height;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var fromProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.fromObject));
            var targetProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.target));

            var oneLine = AFStyles.Height + AFStyles.VerticalSpace;
            float height = 0;
            height += EditorGUI.GetPropertyHeight(fromProp) + AFStyles.VerticalSpace;
            if (fromProp.objectReferenceValue == null) height += oneLine;
            height += EditorGUI.GetPropertyHeight(targetProp) + AFStyles.VerticalSpace;
            height += oneLine;
            height += oneLine;
            height += oneLine;

            return height;
        }
    }
}