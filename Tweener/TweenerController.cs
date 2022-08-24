using System;
using System.Collections.Generic;
using System.Diagnostics;
using AnimFlex.Core;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Tweener
{
    internal static class TweenerController
    {
        #region events
        /// <summary>
        /// gets called right at the start of each Tick
        /// </summary>
        internal static event Action onBeforeTick = delegate { };
        #endregion


        #region local variables
        #endregion
        

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void BindToAnimFlexInitializer()
        {
            AnimFlexInitializer.onStart += Init;
            AnimFlexInitializer.onTick += Tick;
        }


        internal static List<Tweener> _tweeners;
        
        /// <summary>
        /// initializes the tweener controller
        /// </summary>
        public static void Init()
        {
            // check if the session already had activities
            if(_tweeners != null)
            {
                foreach (var tweener in _tweeners)
                {
                    tweener?.Revert();
                }
                _tweeners.Clear();
            }

            _tweeners = new List<Tweener>();
        }

        /// <summary>
        /// updates all active tweens. heart of the tweener
        /// </summary>
        public static void Tick()
        {
            onBeforeTick();
            onBeforeTick = delegate { };

            initialize_phase:
            foreach (var tweener in _tweeners)
            {
                if (tweener.flag.HasFlag(TweenerFlag.Initialized) == false)
                {
                    tweener.Init();
                    tweener.flag |= TweenerFlag.Initialized;
                    tweener.OnStart();
                }
            }

            // deletion phase
            deletion_phase:
            for (var i = 0; i < _tweeners.Count; i++)
            {
                var tweener = _tweeners[i];
                // check if contains a delete flag
                if (tweener.flag.HasFlag(TweenerFlag.Deleting))
                {
                    _tweeners[i].OnKill();
                    _tweeners.RemoveAt(i--);
                }
            }

            setter_phase:
            // ignore this phase if this frame is slow
            if (Time.deltaTime > AnimFlexSettings.Instance.deltaTimeIgnoreThreshold)
                return;

            bool _c = false; // mark for tweener's completion
            for (var i = 0; i < _tweeners.Count; i++)
            {
                var tweener = _tweeners[i];
                var t = tweener._t + Time.deltaTime;
                tweener._t = t; // save for next Ticks

                _c = t >= tweener.duration + tweener.delay; // completion check
                t = _c ? 1 : t <= tweener.delay ? 0 : (t - tweener.delay) / tweener.duration;

                tweener.Set(t); // applying delay
                tweener.OnUpdate();

                // check for completion
                if (_c)
                {
                    tweener.flag |= TweenerFlag.Deleting; // add deletion flag
                    if (tweener.flag.HasFlag(TweenerFlag.ForceNoOnComplete) == false)
                        tweener.OnComplete();
                }
            }
        }

        public static void AddTweener(Tweener tweener)
        {
                if (tweener.flag.HasFlag(TweenerFlag.Created))
                {
                    onBeforeTick += () =>
                    {
                        Debug.LogWarning("Tweener already exists. Removing the previous one...");
                        _tweeners.Remove(tweener);
                        tweener._t = 0; // resetting for new usage
                    };
                }

                onBeforeTick += () =>
                {
                    tweener.flag |= TweenerFlag.Created;
                    _tweeners.Add(tweener);
                    // Debug.Log($"Tweener Added. Total count: {_tweeners.Count}");
                };
        }

        public static void KillTweener(Tweener tweener, bool complete = true, bool onCompleteCallback = true)
        {
            if (tweener == null)
                throw new NullReferenceException("tweener");
            
            tweener.flag |= TweenerFlag.Deleting;

            if (complete)
            {
                // manipulate it's inner time
                tweener._t = tweener.delay + tweener.duration;
            }

            if (onCompleteCallback == false)
            {
                tweener.flag |= TweenerFlag.ForceNoOnComplete;
            }
            
        }
    }
}