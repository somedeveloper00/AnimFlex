using System;
using UnityEngine;

namespace AnimFlex.Tweener
{
    /// <summary>
    /// low level extensions
    /// </summary>
    public partial class Tweener
    {
        public static Tweener<float> Generate(
            Func<float> getter, Action<float> setter, float endValue, Ease ease, float duration = 1, float delay = 0)
        {
            var tweener = new Tweener<float>()
            {
                getter = getter,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay
            };
            tweener.setter = t =>
                setter(Mathf.Lerp(tweener.startValue, tweener.endValue, EaseUtility.EvaluateEase(ease, t, null)));
            return tweener;
        }

        public static Tweener<int> Generate(
            Func<int> getter, Action<int> setter, int endValue, Ease ease, float duration = 1, float delay = 0)
        {
            var tweener = new Tweener<int>()
            {
                getter = getter,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay
            };
            tweener.setter = t =>
                setter(Mathf.RoundToInt(Mathf.Lerp(tweener.startValue, tweener.endValue,
                    EaseUtility.EvaluateEase(ease, t, null))));
            return tweener;
        }

        public static Tweener<Vector2> Generate(
            Func<Vector2> getter, Action<Vector2> setter, Vector2 endValue, Ease ease, float duration = 1,
            float delay = 0)
        {
            var tweener = new Tweener<Vector2>()
            {
                getter = getter,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay
            };
            tweener.setter = t =>
                setter(Vector2.Lerp(tweener.startValue, tweener.endValue, EaseUtility.EvaluateEase(ease, t, null)));
            return tweener;
        }

        public static Tweener<Vector3> Generate(
            Func<Vector3> getter, Action<Vector3> setter, Vector3 endValue, Ease ease, float duration = 1,
            float delay = 0)
        {
            var tweener = new Tweener<Vector3>()
            {
                getter = getter,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay
            };
            tweener.setter = t =>
                setter(Vector3.Lerp(tweener.startValue, tweener.endValue, EaseUtility.EvaluateEase(ease, t, null)));
            return tweener;
        }

        public static Tweener<Quaternion> Generate(
            Func<Quaternion> getter, Action<Quaternion> setter, Quaternion endValue, Ease ease, float duration = 1,
            float delay = 0)
        {
            var tweener = new Tweener<Quaternion>()
            {
                getter = getter,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay
            };
            tweener.setter = t =>
                setter(Quaternion.Lerp(tweener.startValue, tweener.endValue, EaseUtility.EvaluateEase(ease, t, null)));
            return tweener;
        }

        public static Tweener<Rect> Generate(
            Func<Rect> getter, Action<Rect> setter, Rect endValue, Ease ease, float duration = 1, float delay = 0)
        {
            var tweener = new Tweener<Rect>()
            {
                getter = getter,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay
            };
            tweener.setter = t => setter(new Rect(
                Mathf.Lerp(tweener.startValue.x, tweener.endValue.x, t),
                Mathf.Lerp(tweener.startValue.y, tweener.endValue.y, t),
                Mathf.Lerp(tweener.startValue.width, tweener.endValue.width, t),
                Mathf.Lerp(tweener.startValue.height, tweener.endValue.height, EaseUtility.EvaluateEase(ease, t, null)))
            );
            return tweener;
        }

        public static Tweener<Color> Generate(
            Func<Color> getter, Action<Color> setter, Color endValue, Ease ease, float duration = 1, float delay = 0)
        {
            var tweener = new Tweener<Color>()
            {
                getter = getter,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay
            };
            tweener.setter = t =>
                setter(Color.Lerp(tweener.startValue, tweener.endValue, EaseUtility.EvaluateEase(ease, t, null)));
            return tweener;
        }
    }
}