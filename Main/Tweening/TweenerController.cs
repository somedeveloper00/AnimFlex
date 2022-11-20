using System;
using System.Collections.Generic;
using AnimFlex.Core;
using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Tweening
{
    internal class TweenerController
    {
        public static TweenerController Instance => AnimFlexCore.Instance.TweenerController;

        internal TweenerController()
        {
            _tweeners = new PreservedArray<Tweener>(AnimFlexSettings.Instance.maxTweenCount);
        }

        private PreservedArray<Tweener> _tweeners;
        private List<Tweener> _deletingTweeners = new List<Tweener>(2 * 2 * 2 * 2 * 2 * 2 * 2);

        /// <summary>
        /// updates all active tweens. heart of the tweener
        /// </summary>
        public void Tick(float deltaTime)
        {
#if UNITY_EDITOR
	        Profiler.BeginSample("Tweener Tick");
#endif

            _tweeners.FlushQueueIn(); // flush the array

            // init phase
            for (var i = 0; i < _tweeners.Length; i++)
            {
                var tweener = _tweeners[i];
                if (tweener.flag.HasFlag(TweenerFlag.Initialized) == false)
                {
                    tweener.flag |= TweenerFlag.Initialized;
                    tweener.Init();
                    tweener.OnStart();
                }
            }

			// setter (or tick) phase
            bool _completed = false; // mark for tweener's completion
            for (var i = 0; i < _tweeners.Length; i++)
            {
                var tweener = _tweeners[i];
                var totalTime = tweener.duration + tweener.delay;

                // is not valid, set it as completed
                if (!tweener.IsValid())
                {
                    _completed = true;
                }
                else
                {
                    var t = tweener._t + deltaTime;

                    // apply loop
                    if (tweener.loops != 0 && t >= totalTime)
                    {
                        t %= totalTime;
                        t += tweener.delay - tweener.loopDelay;
                        tweener.loops--;
                    }

                    // to avoid repeated evaluations
                    if (tweener._t == t) continue;

                    tweener._t = t; // save for next Ticks



                    _completed = t >= totalTime; // completion check
                    t = _completed ? 1 : t <= tweener.delay ? 0 : (t - tweener.delay) / tweener.duration; // advanced clamp


                    // apply ping pong
                    if (tweener.pingPong && t != 0)
                    {
                        t *= 2;
                        if (t > 1) t = 2 - t;
                    }

                    tweener.Set(EaseEvaluator.Instance.EvaluateEase(tweener.ease, t, tweener.useCurve ? tweener.customCurve : null));
                    tweener.OnUpdate();
                }

                // check for completion
                if (_completed)
                {
                    tweener.flag |= TweenerFlag.Deleting; // add deletion flag
                    if (tweener.flag.HasFlag(TweenerFlag.ForceNoOnComplete) == false)
                        tweener.OnComplete();
                }
            }

			// deletion phase
            for (var i = 0; i < _tweeners.Length; i++)
            {
                // check if contains a delete flag
                if (_tweeners[i].flag.HasFlag(TweenerFlag.Deleting))
                {
                    _tweeners[i].OnKill();
                    _tweeners.RemoveAt(i--);
                }
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }

        public void AddTweener(Tweener tweener)
        {
            if (tweener.flag.HasFlag(TweenerFlag.Created))
            {
                Debug.LogWarning("Tweener already created! we'll remove it.");
                tweener.flag |= TweenerFlag.Deleting;
                return;
            }

            tweener.flag |= TweenerFlag.Created;

            _tweeners.AddToQueue(tweener);
        }

        public void KillTweener(Tweener tweener, bool complete = true, bool onCompleteCallback = true)
        {
            if (tweener == null)
                throw new NullReferenceException("tweener");
            if (tweener.flag.HasFlag(TweenerFlag.Deleting))
                throw new Exception("Tweener has already been destroyed!");

            tweener.flag |= TweenerFlag.Deleting;

            if (complete)
            {
                // manipulate it's time, and let Tick call it's setter() on the next loop
                tweener._t = tweener.delay + tweener.duration;
            }

            if (onCompleteCallback == false)
            {
                tweener.flag |= TweenerFlag.ForceNoOnComplete;
            }

        }
    }
}
