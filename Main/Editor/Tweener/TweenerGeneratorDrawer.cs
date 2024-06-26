﻿using AnimFlex.Editor.Preview;
using AnimFlex.Sequencer;
using AnimFlex.Sequencer.Clips;
using AnimFlex.Tweening;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor.Tweener
{
    [CustomPropertyDrawer(typeof(TweenerGenerator), true)]
    public class TweenerGeneratorDrawer : PropertyDrawer
    {
        protected SerializedProperty property;
        protected TweenerGenerator target;
        protected int selectedEvent = 0;
        private static GUIContent[] s_eventSelectionGuiContents;
        private static GUIContent s_fromGuiContnet;
        private static readonly GUIContent s_selfBtnGuiContent = new("Self", "Sets the From Object to the game object this is attached to");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var styledGuiScope = new AFStyles.StyledGuiScope(false);

            this.property = property;
            target = (TweenerGenerator)property.GetValue();

            using (new AFStyles.GuiColor(Color.white))
            {
                EditorGUI.BeginProperty(position, label, property);
                position.height = AFStyles.Height;

                position.y += AFStyles.VerticalSpace;
                DrawPlayback(position);

                position.y += DrawPlayback_Height() + AFStyles.VerticalSpace;
                DrawFrom(position);

                position.y += DrawFrom_Height() + AFStyles.VerticalSpace;
                DrawValue(position);

                position.y += DrawValue_Height() + AFStyles.VerticalSpace;
                DrawCurve(position);

                position.y += DrawCurve_Height() + AFStyles.VerticalSpace;
                DrawTiming(position);

                position.y += DrawTiming_Height() + AFStyles.VerticalSpace;
                DrawLoop(position);

                position.y += DrawLoop_Height() + AFStyles.VerticalSpace;
                DrawUnityEvents(position);

                EditorGUI.EndProperty();
            }
        }

        protected virtual float DrawPlayback_Height() => SequenceAnimEditor.Current != null ? 0 : AFStyles.Height;

        protected virtual void DrawPlayback(Rect position)
        {
            if (SequenceAnimEditor.Current != null)
            {
                return;
            }
            var pos = new Rect(position) { width = 100 };
            pos.x += (position.width - pos.width) / 2f;

            using (new AFStyles.GuiForceActive(true))
            {
                if (GUI.Button(pos, AFPreviewUtils.IsActive ? "Stop" : "Play Tweener", AFStyles.Button))
                {
                    if (AFPreviewUtils.IsActive)
                        AFPreviewUtils.StopPreviewMode();
                    else
                        AFPreviewUtils.PreviewTweener(target);
                }
            }
        }

        protected virtual float DrawUnityEvents_Height()
        {
            var onStart = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.onStart));
            var onComplete = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.onComplete));
            var onKill = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.onKill));

            return !onStart.isExpanded ? 0 : selectedEvent switch
            {
                0 => EditorGUI.GetPropertyHeight(onStart, GUIContent.none),
                1 => EditorGUI.GetPropertyHeight(onComplete, GUIContent.none),
                2 => EditorGUI.GetPropertyHeight(onKill, GUIContent.none),
                _ => AFStyles.Height
            };
        }

        protected virtual void DrawUnityEvents(Rect position)
        {
            var pos = new Rect(position);
            var onStart = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.onStart));
            var onComplete = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.onComplete));
            var onKill = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.onKill));

            pos.x += 15;
            pos.width = 10;
            onStart.isExpanded = EditorGUI.Foldout(pos, onStart.isExpanded, GUIContent.none, true);
            pos.x += pos.width;
            pos.width = position.width - 10 - 15;

            s_eventSelectionGuiContents ??= new GUIContent[]
            {
                    new("On Start", onStart.tooltip),
                    new("On Complete", onComplete.tooltip),
                    new("On Kill", onKill.tooltip)
            };
            selectedEvent = GUI.Toolbar(pos, selectedEvent, s_eventSelectionGuiContents);

            if (onStart.isExpanded)
            {
                pos.y += AFStyles.Height + AFStyles.VerticalSpace;
                pos.width = position.width;
                pos.x = position.x;

                switch (selectedEvent)
                {
                    case 0: EditorGUI.PropertyField(pos, onStart, GUIContent.none); break;
                    case 1: EditorGUI.PropertyField(pos, onComplete, GUIContent.none); break;
                    case 2: EditorGUI.PropertyField(pos, onKill, GUIContent.none); break;
                }
            }
        }

        protected virtual float DrawLoop_Height() => AFStyles.Height;

        protected virtual void DrawLoop(Rect position)
        {
            var loopsProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.loops));
            var loopDelayProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.loopDelay));

            var pos = new Rect(position)
            {
                width = (position.width - 80) / 2
            };
            using (new AFStyles.EditorLabelWidth(80))
                EditorGUI.PropertyField(pos, loopsProp, new GUIContent("Loop :", loopsProp.tooltip));

            using (new EditorGUI.DisabledGroupScope(loopsProp.intValue == 0))
            {
                pos.x += pos.width;
                using (new AFStyles.EditorLabelWidth(80))
                    EditorGUI.PropertyField(pos, loopDelayProp, new GUIContent("LoopDelay: ", loopDelayProp.tooltip));
            }


            pos.x += pos.width;
            pos.width = 80;
            loopsProp.intValue = Mathf.Max(loopsProp.intValue, -1);

            if (AFStyles.DrawBooleanEnum(pos, "Infinite", "Finite",
                   loopsProp.intValue < 0,
                   "Infinite loop means the tween will never end " +
                   ", while in Finite loop, you can specify a limited number of times to repeat the tween", out var result))
            {
                if (result == false) loopsProp.intValue = 0;
                else loopsProp.intValue = -1;
            }
        }

        protected virtual float DrawTiming_Height() => AFStyles.Height;

        protected virtual void DrawTiming(Rect position)
        {
            var durationProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.duration));
            var delayProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.delay));
            var pingPongProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.pingPong));


            var pos = new Rect(position)
            {
                width = (position.width - 80) / 2
            };

            using (new AFStyles.EditorLabelWidth(80))
                EditorGUI.PropertyField(pos, durationProp, new GUIContent("Duration :", durationProp.tooltip));

            pos.x += pos.width;
            using (new AFStyles.EditorLabelWidth(80))
                EditorGUI.PropertyField(pos, delayProp, new GUIContent("Delay :", delayProp.tooltip));

            pos.x += pos.width;
            pos.width = 80;
            AFStyles.DrawBooleanEnum(pos, "Ping-Pong", "Straight", pingPongProp);
        }

        protected virtual float DrawCurve_Height() => AFStyles.Height;

        protected virtual void DrawCurve(Rect position)
        {
            var easeProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.ease));
            var useCustom = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.useCurve));
            var customCurve = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.customCurve));

            var pos = new Rect(position)
            {
                height = AFStyles.Height,

                width = position.width - 80
            };

            using (new AFStyles.EditorLabelWidth(80))
                if (useCustom.boolValue)
                {
                    EditorGUI.PropertyField(pos, customCurve, new GUIContent("Ease :", easeProp.tooltip));
                }
                else
                {
                    var ease = (Ease)easeProp.enumValueIndex;
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        ease = (Ease)EditorGUI.EnumPopup(pos,
                            new GUIContent("Ease :", easeProp.tooltip), ease,
                            AFStyles.Popup);
                        if (check.changed)
                            easeProp.enumValueIndex = (int)ease;
                    }
                }

            pos.x += pos.width;
            pos.width = 80;
            AFStyles.DrawBooleanEnum(pos, "Custom", "Built-in", useCustom);

        }

        protected virtual float DrawValue_Height()
        {
            var targetProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.target));
            return Mathf.Max(AFStyles.Height, EditorGUI.GetPropertyHeight(targetProp));
        }

        protected virtual void DrawValue(Rect position)
        {
            var fromProp = property.FindPropertyRelative(nameof(TweenerGenerator.@from));
            var targetProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.target));
            var relativeProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.relative));

            var pos = new Rect(position)
            {
                height = AFStyles.Height,

                width = 80
            };
            AFStyles.DrawBooleanEnum(pos, "From", "To", fromProp);

            pos.x += pos.width;
            pos.width = position.width - 80 - 80;
            if (SequenceAnimEditor.Current != null && ClipNodeDrawer.Current != null)
            {
                // draw variable fetch from clip node (parent)
                // get clip node
                string parentPropertyPath = AFEditorUtils.GetParentPropertyPath(property.propertyPath);
                var clipNodeProp = property.serializedObject.FindProperty(parentPropertyPath);
                if (clipNodeProp == null)
                {
                    // draw normal value
                    EditorGUI.PropertyField(pos, targetProp, GUIContent.none);
                }
                else
                {
                    var varProp = clipNodeProp.FindPropertyRelative(nameof(CTweenerPosition.target));
                    pos.x += 5;
                    pos.width -= 5 * 2;
                    using (new AFStyles.EditorLabelWidth(40))
                        EditorGUI.PropertyField(pos, varProp, GUIContent.none);
                    pos.x -= 5;
                    pos.width += 5 * 2;
                }
            }
            else
            {
                // draw normal value
                EditorGUI.PropertyField(pos, targetProp, GUIContent.none);
            }

            pos.x += pos.width;
            pos.width = 80;
            AFStyles.DrawBooleanEnum(pos, "Relative", "Absolute", relativeProp);
        }

        protected virtual float DrawFrom_Height()
        {
            var fromProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.fromObject));
            var height = Mathf.Max(AFStyles.Height, EditorGUI.GetPropertyHeight(fromProp));
            return height;
        }

        protected virtual void DrawFrom(Rect position)
        {
            var fromProp = property.FindPropertyRelative(nameof(TweenerGeneratorPosition.fromObject));
            s_fromGuiContnet ??= new GUIContent("For :", fromProp.tooltip);
            var pos = new Rect(position)
            {
                width = position.width - 80
            };

            using (new AFStyles.EditorLabelWidth(80))
            {
                if (SequenceAnimEditor.Current != null && ClipNodeDrawer.Current != null)
                {
                    // draw variable fetch from clip node (parent)
                    // get clip node
                    string parentPropertyPath = AFEditorUtils.GetParentPropertyPath(property.propertyPath);
                    var clipNodeProp = property.serializedObject.FindProperty(parentPropertyPath);
                    if (clipNodeProp == null)
                    {
                        // draw normal value
                        EditorGUI.PropertyField(pos, fromProp, s_fromGuiContnet);
                    }
                    else
                    {
                        var prop = clipNodeProp.FindPropertyRelative(nameof(CTweenerPosition.from));
                        pos.x += 5;
                        pos.width -= 5 * 2;
                        using (new AFStyles.EditorLabelWidth(40))
                            EditorGUI.PropertyField(pos, prop, s_fromGuiContnet);
                        pos.x -= 5;
                        pos.width += 5 * 2;
                    }
                }
                else
                {
                    // draw normal value
                    EditorGUI.PropertyField(pos, fromProp, s_fromGuiContnet);
                }
            }

            pos.x += pos.width;
            pos.width = 80;
            if (GUI.Button(pos, s_selfBtnGuiContent, AFStyles.Button))
            {
                var obj = ((Component)property.serializedObject.targetObject).GetComponent(target.GetFromValueType());
                if (SequenceAnimEditor.Current == null)
                {
                    fromProp.objectReferenceValue = obj;
                }
                else
                {
                    var clipNodeProp = property.serializedObject.FindProperty(AFEditorUtils.GetParentPropertyPath(property.propertyPath));
                    if (clipNodeProp != null)
                    {
                        clipNodeProp
                            .FindPropertyRelative(nameof(CTweenerPosition.from))
                            .FindPropertyRelative(nameof(VariableFetch<int>.value)).objectReferenceValue = obj;
                        clipNodeProp
                            .FindPropertyRelative(nameof(CTweenerPosition.from))
                            .FindPropertyRelative(nameof(VariableFetch<int>._index)).intValue = 0;
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            this.property = property;
            float height = AFStyles.Height + AFStyles.VerticalSpace;

            height += DrawPlayback_Height() + AFStyles.VerticalSpace;
            height += DrawFrom_Height() + AFStyles.VerticalSpace;
            height += DrawValue_Height() + AFStyles.VerticalSpace;
            height += DrawCurve_Height() + AFStyles.VerticalSpace;
            height += DrawTiming_Height() + AFStyles.VerticalSpace;
            height += DrawLoop_Height() + AFStyles.VerticalSpace;
            height += DrawUnityEvents_Height() + AFStyles.VerticalSpace;

            return height;
        }
    }
}
