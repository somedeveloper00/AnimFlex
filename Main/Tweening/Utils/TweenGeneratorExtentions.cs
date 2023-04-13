using AnimFlex.Core.Proxy;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
    public static class TweenGeneratorExtentions
    {
        #region AnimPositionTo

        public static Tweener AnimPositionTo(this Transform transform, Vector3 endPosition, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimPositionTo(transform, endPosition, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimPositionTo(this Transform transform, Vector3 endPosition, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimPositionTo(transform, endPosition, duration, delay, ease, null, proxy);

        internal static Tweener AnimPositionTo(this Transform transform, Vector3 endPosition, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => transform.position,
                (value) => transform.position = value,
                endPosition, duration, delay, ease, curve,
                () => transform != null, proxy);
        }
        #endregion

        #region AnimLocalPositionTo

        public static Tweener AnimLocalPositionTo(this Transform transform, Vector3 endPosition, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimLocalPositionTo(transform, endPosition, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimLocalPositionTo(this Transform transform, Vector3 endPosition, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimLocalPositionTo(transform, endPosition, duration, delay, ease, null, proxy);

        internal static Tweener AnimLocalPositionTo(this Transform transform, Vector3 endPosition, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => transform.localPosition,
                (value) => transform.localPosition = value,
                endPosition, duration, delay, ease, curve,
                () => transform != null, proxy);
        }

        #endregion

        #region AnimRotationTo

        public static Tweener AnimRotationTo(this Transform transform, Quaternion endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimRotationTo(transform, endRotation, duration, delay, ease, null, proxy);

        public static Tweener AnimRotationTo(this Transform transform, Quaternion endRotation, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimRotationTo(transform, endRotation, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        internal static Tweener AnimRotationTo(this Transform transform, Quaternion endRotation, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => transform.rotation,
                (value) => transform.rotation = value,
                endRotation, duration, delay, ease, curve, 
                () => transform != null, proxy);
        }

        #endregion

        #region AnimLocalRotationTo

        public static Tweener AnimLocalRotationTo(this Transform transform, Quaternion endRotation, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimLocalRotationTo(transform, endRotation, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimLocalRotationTo(this Transform transform, Quaternion endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimLocalRotationTo(transform, endRotation, duration, delay, ease, null, proxy);

        internal static Tweener AnimLocalRotationTo(this Transform transform, Quaternion endRotation, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => transform.localRotation,
                (value) => transform.localRotation = value,
                endRotation, duration, delay, ease, curve, 
                () => transform != null, proxy);
        }
        public static Tweener AnimLocalRotationTo(this Transform transform, Vector3 endRotation, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimLocalRotationTo(transform, endRotation, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimLocalRotationTo(this Transform transform, Vector3 endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimLocalRotationTo(transform, endRotation, duration, delay, ease, null, proxy);

        internal static Tweener AnimLocalRotationTo(this Transform transform, Vector3 endRotation, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
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
                1, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => transform != null );
        }

        #endregion

        #region AnimRotationTo

        public static Tweener AnimRotationTo(this Transform transform, Vector3 endRotation, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimRotationTo(transform, endRotation, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimRotationTo(this Transform transform, Vector3 endRotation, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimRotationTo(transform, endRotation, duration, delay, ease, null, proxy);

        internal static Tweener AnimRotationTo(this Transform transform, Vector3 endRotation, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
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
                1, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => transform != null );
        }



        #endregion

        #region AnimScaleTo

        public static Tweener AnimScaleTo(this Transform transform, Vector3 endScale, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimScaleTo(transform, endScale, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimScaleTo(this Transform transform, Vector3 endScale, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimScaleTo(transform, endScale, duration, delay, ease, null, proxy);

        internal static Tweener AnimScaleTo(this Transform transform, Vector3 endScale, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => transform.localScale,
                (value) => transform.localScale = value,
                endScale, duration, delay, ease, curve,
                () => transform != null, proxy);
        }

        #endregion


        #region AnimFadeTo
        public static Tweener AnimFadeTo(this Graphic graphic, float endFade, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(graphic, endFade, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimFadeTo(this Graphic graphic, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(graphic, endFade, duration, delay, ease, null, proxy);

        internal static Tweener AnimFadeTo(this Graphic graphic, float endFade, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => graphic.color.a,
                (value) =>
                {
                    var graphicColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, value);
                    graphic.color = graphicColor;
                },
                endFade, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => graphic != null );
        }

        public static Tweener AnimFadeTo(this Material material, float endFade, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(material, endFade, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimFadeTo(this Material material, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(material, endFade, duration, delay, ease, null, proxy);

        internal static Tweener AnimFadeTo(this Material material, float endFade, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => material.color.a,
                (value) => material.color = new Color(material.color.r, material.color.g, material.color.b, value),
                endFade, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => material != null );
        }


        public static Tweener AnimFadeTo(this Renderer renderer, float endFade, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(renderer, endFade, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimFadeTo(this Renderer renderer, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(renderer, endFade, duration, delay, ease, null, proxy);

        internal static Tweener AnimFadeTo(this Renderer renderer, float endFade, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => renderer.material.color.a,
                (value) =>
                {
                    var material = renderer.material;
                    material.color = new Color(material.color.r, material.color.g, material.color.b, value);
                },
                endFade, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => renderer != null );
        }


        public static Tweener AnimFadeTo(this CanvasGroup canvasGroup, float endFade, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(canvasGroup, endFade, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimFadeTo(this CanvasGroup canvasGroup, float endFade, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimFadeTo(canvasGroup, endFade, duration, delay, ease, null, proxy);

        internal static Tweener AnimFadeTo(this CanvasGroup canvasGroup, float endFade, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => canvasGroup.alpha,
                (value) => canvasGroup.alpha = value,
                endFade, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => canvasGroup != null );
        }

        #endregion

        #region AnimColorTo

        public static Tweener AnimColorTo(this Graphic graphic, Color endColor, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimColorTo(graphic, endColor, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimColorTo(this Graphic graphic, Color endColor, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimColorTo(graphic, endColor, duration, delay, ease, null, proxy);

        internal static Tweener AnimColorTo(this Graphic graphic, Color endColor, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => graphic.color,
                (value) => graphic.color = value,
                endColor, duration, delay, ease, curve,
                () => graphic != null, proxy);
        }


        public static Tweener AnimColorTo(this Material material, Color endColor, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimColorTo(material, endColor, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimColorTo(this Material material, Color endColor, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimColorTo(material, endColor, duration, delay, ease, null, proxy);

        internal static Tweener AnimColorTo(this Material material, Color endColor, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => material.color,
                (value) => material.color = value,
                endColor, duration, delay, ease, curve,
                () => material != null, proxy);
        }


        public static Tweener AnimColorTo(this Renderer renderer, Color endColor, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimColorTo(renderer, endColor, duration, delay, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, proxy);

        public static Tweener AnimColorTo(this Renderer renderer, Color endColor, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
            => AnimColorTo(renderer, endColor, duration, delay, ease, null, proxy);

        internal static Tweener AnimColorTo(this Renderer renderer, Color endColor, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => renderer.material.color,
                (value) => renderer.material.color = value,
                endColor, duration, delay, ease, curve,
                () => renderer != null, proxy);
        }

        #endregion
    }
}
