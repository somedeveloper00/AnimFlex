using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
    public static class TweenGeneratorExtentions
    {
        #region AnimPositionTo

        public static Tweener AnimPositionTo(this Transform transform, Vector3 endPosition, AnimationCurve curve, float duration = 1, float delay = 0) =>
            AnimPositionTo(transform, endPosition, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimPositionTo(this Transform transform, Vector3 endPosition, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0) =>
            AnimPositionTo(transform, endPosition, ease, duration, delay, null);

        internal static Tweener AnimPositionTo(this Transform transform, Vector3 endPosition, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => transform.position,
                (value) => transform.position = value,
                endPosition, ease, duration, delay, curve,
                () => transform != null);
        }
        #endregion

        #region AnimLocalPositionTo

        public static Tweener AnimLocalPositionTo(this Transform transform, Vector3 endPosition, AnimationCurve curve, float duration = 1, float delay = 0) =>
            AnimLocalPositionTo(transform, endPosition, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimLocalPositionTo(this Transform transform, Vector3 endPosition, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0) =>
            AnimLocalPositionTo(transform, endPosition, ease, duration, delay, null);

        internal static Tweener AnimLocalPositionTo(this Transform transform, Vector3 endPosition,
            Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => transform.localPosition,
                (value) => transform.localPosition = value,
                endPosition, ease, duration, delay, curve,
                () => transform != null);
        }

        #endregion

        #region AnimRotationTo

        public static Tweener AnimRotationTo(this Transform transform, Quaternion endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0) =>
            AnimRotationTo(transform, endRotation, ease, duration, delay, null);

        public static Tweener AnimRotationTo(this Transform transform, Quaternion endRotation, AnimationCurve curve, float duration = 1, float delay = 0) =>
            AnimRotationTo(transform, endRotation, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        internal static Tweener AnimRotationTo(this Transform transform, Quaternion endRotation,
            Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => transform.rotation,
                (value) => transform.rotation = value,
                endRotation, ease, duration, delay, curve, 
                () => transform != null);
        }

        #endregion

        #region AnimLocalRotationTo

        public static Tweener AnimLocalRotationTo(this Transform transform, Quaternion endRotation, AnimationCurve curve, float duration = 1, float delay = 0) =>
            AnimLocalRotationTo(transform, endRotation, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimLocalRotationTo(this Transform transform, Quaternion endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0) =>
            AnimLocalRotationTo(transform, endRotation, ease, duration, delay, null);

        internal static Tweener AnimLocalRotationTo(this Transform transform, Quaternion endRotation, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => transform.localRotation,
                (value) => transform.localRotation = value,
                endRotation, ease, duration, delay, curve, 
                () => transform != null);
        }
        public static Tweener AnimLocalRotationTo(this Transform transform, Vector3 endRotation, AnimationCurve curve, float duration = 1, float delay = 0) =>
            AnimLocalRotationTo(transform, endRotation, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimLocalRotationTo(this Transform transform, Vector3 endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0) =>
            AnimLocalRotationTo(transform, endRotation, ease, duration, delay, null);

        internal static Tweener AnimLocalRotationTo(this Transform transform, Vector3 endRotation, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            var fromVec = transform.localRotation.eulerAngles;
            var vec = fromVec;
            float t = 0;

            return Tweener.Generate(
                () => t,
                value =>
                {
                    t = value;
                    vec = Vector3.LerpUnclamped(fromVec, endRotation, t);
                    transform.rotation = Quaternion.Euler(vec);
                },
                1, ease, duration, delay, curve,
                () => transform != null);
        }

        #endregion

        #region AnimRotationTo

        public static Tweener AnimRotationTo(this Transform transform, Vector3 endRotation, AnimationCurve curve, float duration = 1, float delay = 0) =>
            AnimRotationTo(transform, endRotation, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimRotationTo(this Transform transform, Vector3 endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0) =>
            AnimRotationTo(transform, endRotation, ease, duration, delay, null);

        internal static Tweener AnimRotationTo(this Transform transform, Vector3 endRotation, Ease ease, float duration, float delay, AnimationCurve curve)
        {

            var fromVec = transform.rotation.eulerAngles;
            var vec = fromVec;
            float t = 0;

            return Tweener.Generate(
                () => t,
                value =>
                {
                    t = value;
                    vec = Vector3.Lerp(fromVec, endRotation, t);
                    transform.rotation = Quaternion.Euler(vec);
                },
                1, ease, duration, delay, curve,
                () => transform != null);
        }



        #endregion

        #region AnimScaleTo

        public static Tweener AnimScaleTo(this Transform transform, Vector3 endScale, AnimationCurve curve, float duration = 1, float delay = 0) =>
            AnimScaleTo(transform, endScale, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimScaleTo(this Transform transform, Vector3 endScale, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0) =>
            AnimScaleTo(transform, endScale, ease, duration, delay, null);

        internal static Tweener AnimScaleTo(this Transform transform, Vector3 endScale, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => transform.localScale,
                (value) => transform.localScale = value,
                endScale, ease, duration, delay, curve,
                () => transform != null);
        }

        #endregion


        #region AnimFadeTo
        public static Tweener AnimFadeTo(this Graphic graphic, float endFade, AnimationCurve curve, float duration = 1, float delay = 0)
            => AnimFadeTo(graphic, endFade, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimFadeTo(this Graphic graphic, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
            => AnimFadeTo(graphic, endFade, ease, duration, delay, null);

        internal static Tweener AnimFadeTo(this Graphic graphic, float endFade, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => graphic.color.a,
                (value) =>
                {
                    var graphicColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, value);
                    graphic.color = graphicColor;
                },
                endFade, ease, duration, delay, curve,
                () => graphic != null);
        }

        public static Tweener AnimFadeTo(this Material material, float endFade, AnimationCurve curve, float duration = 1, float delay = 0)
            => AnimFadeTo(material, endFade, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimFadeTo(this Material material, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
            => AnimFadeTo(material, endFade, ease, duration, delay, null);

        internal static Tweener AnimFadeTo(this Material material, float endFade, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => material.color.a,
                (value) => material.color = new Color(material.color.r, material.color.g, material.color.b, value),
                endFade, ease, duration, delay, curve,
                () => material != null);
        }


        public static Tweener AnimFadeTo(this Renderer renderer, float endFade, AnimationCurve curve, float duration = 1, float delay = 0)
            => AnimFadeTo(renderer, endFade, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimFadeTo(this Renderer renderer, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
            => AnimFadeTo(renderer, endFade, ease, duration, delay, null);

        internal static Tweener AnimFadeTo(this Renderer renderer, float endFade, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => renderer.material.color.a,
                (value) =>
                {
                    var material = renderer.material;
                    material.color = new Color(material.color.r, material.color.g, material.color.b, value);
                },
                endFade, ease, duration, delay, curve,
                () => renderer != null);
        }


        public static Tweener AnimFadeTo(this CanvasGroup canvasGroup, float endFade, AnimationCurve curve, float duration = 1, float delay = 0)
            => AnimFadeTo(canvasGroup, endFade, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimFadeTo(this CanvasGroup canvasGroup, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
            => AnimFadeTo(canvasGroup, endFade, ease, duration, delay, null);

        internal static Tweener AnimFadeTo(this CanvasGroup canvasGroup, float endFade, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => canvasGroup.alpha,
                (value) => canvasGroup.alpha = value,
                endFade, ease, duration, delay, curve,
                () => canvasGroup != null);
        }

        #endregion

        #region AnimColorTo

        public static Tweener AnimColorTo(this Graphic graphic, Color endColor, AnimationCurve curve, float duration = 1, float delay = 0)
            => AnimColorTo(graphic, endColor, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimColorTo(this Graphic graphic, Color endColor, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
            => AnimColorTo(graphic, endColor, ease, duration, delay, null);

        internal static Tweener AnimColorTo(this Graphic graphic, Color endColor, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => graphic.color,
                (value) => graphic.color = value,
                endColor, ease, duration, delay, curve,
                () => graphic != null);
        }


        public static Tweener AnimColorTo(this Material material, Color endColor, AnimationCurve curve, float duration = 1, float delay = 0)
            => AnimColorTo(material, endColor, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimColorTo(this Material material, Color endColor, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
            => AnimColorTo(material, endColor, ease, duration, delay, null);

        internal static Tweener AnimColorTo(this Material material, Color endColor, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => material.color,
                (value) => material.color = value,
                endColor, ease, duration, delay, curve,
                () => material != null);
        }


        public static Tweener AnimColorTo(this Renderer renderer, Color endColor, AnimationCurve curve, float duration = 1, float delay = 0)
            => AnimColorTo(renderer, endColor, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, duration, delay, curve);

        public static Tweener AnimColorTo(this Renderer renderer, Color endColor, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
            => AnimColorTo(renderer, endColor, ease, duration, delay, null);

        internal static Tweener AnimColorTo(this Renderer renderer, Color endColor, Ease ease, float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => renderer.material.color,
                (value) => renderer.material.color = value,
                endColor, ease, duration, delay, curve,
                () => renderer != null);
        }

        #endregion
    }
}
