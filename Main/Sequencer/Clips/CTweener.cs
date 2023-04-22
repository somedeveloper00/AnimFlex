using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips {
    public abstract class CTweener : Clip {
        [Tooltip( "If checked, it'll play the next clip right away. otherwise it'll wait for the tween to finish" )]
        public bool playNextOnStart = false;

        protected Tweener tweener;

        public override void OnEnd() {
            if (tweener.IsActive()) tweener.tweenerController.KillTweener( tweener, true, false );
        }
    }

    public abstract class CTweener<T> : CTweener where T : TweenerGenerator {
        public T tweenerGenerator;


        protected override void OnStart() {

            if (tweenerGenerator.TryGenerateTween( proxy, out var tweener )) {
                this.tweener = tweener;
                if (playNextOnStart)
                    PlayNext();
                else
                    tweener.onComplete += () => PlayNext();
            }
            else {
                Debug.LogWarning( $"Could not generate tweener! playing the next clip..." );
                PlayNext();
            }
        }


    }
}