
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Play Sequence")]
    [Category("Misc/Play Sequence")]
    public sealed class CPlaySequence : Clip
    {
        [Tooltip("Whether to wait for the sequence to finish before ending this clip")]
        public bool waitTillFinished = true;

        [Tooltip("Whether to stop the started sequence once this clip is ended.")]
        public bool stopSequenceOnClipEnd = true;

        public VariableFetch<SequenceAnim> sequence;

        protected override void OnStart()
        {
            InjectVariable(ref sequence);
            if (sequence.value)
            {
                sequence.value.sequence.onComplete -= PlayNext; // to erase any previous duplicate
                if (sequence.value.IsPlaying())
                {
                    sequence.value.StopSequence();
                }
                sequence.value.sequence.onComplete += PlayNext;
                sequence.value.PlaySequence();
            }
        }

        public override void OnEnd()
        {
            if (stopSequenceOnClipEnd)
            {
                if (sequence.value.IsPlaying())
                {
                    sequence.value.StopSequence();
                }
            }
        }
    }
}