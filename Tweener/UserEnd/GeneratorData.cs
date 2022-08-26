using System;
using UnityEngine;
using UnityEngine.Events;

namespace AnimFlex.Tweener
{
    [Serializable]
    public class GeneratorData
    {
        public GameObject fromObject;
        public TweenerType tweenerType;
        
        public Vector3 targetVector3;
        public Vector2 targetVector2;
        public float targetFloat;
        public Color targetColor;
        public Transform targetTransform;
        public Quaternion targetQuaternion;
        public AnimationCurve customCurve;
        
        public bool useQuaternion;
        public bool relative;

        public Ease ease;
        public float duration;
        public float delay;
        
        public UnityEvent onStart, onUpdate, onComplete, onKill;
        
        public enum TweenerType
        {
            LocalPosition,
            Position,
            LocalRotation,
            Rotation,
            Scale,
            Fade,
            Color
        }
    }
}