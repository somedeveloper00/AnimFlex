using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Tweening.Editor
{
    [CustomPropertyDrawer(typeof(GameObjectTweenUtilities.TweenerValues))]
    public class TweenerValuesEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            var tweenTypeProp = property.FindPropertyRelative(nameof(GameObjectTweenUtilities.TweenerValues.tweenType));

            var tweenType = (GameObjectTweenUtilities.TweenerValues.TweenType)tweenTypeProp.enumValueIndex;
            
            var ref1 = property.FindPropertyRelative(nameof(GameObjectTweenUtilities.TweenerValues.ref1));
            var float1 = property.FindPropertyRelative(nameof(GameObjectTweenUtilities.TweenerValues.toFloat1));
            var float2 = property.FindPropertyRelative(nameof(GameObjectTweenUtilities.TweenerValues.toFloat2));
            var float3 = property.FindPropertyRelative(nameof(GameObjectTweenUtilities.TweenerValues.toFloat3));
            var float4 = property.FindPropertyRelative(nameof(GameObjectTweenUtilities.TweenerValues.toFloat4));


            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, tweenTypeProp);
            position.y += position.height + 2;
            
            switch (tweenType)
            {
                case GameObjectTweenUtilities.TweenerValues.TweenType.Move:
                    position = DrawMove(position, float1, float2, float3);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.Rotate:
                    position = DrawRotate(position, float1, float2, float3, float4);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.LocalMove:
                    position = DrawMove(position, float1, float2, float3);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.LocalScale:
                    position = DrawScale(position, float1, float2, float3);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.LocalRotate:
                    position = DrawRotate(position, float1, float2, float3, float4);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.ToTransformPosition:
                    position = DrawTransform(position, ref1);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.ToTransformRotation:
                    position = DrawTransform(position, ref1);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.Color:
                    position = DrawColor(position, float1, float2, float3, float4);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.Alpha:
                    position = DrawAlpha(position, float1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

     


        private static Rect DrawMove(Rect pos, SerializedProperty float1, SerializedProperty float2, SerializedProperty float3)
        {
            Vector3 vec3Val = new Vector3(float1.floatValue, float2.floatValue, float3.floatValue);
            EditorGUI.BeginChangeCheck();
            
            pos.height = EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, new GUIContent("Move To"));
            vec3Val = EditorGUI.Vector3Field(pos, "Move To", vec3Val);
            pos.y += pos.height;
            if (EditorGUI.EndChangeCheck())
            {
                float1.floatValue = vec3Val.x;
                float2.floatValue = vec3Val.y;
                float3.floatValue = vec3Val.z;
            }

            return pos;
        }

        private static Rect DrawRotate(Rect pos, SerializedProperty float1, SerializedProperty float2, SerializedProperty float3,
            SerializedProperty float4)
        {
            Quaternion quatVal = new Quaternion(float1.floatValue, float2.floatValue, float3.floatValue, float4.floatValue);
            Vector3 vec3Val = quatVal.eulerAngles;
            EditorGUI.BeginChangeCheck();
            vec3Val = EditorGUI.Vector3Field(pos, "Rotate To", vec3Val);
            pos.y += EditorGUIUtility.singleLineHeight;
            if (EditorGUI.EndChangeCheck())
            {
                quatVal = Quaternion.Euler(vec3Val);
                float1.floatValue = quatVal.x;
                float2.floatValue = quatVal.y;
                float3.floatValue = quatVal.z;
                float4.floatValue = quatVal.w;
            }

            return pos;
        }
        private Rect DrawScale(Rect pos, SerializedProperty float1, SerializedProperty float2, SerializedProperty float3)
        {
            Vector3 vec3Val = new Vector3(float1.floatValue, float2.floatValue, float3.floatValue);
            EditorGUI.BeginChangeCheck();
            vec3Val = EditorGUI.Vector3Field(pos, "Scale To", vec3Val);
            pos.y += EditorGUIUtility.singleLineHeight;
            if (EditorGUI.EndChangeCheck())
            {
                float1.floatValue = vec3Val.x;
                float2.floatValue = vec3Val.y;
                float3.floatValue = vec3Val.z;
            }

            return pos;
        }
        private Rect DrawTransform(Rect pos, SerializedProperty ref1)
        {
            EditorGUI.PropertyField(pos, ref1, new GUIContent("Target Transform"));
            pos.y += EditorGUIUtility.singleLineHeight;
            return pos;
        }
        private Rect DrawColor(Rect pos, SerializedProperty float1, SerializedProperty float2, SerializedProperty float3, SerializedProperty float4)
        {
            var color = new Color(float1.floatValue, float2.floatValue, float3.floatValue, float4.floatValue);
            EditorGUI.BeginChangeCheck();
            color = EditorGUI.ColorField(pos, "Color To", color);
            pos.y += EditorGUIUtility.singleLineHeight;
            if (EditorGUI.EndChangeCheck())
            {
                float1.floatValue = color.r;
                float2.floatValue = color.g;
                float3.floatValue = color.b;
                float4.floatValue = color.a;
            }

            return pos;
        }

        private Rect DrawAlpha(Rect pos, SerializedProperty float1)
        {
            EditorGUI.PropertyField(pos, float1, new GUIContent("To Alpha"));
            pos.y += EditorGUIUtility.singleLineHeight;
            return pos;
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var totalHeight = EditorGUI.GetPropertyHeight(SerializedPropertyType.ObjectReference, label);
            var tweenTypeProp = property.FindPropertyRelative(nameof(GameObjectTweenUtilities.TweenerValues.tweenType));
            var tweenType = (GameObjectTweenUtilities.TweenerValues.TweenType)tweenTypeProp.intValue;

            switch (tweenType)
            {
                case GameObjectTweenUtilities.TweenerValues.TweenType.Move:
                case GameObjectTweenUtilities.TweenerValues.TweenType.Rotate:
                case GameObjectTweenUtilities.TweenerValues.TweenType.LocalMove:
                case GameObjectTweenUtilities.TweenerValues.TweenType.LocalScale:
                case GameObjectTweenUtilities.TweenerValues.TweenType.LocalRotate:
                    totalHeight += EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, label);
                    break;
                case GameObjectTweenUtilities.TweenerValues.TweenType.ToTransformPosition:
                case GameObjectTweenUtilities.TweenerValues.TweenType.ToTransformRotation:
                case GameObjectTweenUtilities.TweenerValues.TweenType.Color:
                case GameObjectTweenUtilities.TweenerValues.TweenType.Alpha:
                    totalHeight += EditorGUIUtility.singleLineHeight;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return totalHeight + 2;
        }
    }
}