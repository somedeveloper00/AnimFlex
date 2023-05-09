using System;
using System.Threading.Tasks;

namespace AnimFlex.Tweening {
    public static class Helpers {
        /// <summary>
        /// kills the tweener in the next frame
        /// </summary>
        public static void Kill(this Tweener tweener, bool complete, bool onCompleteCallback) {
            tweener.tweenerController.KillTweener( tweener, complete, onCompleteCallback );
        }

        /// <summary>
        /// Sets the duration of the Tweener
        /// </summary>
        public static Tweener SetDuration(this Tweener tweener, float duration) {
            tweener.duration = duration;
            return tweener;
        }

        /// <summary>
        /// Awaits the completion of the tweener
        /// </summary>
        public static async Task AwaitComplete(this Tweener tweener) {
            while(tweener.IsActive()) await Task.Yield();
        }

        /// <summary>
        /// Sets the delay of the tweener
        /// </summary>
        public static Tweener SetDelay(this Tweener tweener, float delay) {
            tweener.delay = delay;
            return tweener;
        }

        /// <summary>
        /// Sets this Tweeenr as PingPong
        /// </summary>
        public static Tweener SetPingPong(this Tweener tweener) {
            tweener.pingPong = true;
            return tweener;
        }
        
        /// <summary>
        /// Sets the Ease of this Tweener
        /// </summary>
        public static Tweener SetEase(this Tweener tweener, Ease ease) {
            tweener.ease = ease;
            return tweener;
        }

        /// <summary>
        /// Adds a callback to execute when the tweener is complete
        /// </summary>
        public static Tweener AddOnComplete(this Tweener tweener, Action onComplete) {
            tweener.onComplete += onComplete;
            return tweener;
        }

        /// <summary>
        /// Adds a callback to execute when the tweener is killed
        /// </summary>
        public static Tweener AddOnKill(this Tweener tweener, Action onKill) {
            tweener.onKill += onKill;
            return tweener;
        }

        /// <summary>
        /// Adds a callback to execute when the tweener is complete
        /// </summary>
        public static Tweener AddOnUpdate(this Tweener tweener, Action onUpdate) {
            tweener.onUpdate += onUpdate;
            return tweener;
        }
    }
}