using System;
using System.Collections.Generic;
using AnimFlex.Core;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Tweener
{
    internal class TweenerController
    {
        public static TweenerController Instance => AnimFlexCore.Instance.TweenerController;
        
        internal TweenerController()
        {
            _activeTweeners = new Tweener[AnimFlexSettings.Instance.maxTweenCount];
        }

        private Tweener[] _activeTweeners;
        private int _activeTweenersLength = 0; // real length of active tweens
        private int _newTweenersInQueue = 0; // the amount of tweeners after _activeTweenersLength in queue for the next frame
        private List<Tweener> _deletingTweeners = new List<Tweener>(2 * 2 * 2 * 2 * 2 * 2 * 2);

        /// <summary>
        /// updates all active tweens. heart of the tweener
        /// </summary>
        public void Tick(float deltaTime)
        {
            _activeTweenersLength += _newTweenersInQueue;
            _newTweenersInQueue = 0;
            
            initialize_phase:
            for (var i = 0; i < _activeTweenersLength; i++)
            {
                var tweener = _activeTweeners[i];
                if (tweener.flag.HasFlag(TweenerFlag.Initialized) == false)
                {
                    tweener.flag |= TweenerFlag.Initialized;
                    tweener.Init();
                    tweener.OnStart();
                }
            }

            setter_phase:
            bool _c = false; // mark for tweener's completion
            for (var i = 0; i < _activeTweenersLength; i++)
            {
                var tweener = _activeTweeners[i];
                var totalTime = tweener.duration + tweener.delay;
                
                var t = tweener._t + deltaTime;
                
                // apply loop
                if (tweener.loops > 0 && t >= totalTime)
                {
                    t %= totalTime;
                    t += tweener.delay - tweener.loopDelay;
                    tweener.loops--;
                }
                
                // to avoid repeated evaluations
                if(tweener._t == t) continue;

                tweener._t = t; // save for next Ticks

                
                
                _c = t >= totalTime; // completion check
                t = _c ? 1 : t <= tweener.delay ? 0 : (t - tweener.delay) / tweener.duration; // advanced clamp

                
                // apply ping pong
                if (tweener.pingPong && t != 0)
                {
                    t *= 2;
                    if (t > 1) t = 2 - t;
                }
                

                tweener.Set(EaseEvaluator.Instance.EvaluateEase(tweener.ease, t, tweener.useCurve ? tweener.customCurve : null));
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
                    
                    // paste last active tween here and length--
                    _activeTweeners[i] = _activeTweeners[_activeTweenersLength - 1];
                    _activeTweenersLength--;
                    
                    // paste last new queued tween to the duplicate last active tween and queue length--
                    _activeTweeners[_activeTweenersLength] = _activeTweeners[_activeTweenersLength + _newTweenersInQueue];
                    
                    i--;
                }
            }
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

            if (_activeTweeners.Length == _activeTweenersLength + _newTweenersInQueue)
            {
                Debug.LogWarning(
                    $"maximum capacity reached: automatically increasing " +
                    $"from {_activeTweeners.Length} to {_activeTweeners.Length * 2}");
                var _tmp = new Tweener[_activeTweeners.Length * 2];
                for (int i = 0; i < _activeTweeners.Length; i++) 
                    _tmp[i] = _activeTweeners[i];
                _activeTweeners = _tmp;
            }

            _newTweenersInQueue++;
            _activeTweeners[_activeTweenersLength + _newTweenersInQueue - 1] = tweener;
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