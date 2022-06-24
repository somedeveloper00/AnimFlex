using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
    public static class GameObjectTweenUtilities
    {
            
        [Serializable]
        internal struct TweenerValues
        {
            public TweenType tweenType;
            public Component ref1;
            public float toFloat1, toFloat2, toFloat3, toFloat4;
            
            public enum TweenType
            {
                Move,
                Rotate,
                LocalMove,
                LocalScale,
                LocalRotate,
                ToTransformPosition,
                ToTransformRotation,
                Color,
                Alpha
            }
        }
        
        internal static Tween CreateTween(this MonoBehaviour caller, TweenerValues values, float duration)
        {
            Tween tween = null;
            
            switch (values.tweenType)
            {
                case TweenerValues.TweenType.Move:
                    tween = caller.GenerateTween(
                        caller.transform.position,
                        new Vector3(values.toFloat1, values.toFloat2, values.toFloat3), duration,
                        (value) => caller.transform.position = value);
                    break;
                
                case TweenerValues.TweenType.Rotate:
                    tween = caller.GenerateTween(
                        caller.transform.rotation,
                        new Quaternion(values.toFloat1, values.toFloat2, values.toFloat3, values.toFloat4), duration,
                        (value) => caller.transform.rotation = value);
                    break;
                
                case TweenerValues.TweenType.LocalMove:
                    tween = caller.GenerateTween(
                        caller.transform.localPosition,
                        new Vector3(values.toFloat1, values.toFloat2, values.toFloat3), duration,
                        (value) => caller.transform.localPosition = value);
                    break;
                
                case TweenerValues.TweenType.LocalRotate:
                    tween = caller.GenerateTween(
                        caller.transform.localRotation,
                        new Quaternion(values.toFloat1, values.toFloat2, values.toFloat3, values.toFloat4), duration,
                        (value) => caller.transform.localRotation = value);
                    break;
                
                case TweenerValues.TweenType.LocalScale:
                    tween = caller.GenerateTween(
                        caller.transform.localScale,
                        new Vector3(values.toFloat1, values.toFloat2, values.toFloat3), duration,
                        (value) => caller.transform.localScale = value);
                    break;
                
                case TweenerValues.TweenType.ToTransformPosition:
                    tween = caller.GenerateTween(
                        caller.transform,
                        values.ref1.transform, duration,
                        ((Vector3 pos, Quaternion rot) value) =>
                        {
                            caller.transform.position = value.pos;
                        });
                    break;
                
                case TweenerValues.TweenType.ToTransformRotation:
                    tween = caller.GenerateTween(
                        caller.transform,
                        values.ref1.transform, duration,
                        ((Vector3 pos, Quaternion rot) value) =>
                        {
                            caller.transform.rotation = value.rot;
                        });
                    break;
                
                case TweenerValues.TweenType.Color:
                    SpriteRenderer spriteRenderer;
                    Image image;
                    
                    if(caller.TryGetComponent(out spriteRenderer))
                    {
                        tween = caller.GenerateTween(
                            spriteRenderer.color,
                            new Color(values.toFloat1, values.toFloat2, values.toFloat3, values.toFloat4), duration,
                            (value) => spriteRenderer.color = value);
                    }
                    else if(caller.TryGetComponent(out image))
                    {
                        tween = caller.GenerateTween(
                            image.color,
                            new Color(values.toFloat1, values.toFloat2, values.toFloat3, values.toFloat4), duration,
                            (value) => image.color = value);
                    }
                    else
                    {
                        throw new Exception($"{nameof(GameObjectTweener)}: Color tween type requires a {nameof(SpriteRenderer)} or {nameof(Image)} component.");
                    }
                    break;
                case TweenerValues.TweenType.Alpha:
                    if(caller.TryGetComponent(out spriteRenderer))
                    {
                        tween = caller.GenerateTween(
                            spriteRenderer.color.a,
                            values.toFloat1, duration,
                            (value) =>
                            {
                                var oldColor = spriteRenderer.color;
                                spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, value);
                            });
                    }
                    else if(caller.TryGetComponent(out image))
                    {
                        tween = caller.GenerateTween(
                            image.color.a,
                            values.toFloat1, duration,
                            (value) =>
                            {
                                var oldColor = image.color;
                                image.color = new Color(oldColor.r, oldColor.g, oldColor.b, value);
                            });
                    }
                    else
                    {
                        throw new Exception($"{nameof(GameObjectTweener)}: Color tween type requires a {nameof(SpriteRenderer)} or {nameof(Image)} component.");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(values.tweenType), values.tweenType, null);
            }
            return tween;
        }

    }
}