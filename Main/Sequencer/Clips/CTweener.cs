using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    public abstract class CTweener : Clip { }

    public abstract class CTweener<T> : CTweener, Clip.IHasEnd where T : TweenerGenerator
    {
        [Tooltip("If checked, it'll play the next clip right away. otherwise it'll wait for the tween to finish")]
        public bool playNextOnStart = false;
        public T tweenerGenerator;

        private Tweening.Tweener tweener;

        protected override void OnStart() {
            if ( playNextOnStart )
                PlayNext( false );

            if ( this.tweener != null && this.tweener.IsValid() ) {
                this.tweener.Kill( true, false );
            }

            if (tweenerGenerator.TryGenerateTween(out var tweener)) {
	            this.tweener = tweener;
                if ( !playNextOnStart )
                    tweener.onComplete += () => PlayNext( true );
            }
            else {
                Debug.LogWarning($"Could not generate tweener! playing the next clip...");
                PlayNext();
            }
        }

        public void OnEnd() {
            if ( tweener != null && tweener.IsValid() )
                TweenerController.Instance.KillTweener( tweener, true, false );
        }

    }
}
