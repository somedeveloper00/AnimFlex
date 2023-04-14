using AnimFlex.Sequencer;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Core
{
    /// <summary>
    /// the first entry point for AnimFlex functionality. NOT TO BE USED MANUALLY
    /// </summary>
    internal sealed class AnimFlexCore : MonoBehaviour {
        public AnimFlexSettings Settings { get; private set; } = AnimFlexSettings.Instance;
        public EaseEvaluator EaseEvaluator { get; } = new EaseEvaluator();
        public TweenerController TweenerController { get; } = new TweenerController();
        public SequenceController SequenceController { get; } = new SequenceController();

        public void Tick(float deltaTime) {
            SequenceController.Tick( deltaTime );
            TweenerController.Tick( deltaTime );
        }
    }
}
