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
        
        
        private SerializedProperty _onStartProp;
        private SerializedProperty _onUpdateProp;
        private SerializedProperty _onCompleteProp;
        private SerializedProperty _onKillProp;

        private int selectedEventIndex = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            targetObject = property.serializedObject.targetObject;
            
            SaveProperties(property);

            EditorGUI.BeginProperty(position, label, property);
            position.width = Mathf.Max(position.width, MIN_WIDTH);
            position.height = Styles.Height;

            Rect linePos;
            DrawFromObject(position);

            position.y += position.height + Styles.VerticalSpace;

            DrawTweenerType(position);

            position.y += position.height + Styles.VerticalSpace;
            DrawValue(position);

            position.y += position.height + Styles.VerticalSpace;
            DrawCurve(position);

            position.y += position.height + Styles.VerticalSpace;
            position.y += 20;
            DrawEvents(position);
            
            position.y += position.height + Styles.VerticalSpace;
            if(DrawPlayback(position))
                return;

            EditorGUI.EndProperty();
        }

        private void DrawEvents(Rect position)
        {
            var linePos = new Rect(position);
            // linePos.width = position.width / 4f;

            var hasOnStart = _onStartProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;
            var hasOnUpdate = _onUpdateProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;
            var hasOnComplete = _onCompleteProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;
            var hasOnKill = _onKillProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;

            GUI.Toolbar(linePos, 0, new [] { "On Start", "On Update", "On Complete", "On Kill" }, Styles.Button);
            
            // DrawButton(hasOnStart, "On Start", _onStartProp, EditorStyles.toolbarButton);
            // DrawButton(hasOnUpdate, "On Update", _onUpdateProp, EditorStyles.miniButtonMid);
            // DrawButton(hasOnComplete, "On Complete", _onCompleteProp, EditorStyles.miniButtonMid);
            // DrawButton(hasOnKill, "On Kill", _onKillProp, EditorStyles.miniButtonRight);


            void DrawButton(bool isON, string label, SerializedProperty eventProp, GUIStyle style)
            {
                if (GUI.Button(linePos, label, style))
                {
                    if (isON)
                        eventProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").ClearArray();
                    else
                        eventProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").InsertArrayElementAtIndex(0);
                }

                linePos.x += linePos.width;
            }
        }

        private void DrawCurve(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            GUI.Label(linePos, "Curve :", Styles.Label);

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
                    ease = (Ease)EditorGUI.EnumPopup(linePos, GUIContent.none, ease, Styles.Popup);
                    if (check.changed)
                        _easeProp.enumValueIndex = (int)ease;
                }
            }

            linePos.x += linePos.width;
            linePos.width = 80;

            if (GUI.Button(linePos, _useCustomCurveProp.boolValue ? "Cutsom" : "Prebuilt", Styles.YellowButton))
            {
                _useCustomCurveProp.boolValue = !_useCustomCurveProp.boolValue;
            }

        }

        private bool DrawPlayback(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 20;
            linePos.x += position.width / 2 - 10;

            using (new EditorGUI.DisabledGroupScope(PreviewUtils.isActive))
            {
                if (GUI.Button(linePos, "P"))
                {
                    PreviewUtils.PreviewTweener(targetObject as TweenerAnim);
                    return true;
                }
            }

            linePos.x += linePos.width;
            using (new EditorGUI.DisabledGroupScope(!PreviewUtils.isActive))
            {
                if (GUI.Button(linePos, "S"))
                {
                    PreviewUtils.StopPreviewMode();
                }
            }

            return false;

        }

        private void DrawValue(Rect position)
        {
            var linePos = new Rect(position);

            linePos.width = 80;
            if (GUI.Button(linePos, _fromProp.boolValue ? "From :" : "To :", Styles.YellowButton))
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
                    if (GUI.Button(linePos, _useTargetTransformProp.boolValue ? "Transform" : "Vector3", Styles.YellowButton))
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
                    if (GUI.Button(linePos, useQuaternion ? "Quaternion" : "Vector3", Styles.YellowButton))
                    {
                        _useQuaternionProp.boolValue = !useQuaternion;
                    }
                    break;
                
                case GeneratorData.TweenerType.Fade:
                    linePos.width = position.width - 80;
                    var oldwidth = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = 10;
                    EditorGUI.PropertyField(linePos, _targetFloatProp, new GUIContent("    "));
                    EditorGUIUtility.labelWidth = oldwidth;
                    break;
                
                case GeneratorData.TweenerType.Color:
                    linePos.width = position.width - 80;
                    EditorGUI.PropertyField(linePos, _targetColorProp, GUIContent.none);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void DrawTweenerType(Rect position)
        {
            var linePos = new Rect(position);
            linePos.width = 80;
            GUI.Label(linePos, "Type :", Styles.Label);
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
                        enumValue = (GeneratorData.TweenerType)EditorGUI.EnumPopup(linePos, enumValue, Styles.Popup);
                        if (check.changed)
                            _tweenerTypeProp.enumValueIndex = (int)enumValue;
                    }
                    linePos.x += 20;
                    linePos.x += linePos.width;
                    linePos.width = 80;
                    GUI.Label(linePos, "Relative", Styles.Label);
                    linePos.x += linePos.width;
                    linePos.width = 50;
                    EditorGUI.PropertyField(linePos, _relativeProp, GUIContent.none);
                    break;
                
                case GeneratorData.TweenerType.Position:
                    
                    linePos.width = position.width - 80 - 150;
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        enumValue = (GeneratorData.TweenerType)EditorGUI.EnumPopup(linePos, enumValue, Styles.Popup);
                        if (check.changed)
                            _tweenerTypeProp.enumValueIndex = (int)enumValue;
                    }
                    linePos.x += 20;
                    linePos.x += linePos.width;
                    if (_useTargetTransformProp.boolValue == false)
                    {
                        linePos.width = 80;
                        GUI.Label(linePos, "Relative", Styles.Label);
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
            GUI.Label(linePos, "From :", Styles.Label);

            linePos.x += linePos.width;
            linePos.width = position.width - 80 - 80;
            EditorGUI.PropertyField(linePos, _fromObjectProp, GUIContent.none);

            linePos.x += linePos.width;
            linePos.width = 80;
            if (GUI.Button(linePos, "self", Styles.Button))
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
            
            var singleLine = Styles.Height + Styles.VerticalSpace;
            float height = 0;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            height += singleLine;
            height += 20;
            
            var hasOnStart = _onStartProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;
            var hasOnUpdate = _onUpdateProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;
            var hasOnComplete = _onCompleteProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;
            var hasOnKill = _onKillProp.FindPropertyRelative("m_PersistentCalls").FindPropertyRelative("m_Calls").arraySize > 0;

            if (hasOnStart)     height += EditorGUI.GetPropertyHeight(_onStartProp);
            if (hasOnUpdate)    height += EditorGUI.GetPropertyHeight(_onUpdateProp);
            if (hasOnComplete)  height += EditorGUI.GetPropertyHeight(_onCompleteProp);
            if (hasOnKill)      height += EditorGUI.GetPropertyHeight(_onKillProp);
            
            height += singleLine;


            return height;
        }
        
        private void SaveProperties(SerializedProperty property)
        {
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
            _onStartProp = property.FindPropertyRelative(nameof(GeneratorData.onStart));
            _onUpdateProp = property.FindPropertyRelative(nameof(GeneratorData.onUpdate));
            _onCompleteProp = property.FindPropertyRelative(nameof(GeneratorData.onComplete));
            _onKillProp = property.FindPropertyRelative(nameof(GeneratorData.onKill));
        }


    }
    
    
}