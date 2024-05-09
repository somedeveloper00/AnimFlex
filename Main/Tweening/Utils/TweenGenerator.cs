using System;
using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
    /// <summary>
    /// low level extensions
    /// </summary>
    public partial class Tweener
    {
        public static Tweener<float> Generate(Func<float> getter, Action<float> setter, float endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerFloat
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<int> Generate(Func<int> getter, Action<int> setter, int endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerInt
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<uint> Generate(Func<uint> getter, Action<uint> setter, uint endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerUInt
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<long> Generate(Func<long> getter, Action<long> setter, long endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerLong
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<ulong> Generate(Func<ulong> getter, Action<ulong> setter, ulong endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerULong
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<decimal> Generate(Func<decimal> getter, Action<decimal> setter, decimal endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerDecimal
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<Vector2> Generate(Func<Vector2> getter, Action<Vector2> setter, Vector2 endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerVector2
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<Vector3> Generate(Func<Vector3> getter, Action<Vector3> setter, Vector3 endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerVector3
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<Quaternion> Generate(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerQuaternion
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<Rect> Generate(Func<Rect> getter, Action<Rect> setter, Rect endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerRect
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<Color> Generate(Func<Color> getter, Action<Color> setter, Color endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerColor
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }

        public static Tweener<string> Generate(Func<string> getter, Action<string> setter, string endValue, float duration = 1, float delay = 0, Ease ease = Ease.InOutSine, AnimationCurve customCurve = null, Func<bool> isValid = null, AnimflexCoreProxy proxy = null)
        {
            var tweener = new TweenerString
            {
                tweenerController = proxy?.core.TweenerController ?? AnimflexCoreProxy.MainDefault.core.TweenerController,
                getter = getter,
                setter = setter,
                isValid = isValid,
                endValue = endValue,
                ease = ease,
                duration = duration,
                delay = delay,
                customCurve = customCurve,
                useCurve = customCurve != null
            };
            tweener.Construct();
            return tweener;
        }
    }
}
