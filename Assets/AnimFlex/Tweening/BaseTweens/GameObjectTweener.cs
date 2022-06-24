using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
    public sealed class GameObjectTweener : MonoBehaviour
    {
        [SerializeField] private GameObjectTweenUtilities.TweenerValues tweenerValues;
        public float duration;
        public float delay;
        public bool playOnStart;
        public Easing easing;

        private Tween _tween;

        private void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        public void Play()
        {
            if (_tween != null && !_tween.IsFinished)
            {
                _tween.ForceEnd();
            }

            Debug.Log($"started at {DateTime.Now.Second} : {DateTime.Now.Millisecond}");
            _tween = this.CreateTween(tweenerValues, duration);
            _tween.delay = delay;
            _tween.easing = easing;
            _tween.AddOnEnd(() =>
            {
                Debug.Log($"ended at {DateTime.Now.Second} : {DateTime.Now.Millisecond}");
            });
            _tween.Play();
        }
    }
}