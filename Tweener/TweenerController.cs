using System;
using System.Collections.Generic;
using AnimFlex.Core;
using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Tweener
{
    internal static class TweenerController
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void BindToAnimFlexInitializer()
        {
            AnimFlexInitializer.onInit += Init;
            AnimFlexInitializer.onTick += Tick;
        }


        internal static Tweener[] _activeTweeners;
        private static int _activeTweenersLength = 0; // real length of active tweens
        
        internal static List<Tweener> _deletingTweeners = new List<Tweener>(2 * 2 * 2 * 2 * 2 * 2 * 2);
        
        
        /// <summary>
        /// initializes the tweener controller
        /// </summary>
        public static void Init()
        {
            // check if the session already had activities
            if(_activeTweeners != null)
            {
                foreach (var tweener in _activeTweeners)
                {
                    tweener?.Revert();
                }

            }
            _activeTweeners = new Tweener[AnimFlexSettings.Instance.maxTweenCount];
            _deletingTweeners.Clear();
        }

        /// <summary>
        /// updates all active tweens. heart of the tweener
        /// </summary>
        public static void Tick()
        {
            
            initialize_phase:
            for (var i = 0; i < _activeTweenersLength; i++)
            {
                var tweener = _activeTweeners[i];
                if (tweener.flag.HasFlag(TweenerFlag.Initialized) == false)
                {
                    tweener.Init();
                    tweener.flag |= TweenerFlag.Initialized;
                    tweener.OnStart();
                }
            }

            setter_phase:
            bool _c = false; // mark for tweener's completion
            for (var i = 0; i < _activeTweenersLength; i++)
            {
                var tweener = _activeTweeners[i];
                var t = tweener._t + Time.deltaTime;
                
                // to avoid repeated evaluations
                if(tweener._t == t) continue;
                
                tweener._t = t; // save for next Ticks

                _c = t >= tweener.duration + tweener.delay; // completion check
                t = _c ? 1 : t <= tweener.delay ? 0 : (t - tweener.delay) / tweener.duration;

                tweener.Set(EaseUtility.EvaluateEase(tweener.ease, tweener.easeQuality, t, null));
                tweener.OnUpdate();

                // check for completion
                if (_c)
                {
                    tweener.flag |= TweenerFlag.Deleting; // add deletion flag
                    if (tweener.flag.HasFlag(TweenerFlag.ForceNoOnComplete) == false)
                        tweener.OnComplete();
                }
            }
            
            deletion_phase:
            for (var i = 0; i < _activeTweenersLength; i++)
            {
                // check if contains a delete flag
                if (_activeTweeners[i].flag.HasFlag(TweenerFlag.Deleting))
                {
                    _activeTweeners[i].OnKill();

                    _activeTweeners[i] = _activeTweeners[_activeTweenersLength - 1];
                    _activeTweenersLength--;
                    i--;
                }
            }
            // actual deletion
        }

        public static void AddTweener(Tweener tweener)
        {
            if (tweener.flag.HasFlag(TweenerFlag.Created))
            {
                Debug.LogWarning("Tweener already created! we'll remove it.");
                tweener.flag |= TweenerFlag.Deleting;
                return;
            }

            tweener.flag |= TweenerFlag.Created;

            if (_activeTweeners.Length == _activeTweenersLength)
            {
                Debug.LogWarning(
                    $"maximum capacity reached: automatically increasing " +
                    $"from {_activeTweeners.Length} to {_activeTweeners.Length * 2}");
                var _tmp = new Tweener[_activeTweeners.Length * 2];
                for (int i = 0; i < _activeTweeners.Length; i++) 
                    _tmp[i] = _activeTweeners[i];
                _activeTweeners = _tmp;
            }
            _activeTweeners[_activeTweenersLength++] = tweener;
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