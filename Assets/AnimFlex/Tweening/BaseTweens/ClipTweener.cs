using System;
using System.ComponentModel;
using AnimFlex.Clipper;
using UnityEngine;

namespace AnimFlex.Tweening
{
    [Serializable]
    [DisplayName("Tweener")]
    public class ClipTweener : Clip
    {
        public MonoBehaviour target;
        [SerializeField] private GameObjectTweenUtilities.TweenerValues tweenerValues;
        public float duration;
        public Easing easing;

        private Tween _tween;
        protected override void OnStart()
        {
            _tween = target.CreateTween(tweenerValues, duration);
            _tween.easing = easing;
            _tween.Play();
            _tween.AddOnEnd(End);
        }
    }
}