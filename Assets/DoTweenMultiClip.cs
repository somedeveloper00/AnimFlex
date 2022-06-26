using AnimFlex.Clipper;
using DG.Tweening;
using System.ComponentModel;
using UnityEngine;

namespace Assets
{
    [DisplayName("DoTweens/Multi-Clip")]
    public class DoTweenMultiClip : Clip
    {
        public DOTweenAnimation[] tweenComponent;

        [Min(0)]
        [Tooltip("If 0, it will not override duration")]
        public float overrideDuration;
        public float delay;


        protected override void OnStart()
        {
            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i < tweenComponent.Length; i++)
            {
                if (overrideDuration != -1 && overrideDuration >= 0)
                    tweenComponent[i].duration = overrideDuration;
                tweenComponent[i].CreateTween();
                var tween = tweenComponent[i].tween;
                sequence.Insert(delay * i, tween);
            }
            sequence.Play();
            sequence.OnComplete(End);
        }
    }
}