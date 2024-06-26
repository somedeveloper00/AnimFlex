﻿using System;
using AnimFlex.Core;
using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Tweening
{
    public sealed class TweenerController
    {
        internal TweenerController()
        {
            _tweeners = new PreservedArray<Tweener>(AnimFlexSettings.Instance.maxTweenCount);
        }

        private PreservedArray<Tweener> _tweeners;

        /// <summary>
        /// updates all active tweens. heart of the tweener
        /// </summary>
        internal void Tick(float deltaTime)
        {
#if UNITY_EDITOR
            Profiler.BeginSample("Tweener Tick");
#endif
            _tweeners.FlushQueueIn(); // flush the array

            // init phase
            for (var i = 0; i < _tweeners.Length; i++)
            {
                var tweener = _tweeners[i];
                if (tweener.flag.HasFlagFast(TweenerFlag.Initialized) == false)
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
                    // if (tweener._t == t) continue;

                    tweener._t = t; // save for next Ticks

                    _completed = t >= totalTime; // completion check
                    t = _completed ? 1
                        : t <= tweener.delay
                        ? 0 : (t - tweener.delay) / tweener.duration; // advanced clamp


                    // apply ping pong
                    if (tweener.pingPong && t != 0)
                    {
                        t = -2 * Mathf.Abs(t - 0.5f) + 1;
                    }

                    try
                    {
                        tweener.Set(EaseEvaluator.Instance.EvaluateEase(tweener.ease, t, tweener.useCurve ? tweener.customCurve : null));
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }

                // check for completion
                if (_completed)
                {
                    tweener.flag |= TweenerFlag.Deleting; // add deletion flag
                    if (tweener.flag.HasFlagFast(TweenerFlag.ForceNoOnComplete) == false)
                    {
                        tweener.OnComplete();
                    }
                }
            }

            // deletion phase
            for (var i = 0; i < _tweeners.Length; i++)
            {
                // check if contains a delete flag
                if (_tweeners[i].flag.HasFlagFast(TweenerFlag.Deleting))
                {
                    _tweeners[i].OnKill();
                    _tweeners.RemoveAt(i--);
                }
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }

        internal void AddTweener(Tweener tweener)
        {
            if (tweener.flag.HasFlagFast(TweenerFlag.Created))
            {
                Debug.LogWarning("Tweener already created! we'll remove it.");
                tweener.flag |= TweenerFlag.Deleting;
                return;
            }

            tweener.flag |= TweenerFlag.Created;

            _tweeners.AddToQueue(tweener);
        }

        internal void KillTweener(Tweener tweener, bool complete = true, bool onCompleteCallback = true)
        {
            if (tweener == null)
            {
                throw new NullReferenceException("tweener");
            }

            if (tweener.flag.HasFlagFast(TweenerFlag.Deleting))
            {
                throw new Exception("Tweener has already been destroyed!");
            }

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