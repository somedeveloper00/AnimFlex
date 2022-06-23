using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
    public sealed class GameObjectTweener : MonoBehaviour
    {
        public TweenType tweenType;
        [SerializeField] private TweenerValues tweenerValues;
        public float duration;
        public float delay;
        public bool playOnStart;
        public Easing easing;

        private Tween _tween;

        private void Start()
        {
            if (playOnStart)
            {
                CreateTween();
                _tween.Play();
            }
        }

        public void CreateTween()
        {
            switch (tweenType)
            {
                case TweenType.Move:
                    _tween = this.GenerateTween(
                        transform.position,
                        new Vector3(tweenerValues.toFloat1, tweenerValues.toFloat2, tweenerValues.toFloat3), duration,
                        (value) => transform.position = value);
                    break;
                
                case TweenType.Rotate:
                    _tween = this.GenerateTween(
                        transform.rotation,
                        new Quaternion(tweenerValues.toFloat1, tweenerValues.toFloat2, tweenerValues.toFloat3, tweenerValues.toFloat4), duration,
                        (value) => transform.rotation = value);
                    break;
                
                case TweenType.LocalMove:
                    _tween = this.GenerateTween(
                        transform.localPosition,
                        new Vector3(tweenerValues.toFloat1, tweenerValues.toFloat2, tweenerValues.toFloat3), duration,
                        (value) => transform.localPosition = value);
                    break;
                
                case TweenType.LocalRotate:
                    _tween = this.GenerateTween(
                        transform.localRotation,
                        new Quaternion(tweenerValues.toFloat1, tweenerValues.toFloat2, tweenerValues.toFloat3, tweenerValues.toFloat4), duration,
                        (value) => transform.localRotation = value);
                    break;
                
                case TweenType.LocalScale:
                    _tween = this.GenerateTween(
                        transform.localScale,
                        new Vector3(tweenerValues.toFloat1, tweenerValues.toFloat2, tweenerValues.toFloat3), duration,
                        (value) => transform.localScale = value);
                    break;
                
                case TweenType.ToTransformPosition:
                    _tween = this.GenerateTween(
                        transform,
                        tweenerValues.ref1.transform, duration,
                        ((Vector3 pos, Quaternion rot) value) =>
                        {
                            transform.position = value.pos;
                        });
                    break;
                
                case TweenType.ToTransformRotation:
                    _tween = this.GenerateTween(
                        transform,
                        tweenerValues.ref1.transform, duration,
                        ((Vector3 pos, Quaternion rot) value) =>
                        {
                            transform.rotation = value.rot;
                        });
                    break;
                
                case TweenType.Color:
                    SpriteRenderer spriteRenderer;
                    Image image;
                    
                    if(TryGetComponent(out spriteRenderer))
                    {
                        _tween = this.GenerateTween(
                            spriteRenderer.color,
                            new Color(tweenerValues.toFloat1, tweenerValues.toFloat2, tweenerValues.toFloat3, tweenerValues.toFloat4), duration,
                            (value) => spriteRenderer.color = value);
                    }
                    else if(TryGetComponent(out image))
                    {
                        _tween = this.GenerateTween(
                            image.color,
                            new Color(tweenerValues.toFloat1, tweenerValues.toFloat2, tweenerValues.toFloat3, tweenerValues.toFloat4), duration,
                            (value) => image.color = value);
                    }
                    else
                    {
                        throw new Exception($"{nameof(GameObjectTweener)}: Color tween type requires a {nameof(SpriteRenderer)} or {nameof(Image)} component.");
                    }
                    break;
                case TweenType.Alpha:
                    if(TryGetComponent(out spriteRenderer))
                    {
                        _tween = this.GenerateTween(
                            spriteRenderer.color.a,
                            tweenerValues.toFloat1, duration,
                            (value) =>
                            {
                                var oldColor = spriteRenderer.color;
                                spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, value);
                            });
                    }
                    else if(TryGetComponent(out image))
                    {
                        _tween = this.GenerateTween(
                            image.color.a,
                            tweenerValues.toFloat1, duration,
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
            }

            _tween.delay = delay;
            _tween.Easing = easing;
        }
        
        [Serializable]
        internal struct TweenerValues
        {
            public Component ref1;
            public float toFloat1, toFloat2, toFloat3, toFloat4;
        }
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
}
