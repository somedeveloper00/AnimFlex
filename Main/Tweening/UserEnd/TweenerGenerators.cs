using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
    #region Transform

    [Serializable]
    public class TweenerGeneratorPosition : TweenerGenerator<Transform, Vector3>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            var toPos = target;
            if (relative) toPos += fromObject.position;
            return fromObject.AnimPositionTo(toPos, ease, duration, delay, curve);
        }
    }

    [Serializable]
    public class TweenerGeneratorLocalPosition : TweenerGenerator<Transform, Vector3>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            var toPos = target;
            if (relative) toPos += fromObject.localPosition;
            return fromObject.AnimLocalPositionTo(toPos, ease, duration, delay, curve);
        }
    }

    [Serializable]
    public class TweenerGeneratorRotation : TweenerGenerator<Transform, Vector3>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            Vector3 toRot = target;
            if (relative) toRot += fromObject.rotation.eulerAngles;
            Vector3 startRot = fromObject.rotation.eulerAngles;
            float t = 0;
            return Tweener.Generate(
                () => t,
                (value) =>
                {
                    t = value;
                    fromObject.rotation = Quaternion.Euler(Vector3.LerpUnclamped(startRot, toRot, t));
                },
                1, ease, duration, delay, curve,
                () => fromObject != null);
        }
    }

    [Serializable]
    public class TweenerGeneratorLocalRotation : TweenerGenerator<Transform, Vector3>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            Vector3 toRot = target;
            if (relative) toRot += fromObject.localRotation.eulerAngles;
            Vector3 startRot = fromObject.localRotation.eulerAngles;
            float t = 0;
            return Tweener.Generate(
                () => t,
                (value) =>
                {
                    t = value;
                    fromObject.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(startRot, toRot, t));
                },
                1, ease, duration, delay, curve,
                () => fromObject != null);
        }
    }

    [Serializable]
    public class TweenerGeneratorTransform : TweenerGenerator<Transform, Transform>
    {
        public bool position, rotation;


        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            float t = 0;
            Action<float> onSet = null;

            if (position)
            {
                Vector3 startPos = fromObject.position;
                onSet += (val) => fromObject.position = Vector3.LerpUnclamped(startPos, target.position, val);
            }

            if (rotation)
            {
                Vector3 startRot = fromObject.rotation.eulerAngles;
                onSet += (val) =>
                    fromObject.rotation =
                        Quaternion.Euler(Vector3.LerpUnclamped(startRot, target.rotation.eulerAngles, val));
            }

            return Tweener.Generate(
                () => t,
                (value) =>
                {
                    t = value;
                    onSet?.Invoke(t);
                },
                1, ease, duration, delay, curve,
                () => fromObject != null);
        }
    }

    [Serializable]
    public class TweenerGeneratorScale : TweenerGenerator<Transform, Vector3>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            Vector3 toScl = target;
            if (relative)
            {
                var localScale = fromObject.localScale;
                toScl.x *= localScale.x;
                toScl.y *= localScale.y;
                toScl.z *= localScale.z;
            }
            return fromObject.AnimScaleTo(toScl, ease, duration, delay, curve);
        }
    }

    #endregion

    #region Fade

    [Serializable]
    public class TweenerGeneratorFadeGraphic : TweenerGenerator<Graphic, float>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            float toVal = target;
            if (relative) toVal += fromObject.color.a;
            return fromObject.AnimFadeTo(toVal, ease, duration, delay, curve);
        }
    }

    [Serializable]
    public class TweenerGeneratorFadeRenderer : TweenerGenerator<Renderer, float>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            float toVal = target;
            if (relative) toVal += fromObject.material.color.a;
            return fromObject.AnimFadeTo(toVal, ease, duration, delay, curve);
        }
    }

    [Serializable]
    public class TweenerGeneratorFadeCanvasGroup : TweenerGenerator<CanvasGroup, float>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            float toVal = target;
            if (relative) toVal += fromObject.alpha;
            return fromObject.AnimFadeTo(toVal, ease, duration, delay, curve);
        }
    }

    #endregion

    #region Color

    [Serializable]
    public class TweenerGeneratorColorGraphic : TweenerGenerator<Graphic, Color>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            var toVal = target;
            if (relative) toVal += fromObject.color;
            return fromObject.AnimColorTo(toVal, ease, duration, delay, curve);
        }
    }

    [Serializable]
    public class TweenerGeneratorColorRenderer : TweenerGenerator<Renderer, Color>
    {
        protected override Tweener GenerateTween(AnimationCurve curve)
        {
            var toVal = target;
            if (relative) toVal += fromObject.material.color;
            return fromObject.AnimColorTo(toVal, ease, duration, delay, curve);
        }
    }

    #endregion
}