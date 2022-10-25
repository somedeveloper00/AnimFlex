﻿using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweener
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
                () => transform != null,
                endPosition, ease, duration, delay, curve);
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
                () => transform != null,
                endPosition, ease, duration, delay, curve);
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
                () => transform != null,
                endRotation, ease, duration, delay, curve);
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
                () => transform != null,
                endRotation, ease, duration, delay, curve);
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
                () => transform != null,
                1, ease, duration, delay, curve);
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
                () => transform != null,
                1, ease, duration, delay, curve);
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
                () => transform != null,
                endScale, ease, duration, delay, curve);
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
					Debug.Log($"col: {graphicColor}");
					graphic.color = graphicColor;
                },
                () => graphic != null,
                endFade, ease, duration, delay, curve);
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
                () => material != null,
                endFade, ease, duration, delay, curve);
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
                () => renderer != null,
                endFade, ease, duration, delay, curve);
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
                () => canvasGroup != null,
                endFade, ease, duration, delay, curve);
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
                () => graphic != null,
                endColor, ease, duration, delay, curve);
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
                () => material != null,
                endColor, ease, duration, delay, curve);
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
                () => renderer != null,
                endColor, ease, duration, delay, curve);
        }

        #endregion
    }
}
