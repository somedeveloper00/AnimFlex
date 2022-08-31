using System;
using AnimFlex.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using PopupWindow = UnityEditor.PopupWindow;

namespace AnimFlex.Tweener.Editor
{
        [CustomPropertyDrawer(typeof(GeneratorData))]
    public class GeneratorDataEditor : PropertyDrawer
    {
        private const int MIN_WIDTH = 300;
        
        private Object targetObject;
        private SerializedProperty _property;
        
        private SerializedProperty _fromObjectProp;
        private SerializedProperty _tweenerTypeProp;
        private SerializedProperty _relativeProp;
        private SerializedProperty _fromProp;
        private SerializedProperty _targetVector3Prop;
        private SerializedProperty _targetQuaternionProp;
        private SerializedProperty _targetColorProp;
        private SerializedProperty _targetFloatProp;
        private SerializedProperty _targetTransformProp;
        
        private SerializedProperty _customCurveProp;
        private SerializedProperty _easeProp;
        private SerializedProperty _useCustomCurveProp;
        
        private SerializedProperty _useQuaternionProp;
        private SerializedProperty _useTargetTransformProp;
        
        private SerializedProperty _durationProp;
        private SerializedProperty _delayProp;
        private SerializedProperty _loopProp;
        private SerializedProperty _loopDelayProp;
        private SerializedProperty _pingPongProp;
        
        private SerializedProperty _onStartProp;
        private SerializedProperty _onUpdateProp;
        private SerializedProperty _onCompleteProp;
        private SerializedProperty _onKillProp;

        private int selectedEventIndex = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var enteredEditorStyles = new AFStyles.CenteredEditorStyles();
            using var guiBackgroundColor = new AFStyles.GuiBackgroundColor(Color.white);
            using var guiColor = new AFStyles.GuiColor(Color.white);
            
            targetObject = property.serializedObject.targetObject;
            
            SaveProperties(property);

            EditorGUI.BeginProperty(position, label, property);
            position.width = Mathf.Max(position.width, MIN_WIDTH);
            position.height = AFStyles.Height;

            Rect linePos;
            DrawFromObject(position);

            position.y += position.height + AFStyles.VerticalSpace;

            DrawTweenerType(position);

            position.y += position.height + AFStyles.VerticalSpace;
            var height = DrawValue(position);

            position.y += height + AFStyles.VerticalSpace;
            DrawCurve(position);

            position.y += position.height + AFStyles.VerticalSpace;
            DrawTimings(position);

            position.y += position.height + AFStyles.VerticalSpace;
            DrawLoop(position);

            position.y += position.height + AFStyles.VerticalSpace;
            position.y += 20;
            height = DrawEvents(position);

            position.y += height + AFStyles.VerticalSpace;
            DrawPlayback(position);

            EditorGUI.EndProperty();
        }

        private void DrawLoop(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            GUI.Label(linePos, new GUIContent("Loop :", _loopProp.tooltip), AFStyles.SpecialLabel);
            linePos.x += linePos.width;

            linePos.width = (position.width - 80 - 80 - 80) / 2;
            using (new AFStyles.EditorLabelWidth())
                _loopProp.intValue = EditorGUI.IntField(linePos, new GUIContent("    "), _loopProp.intValue);
            _loopProp.intValue = Mathf.Max(0, _loopProp.intValue);
            linePos.x += linePos.width;

            using (new EditorGUI.DisabledGroupScope(_loopProp.intValue <= 0))
            {
                linePos.x += 5;
                linePos.width = 80 - 5;
                GUI.Label(linePos, new GUIContent("Loop-delay :", _loopDelayProp.tooltip), AFStyles.SpecialLabel);
                linePos.x += linePos.width;
                linePos.width = (position.width - 80 - 80 - 80) / 2;
                using (new AFStyles.EditorLabelWidth())
                    _loopDelayProp.floatValue = EditorGUI.FloatField(linePos, new GUIContent("    "), _loopDelayProp.floatValue);
            }
            

            linePos.x += linePos.width;
            linePos.width = 80;
            if (GUI.Button(linePos, new GUIContent(_pingPongProp.boolValue ? "Ping Pong" : "Straight", _pingPongProp.tooltip), AFStyles.YellowButton))
            {
                _pingPongProp.boolValue = !_pingPongProp.boolValue;
            }
        }

        private void DrawTimings(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            GUI.Label(linePos, new GUIContent("Duration :", _durationProp.tooltip), AFStyles.SpecialLabel);

            linePos.x += 80;
            linePos.width = position.width / 2f - 80;
            using (new AFStyles.EditorLabelWidth())
                _durationProp.floatValue = EditorGUI.FloatField(linePos, new GUIContent("   "), _durationProp.floatValue);

            linePos.x += linePos.width;
            linePos.width = 80;
            GUI.Label(linePos, new GUIContent("Delay :", _delayProp.tooltip), AFStyles.SpecialLabel);
            
            linePos.x += 80;
            linePos.width = position.width / 2f - 80;
            using (new AFStyles.EditorLabelWidth())
                _delayProp.floatValue = EditorGUI.FloatField(linePos, new GUIContent("   "), _delayProp.floatValue);
        }

        private float DrawEvents(Rect position)
        {
            var linePos = new Rect(position);

            selectedEventIndex = GUI.Toolbar(linePos,
                selectedEventIndex,
                new[]
                {
                    new GUIContent("On Start", _onStartProp.tooltip),
                    new GUIContent("On Update", _onUpdateProp.tooltip),
                    new GUIContent("On Complete", _onCompleteProp.tooltip),
                    new GUIContent("On Kill", _onKillProp.tooltip)
                },
                AFStyles.Button);
            
            linePos.y += AFStyles.Height;
            
            switch (selectedEventIndex)
            {
                case 0:
                    EditorGUI.PropertyField(linePos, _onStartProp, GUIContent.none);
                    return AFStyles.Height + EditorGUI.GetPropertyHeight(_onStartProp);
                case 1:
                    EditorGUI.PropertyField(linePos, _onUpdateProp, GUIContent.none);
                    return AFStyles.Height + EditorGUI.GetPropertyHeight(_onUpdateProp);
                case 2:
                    EditorGUI.PropertyField(linePos, _onCompleteProp, GUIContent.none);
                    return AFStyles.Height + EditorGUI.GetPropertyHeight(_onCompleteProp);
                case 3:
                    EditorGUI.PropertyField(linePos, _onKillProp, GUIContent.none);
                    return AFStyles.Height + EditorGUI.GetPropertyHeight(_onKillProp);
            }

            return AFStyles.Height;
        }

        private void DrawCurve(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            GUI.Label(linePos, new GUIContent("Curve :", _easeProp.tooltip), AFStyles.SpecialLabel);

            linePos.x += linePos.width;
            linePos.width = position.width - 80 - 80;

            if (_useCustomCurveProp.boolValue)
            {
                EditorGUI.PropertyField(linePos, _customCurveProp, GUIContent.none);
            }
            else
            {
                var ease = (Ease)_easeProp.enumValueIndex;
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    ease = (Ease)EditorGUI.EnumPopup(linePos, GUIContent.none, ease, AFStyles.Popup);
                    if (check.changed)
                        _easeProp.enumValueIndex = (int)ease;
                }
            }

            linePos.x += linePos.width;
            linePos.width = 80;

            if (GUI.Button(linePos, new GUIContent(_useCustomCurveProp.boolValue ? "Cutsom" : "Prebuilt", _useCustomCurveProp.tooltip), AFStyles.YellowButton))
            {
                _useCustomCurveProp.boolValue = !_useCustomCurveProp.boolValue;
            }

        }

        private void DrawPlayback(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            linePos.x += position.width / 2 - 40;

            using (new EditorGUI.DisabledGroupScope(PreviewUtils.isActive))
            {
                using (new AFStyles.GuiForceActive())
                {
                    if (GUI.Button(linePos, PreviewUtils.isActive ? "Stop" : "Play", AFStyles.Button))
                    {
                        if(PreviewUtils.isActive)
                            PreviewUtils.StopPreviewMode();
                        else
                            PreviewUtils.PreviewTweener((GeneratorData)_property.GetValue());
                    }
                }
            }
        }

        private float DrawValue(Rect position)
        {
            var linePos = new Rect(position);

            float height = position.height;
            linePos.width = 80;
            if (GUI.Button(linePos, new GUIContent(_fromProp.boolValue ? "From :" : "To :", _fromProp.tooltip), AFStyles.YellowButton))
            {
                _fromProp.boolValue = !_fromProp.boolValue;
            }
            linePos.x += linePos.width;
            
            var enumValue = (GeneratorData.TweenerType)_tweenerTypeProp.enumValueIndex;
            
            // quaternion option
            switch (enumValue)
            {
                case GeneratorData.TweenerType.Position:
                    linePos.width = position.width - 80 - 80;
                    EditorGUI.PropertyField(linePos, _useTargetTransformProp.boolValue ? _targetTransformProp : _targetVector3Prop, GUIContent.none);
                    linePos.x += linePos.width;
                    
                    linePos.width = 80;
                    if (GUI.Button(linePos, _useTargetTransformProp.boolValue ? "Transform" : "Vector3", AFStyles.YellowButton))
                    {
                        _useTargetTransformProp.boolValue = !_useTargetTransformProp.boolValue;
                    }
                    break;
                
                case GeneratorData.TweenerType.LocalPosition:
                case GeneratorData.TweenerType.Scale:
                    linePos.width = position.width - 80;
                    EditorGUI.PropertyField(linePos, _targetVector3Prop, GUIContent.none);
                    break;
                
                case GeneratorData.TweenerType.LocalRotation:
                case GeneratorData.TweenerType.Rotation:
                    var useQuaternion = _useQuaternionProp.boolValue;
                    
                    linePos.width = position.width - 80 - 80;
                    EditorGUI.PropertyField(linePos, useQuaternion ? _targetQuaternionProp : _targetVector3Prop, GUIContent.none);
                    linePos.x += linePos.width;
                    
                    linePos.width = 80;
                    if (GUI.Button(linePos, useQuaternion ? "Quaternion" : "Vector3", AFStyles.YellowButton))
                    {
                        _useQuaternionProp.boolValue = !useQuaternion;
                    }

                    if (useQuaternion && 
                        TweenerPrefs.Instance.showQuaternionWarnings)
                    {
                        // warning
                        linePos = position;
                        linePos.y += linePos.height + AFStyles.VerticalSpace; 
                        
                        var message = "You're using Quaternions! " +
                            "it's highly recommended to use Vector3 instead.\n" +
                            "(to not see this message, change it in Preferences.)";
                        linePos.height = AFStyles.VerticalSpace * 2 + AFStyles.SpecialLabel.CalcHeight(new GUIContent(message), position.width);
                        height += linePos.height;
                        AFStyles.DrawHelpBox(linePos, message, MessageType.Warning);
                    }
                    break;
                
                case GeneratorData.TweenerType.Fade:
                    linePos.width = position.width - 80;
                    using (new AFStyles.EditorLabelWidth())
                        EditorGUI.PropertyField(linePos, _targetFloatProp, new GUIContent("    "));
                    break;
                
                case GeneratorData.TweenerType.Color:
                    linePos.width = position.width - 80;
                    EditorGUI.PropertyField(linePos, _targetColorProp, GUIContent.none);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return height;
        }

        private void DrawTweenerType(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            GUI.Label(linePos, new GUIContent("Type :", _tweenerTypeProp.tooltip), AFStyles.SpecialLabel);
            linePos.x += linePos.width;
            
            var enumValue = (GeneratorData.TweenerType)_tweenerTypeProp.enumValueIndex;
            switch (enumValue)
            {
                case GeneratorData.TweenerType.LocalPosition:
                case GeneratorData.TweenerType.LocalRotation:
                case GeneratorData.TweenerType.Rotation:
                case GeneratorData.TweenerType.Scale:
                case GeneratorData.TweenerType.Fade:
                case GeneratorData.TweenerType.Color:
                    
                    linePos.width = position.width - 80 - 150;
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        enumValue = (GeneratorData.TweenerType)EditorGUI.EnumPopup(linePos, enumValue, AFStyles.Popup);
                        if (check.changed)
                            _tweenerTypeProp.enumValueIndex = (int)enumValue;
                    }
                    linePos.x += 20;
                    linePos.x += linePos.width;
                    linePos.width = 80;
                    GUI.Label(linePos, new GUIContent("Relative", _relativeProp.tooltip), AFStyles.SpecialLabel);
                    linePos.x += linePos.width;
                    linePos.width = 50;
                    EditorGUI.PropertyField(linePos, _relativeProp, GUIContent.none);
                    break;
                
                case GeneratorData.TweenerType.Position:
                    
                    linePos.width = position.width - 80 - 150;
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        enumValue = (GeneratorData.TweenerType)EditorGUI.EnumPopup(linePos, enumValue, AFStyles.Popup);
                        if (check.changed)
                            _tweenerTypeProp.enumValueIndex = (int)enumValue;
                    }
                    linePos.x += 20;
                    linePos.x += linePos.width;
                    if (_useTargetTransformProp.boolValue == false)
                    {
                        linePos.width = 80;
                        GUI.Label(linePos, new GUIContent("Relative", _relativeProp.tooltip), AFStyles.SpecialLabel);
                        linePos.x += linePos.width;
                        linePos.width = 50;
                        EditorGUI.PropertyField(linePos, _relativeProp, GUIContent.none);
                    }
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawFromObject(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            GUI.Label(linePos, new GUIContent("From :", _fromObjectProp.tooltip), AFStyles.SpecialLabel);

            linePos.x += linePos.width;
            linePos.width = position.width - 80 - 80;
            EditorGUI.PropertyField(linePos, _fromObjectProp, GUIContent.none);

            linePos.x += linePos.width;
            linePos.width = 80;
            if (GUI.Button(linePos, new GUIContent("self", "Sets the From Object to the game object this is attached to"), AFStyles.Button))
            {
                _fromObjectProp.objectReferenceValue = (targetObject as Component)?.gameObject;
            }
        }

        private void RecordUndo()
        {
            Undo.RecordObject(targetObject, "tweener modified");
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SaveProperties(property);
            
            var singleLine = AFStyles.Height + AFStyles.VerticalSpace;
            float height = 0;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            
            var enumValue = (GeneratorData.TweenerType)_tweenerTypeProp.enumValueIndex;
            if ((enumValue.HasFlag(GeneratorData.TweenerType.Rotation) || enumValue.HasFlag(GeneratorData.TweenerType.LocalRotation)) &&
                _useQuaternionProp.boolValue && TweenerPrefs.Instance.showQuaternionWarnings)
            {
                height += AFStyles.VerticalSpace * 2 + singleLine * 2;
            }
                
            height += 20;

            switch (selectedEventIndex)
            {
                case 0:
                    height += EditorGUI.GetPropertyHeight(_onStartProp);
                    break;
                case 1:
                    height += EditorGUI.GetPropertyHeight(_onUpdateProp);
                    break;
                case 2:
                    height += EditorGUI.GetPropertyHeight(_onCompleteProp);
                    break;
                case 3:
                    height += EditorGUI.GetPropertyHeight(_onKillProp);
                    break;
            }
            
            height += singleLine;


            return height;
        }
        
        private void SaveProperties(SerializedProperty property)
        {
            _property = property;
            _fromObjectProp = property.FindPropertyRelative(nameof(GeneratorData.fromObject));
            _tweenerTypeProp = property.FindPropertyRelative(nameof(GeneratorData.tweenerType));
            _relativeProp = property.FindPropertyRelative(nameof(GeneratorData.relative));
            _fromProp = property.FindPropertyRelative(nameof(GeneratorData.@from));
            _targetVector3Prop = property.FindPropertyRelative(nameof(GeneratorData.targetVector3));
            _targetQuaternionProp = property.FindPropertyRelative(nameof(GeneratorData.targetQuaternion));
            _targetColorProp = property.FindPropertyRelative(nameof(GeneratorData.targetColor));
            _targetFloatProp = property.FindPropertyRelative(nameof(GeneratorData.targetFloat));
            _targetTransformProp = property.FindPropertyRelative(nameof(GeneratorData.targetTransform));
            _customCurveProp = property.FindPropertyRelative(nameof(GeneratorData.customCurve));
            _easeProp = property.FindPropertyRelative(nameof(GeneratorData.ease));
            _useCustomCurveProp = property.FindPropertyRelative(nameof(GeneratorData.useCurve));
            _useQuaternionProp = property.FindPropertyRelative(nameof(GeneratorData.useQuaternion));
            _useTargetTransformProp = property.FindPropertyRelative(nameof(GeneratorData.useTargetTransform));
            _durationProp = property.FindPropertyRelative(nameof(GeneratorData.duration));
            _delayProp = property.FindPropertyRelative(nameof(GeneratorData.delay));
            _loopProp = property.FindPropertyRelative(nameof(GeneratorData.loops));
            _loopDelayProp = property.FindPropertyRelative(nameof(GeneratorData.loopDelay));
            _pingPongProp = property.FindPropertyRelative(nameof(GeneratorData.pingPong));
            _onStartProp = property.FindPropertyRelative(nameof(GeneratorData.onStart));
            _onUpdateProp = property.FindPropertyRelative(nameof(GeneratorData.onUpdate));
            _onCompleteProp = property.FindPropertyRelative(nameof(GeneratorData.onComplete));
            _onKillProp = property.FindPropertyRelative(nameof(GeneratorData.onKill));
        }


    }
    
    
}