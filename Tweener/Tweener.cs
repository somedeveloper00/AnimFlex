﻿using System;

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
        ForceNoOnComplete = 1 << 3
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
        
        
        public abstract void Init();
        public abstract void Set(float t);
        public abstract void Revert();
        
        
        internal Tweener()
        {
            TweenerController.AddTweener(this);
        }

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
    }

    public class Tweener<T> : Tweener
    {
        internal T startValue, endValue;
        internal Action<float> setter;
        internal Func<T> getter;
        internal Ease ease;


        public override void Revert()
        {
            setter(0);
        }
        
        public override void Init()
        {
            startValue = getter();
        }
        
        public override void Set(float t)
        {
            setter(t);
        }
    }
}