using System;

namespace AnimFlex.Tweening
{
    [Serializable]
    public abstract class Tween
    {
       

        protected abstract void OnInternalUpdate(float t);

        internal virtual bool ShouldCancel() => false;

        #region parametsrs

        /// applies after the CreateTween method
        public float delay;

        /// the duration in which this tween ends
        public float duration = 2;

        /// sets an easing formula to the tween.
        /// for more info, check out https://easings.net
        public Easing easing;

        #endregion

        #region events

        private Action _onStart;
        private Action _onEnd;

        public void AddOnStart(Action callback) => _onStart += callback;
        public void RemoveOnStart(Action callback) => _onStart -= callback;
        public void AddOnEnd(Action callback) => _onEnd += callback;
        public void RemoveOnEnd(Action callback) => _onEnd -= callback;

        #endregion

        #region public methods

        /// <summary>
        ///     plays the tween.
        ///     Note that the tween usually automatically plays when you are using extensions or components. so use this when
        ///     you're sure the tween is not playing
        /// </summary>
        public void Play()
        {
            _onStart?.Invoke();
            Increment = 1 / duration;
            TweeningUpdater.PlayTween(this);
        }

        /// <summary>
        ///     forces the tween to end.
        ///     it will not call the onUpdate event, but the internal functions will make sure the tween goes to it's end state
        /// </summary>
        public void ForceEnd()
        {
            InternalEnd();
        }

        // this makes users avoid manual update
        public void ManualUpdate(float t)
        {
            Time = t * duration + delay;
            InternalUpdate();
        }
        public bool IsFinished => _isFinished;
        #endregion

        #region internal

        private bool _isFinished = false;
        internal float Time; // the time it has taken from the initial CreateTween function
        protected float Increment;
        internal void InternalEnd()
        {
            _onEnd?.Invoke();
            _isFinished = true;
        }
        internal void InternalUpdate()
        {
            var t = EasingUtilities.Evaluate(easing, (Time - delay) * Increment);
            OnInternalUpdate(t);
        }
        #endregion
    }

    public abstract class Tween<T> : Tween
    {
        protected T FromValue, ToValue;
        protected Func<T, T, float, T> Evaluator;
        protected override void OnInternalUpdate(float t)
        {
            var v = Evaluator(FromValue, ToValue, t); // get generic value
            _onUpdate?.Invoke(v);
        }

        internal override bool ShouldCancel()
        {
            return false;
        }

        #region events
        private Action<T> _onUpdate;
        public void AddOnUpdate(Action<T> callback) => _onUpdate += callback;
        public void RemoveOnUpdate(Action<T> callback) => _onUpdate -= callback;
        #endregion
    }
}