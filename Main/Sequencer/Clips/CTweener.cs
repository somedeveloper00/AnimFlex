﻿using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    public abstract class CTweener : Clip
    {
        [Tooltip("If checked, it'll play the next clip right away. otherwise it'll wait for the tween to finish")]
        public bool playNextOnStart = false;

        protected Tweener tweener;
        protected bool _dontKill = false;

        internal abstract TweenerGenerator GetTweenerGenerator();

        public override void OnEnd()
        {
            if (!_dontKill && tweener is not null && tweener.IsActive())
            {
                tweener.tweenerController.KillTweener(tweener, true, false);
            }
        }
    }

    public abstract class CTweener<TFrom, TTo, TGen> : CTweener
        where TFrom : Component
        where TGen : TweenerGenerator<TFrom, TTo>
    {
        public TGen tweenerGenerator;
        [HideInInspector] public VariableFetch<TFrom> from;
        [HideInInspector] public VariableFetch<TTo> target;

        internal override TweenerGenerator GetTweenerGenerator() => tweenerGenerator;

        protected override void OnStart()
        {
            InjectVariable(ref from);
            InjectVariable(ref target);
            tweenerGenerator.fromObject = from.value;
            tweenerGenerator.target = target.value;

            if (tweenerGenerator.TryGenerateTween(proxy, out var tweener))
            {
                this.tweener = tweener;
                if (playNextOnStart)
                {
                    _dontKill = true; // so on OnEnd the tweeners won't be killed right away.
                                      // but we still need the Clip to end, so we set endSelf to true to not recceive
                                      // further Ticks (for micro optimization)
                    PlayNext(endSelf: true);
                    _dontKill = false;
                }
                else
                {
                    tweener.onComplete += () => PlayNext();
                }
            }
            else
            {
                Debug.LogWarning($"Could not generate tweener! playing the next clip...");
                PlayNext();
            }
        }
    }

    public abstract class CMultiTweener<TFrom, TTo, TGen> : CTweener
        where TFrom : Component
        where TGen : MultiTweenerGenerator<TFrom, TTo>
    {
        public TGen tweenerGenerator;
        [HideInInspector] public VariableFetch<TTo> target;

        internal override TweenerGenerator GetTweenerGenerator() => tweenerGenerator;

        protected override void OnStart()
        {
            InjectVariable(ref target);
            tweenerGenerator.target = target.value;

            if (tweenerGenerator.TryGenerateTween(proxy, out var tweener))
            {
                this.tweener = tweener;
                if (playNextOnStart)
                {
                    _dontKill = true; // so on OnEnd the tweeners won't be killed right away.
                                      // but we still need the Clip to end, so we set endSelf to true to not recceive
                                      // further Ticks (for micro optimization)
                    PlayNext(endSelf: true);
                    _dontKill = false;
                }
                else
                {
                    tweener.onComplete += () => PlayNext();
                }
            }
            else
            {
                Debug.LogWarning($"Could not generate tweener! playing the next clip...");
                PlayNext();
            }
        }
    }
}