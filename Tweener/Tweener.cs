using System;
using UnityEngine;

namespace AnimFlex.Tweener
{
    [Flags]
    internal enum TweenerFlag
    {
        /// <summary>
        /// marked for deletion
        /// </summary>
        Deleting = 1 << 0,
        
        /// <summary>
        /// marked created so later on it can't be re-used
        /// </summary>
        Created = 1 << 1,
        
        /// <summary>
        /// whetehr or not the Init function has been called
        /// </summary>
        Initialized = 1 << 2,
    
        /// <summary>
        /// forces the tweener to not call to onComplete while completed
        /// </summary>
        ForceNoOnComplete = 1 << 3,
    }
    
    public abstract partial class Tweener
    {
        /// per-tween T value for evaluation
        internal float _t = 0;
        
        /// <summary>
        /// the flag representing the status of this tween. used mostly by TweenController
        /// </summary>
        internal TweenerFlag flag;
        
        /// <summary>
        /// the duration of which this tween will take to finish. this does NOT include the delay time
        /// </summary>
        internal float duration;
        
        /// <summary>
        /// the initial delay after the Initialization.
        /// during delay time, the tween will keep using setter to set to startValue
        /// </summary>
        internal float delay;
        
        /// <summary>
        /// the type of ease function
        /// </summary>
        internal Ease ease;

        /// <summary>
        /// custom curve used for special cases where user wants to use their specific curve as the Ease
        /// </summary>
        internal AnimationCurve customCurve;

        internal bool useCurve = false;

        internal abstract void Init();
        internal abstract void Set(float t);
        internal abstract void Revert();
        internal abstract void SwapStartAndEnd();
        
        
        internal Tweener() => TweenerController.Instance.AddTweener(this);

        #region events

        /// <summary>
        /// called on Init. the init happens as the first phase of each AnimFlex's Tick.
        /// </summary>
        public event Action onStart = delegate {  };
        
        /// <summary>
        /// called on each AnimFlex's Tick
        /// </summary>
        public event Action onUpdate = delegate {  };
        
        /// <summary>
        /// called on the final setter call when it reaches the endValue. will be called after onUpdate
        /// </summary>
        public event Action onComplete = delegate {  };
        
        /// <summary>
        /// called on the last phase of AnimFlex's Tick. mostly at the same frame as onComplete, unless the tween is killed manually or by error
        /// </summary>
        public event Action onKill = delegate {  };

        internal void OnStart() => onStart();
        internal void OnUpdate() => onUpdate();
        internal void OnComplete() => onComplete();
        internal void OnKill() => onKill();
        #endregion

        #region helpers
        public void Kill(bool complete, bool onKillCallback) => TweenerController.Instance.KillTweener(this, complete, onKillCallback);
        #endregion
    }

    public abstract class Tweener<T> : Tweener
    {
        internal T startValue, endValue;
        internal Action<T> setter;
        internal Func<T> getter;

        internal override void Revert()
        {
            setter(startValue);
        }

        internal override void Init()
        {
            startValue = getter();
        }
        
        internal override void SwapStartAndEnd() => (startValue, endValue) = (endValue, startValue);
    }
}