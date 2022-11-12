using System;
using UnityEngine;

namespace AnimFlex.Tweening
{
    public static class Helpers
    {
        /// <summary>
        /// kills the tweener in the next frame
        /// </summary>
        public static void Kill(this Tweener tweener, bool complete, bool onCompleteCallback)
        {
            TweenerController.Instance.KillTweener(tweener, complete, onCompleteCallback);
        }

        public static Tweener<T> SetDuration<T>(this Tweener<T> tweener, float duration)
        {
            tweener.duration = duration;
            return tweener;
        }

        public static Tweener<T> SetDelay<T>(this Tweener<T> tweener, float delay)
        {
            tweener.delay = delay;
            return tweener;
        }

        public static Tweener<T> SetEase<T>(this Tweener<T> tweener, Ease ease)
        {
            tweener.ease = ease;
            return tweener;
        }

        public static Tweener<T> SetEndValue<T>(this Tweener<T> tweener, T endValue)
        {
            tweener.endValue = endValue;
            return tweener;
        }

        public static Tweener<T> AddOnComplete<T>(this Tweener<T> tweener, Action onComplete)
        {
            tweener.onComplete += onComplete;
            return tweener;
        }

        public static Tweener<T> AddOnKill<T>(this Tweener<T> tweener, Action onKill)
        {
            tweener.onKill += onKill;
            return tweener;
        }

        public static Tweener<T> AddOnUpdate<T>(this Tweener<T> tweener, Action onUpdate)
        {
            tweener.onUpdate += onUpdate;
            return tweener;
        }
    }
}