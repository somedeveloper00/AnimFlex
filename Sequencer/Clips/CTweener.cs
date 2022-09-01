using AnimFlex.Tweener;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    public abstract class CTweener : Clip { }

    public abstract class CTweener<T> : CTweener where T : TweenerGenerator
    {
        [Tooltip("If checked, it'll play the next clip right away. otherwise it'll wait for the tween to finish")]
        public bool playNextOnStart = false;
        public T tweenerGenerator;
        
        protected override void OnStart()
        {
            if (playNextOnStart)
                PlayNext();

            if (tweenerGenerator.TryGenerateTween(out var tweener))
            {
                if (!playNextOnStart)
                    tweener.onComplete += PlayNext;
            }
            else
            {
                Debug.LogError($"Could not generate tweener! playing the next clip...");
                PlayNext();
            }
        }
    }
}