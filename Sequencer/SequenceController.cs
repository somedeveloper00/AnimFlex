using System;
using AnimFlex.Core;
using UnityEngine.Profiling;

namespace AnimFlex.Sequencer
{
    internal class SequenceController
    {
        public static SequenceController Instance => AnimFlexCore.Instance.SequenceController;

        private PreservedArray<Sequence> _sequences;

        public SequenceController()
        {
            _sequences = new PreservedArray<Sequence>(AnimFlexSettings.Instance.sequenceMaxCapacity);
        }


        public void Tick(float deltaTime)
        {
#if UNITY_EDITOR
	        Profiler.BeginSample("Sequencer Tick");
#endif
            _sequences.LetEveryoneIn(); // flush the array

            // init phase
            for (int i = 0; i < _sequences.Length; i++)
            {
                if (!_sequences[i].flags.HasFlag(SequenceFlags.Initialized))
                {
                    _sequences[i].flags |= SequenceFlags.Initialized;
                    _sequences[i].OnPlay();
                }
            }

            // tick phase
            for (int i = 0; i < _sequences.Length; i++)
            {
                if (!_sequences[i].flags.HasFlag(SequenceFlags.Paused))
                {
                    _sequences[i].Tick(deltaTime);
                }
            }

            // remove phase
            for (int i = 0; i < _sequences.Length; i++)
            {
                if (_sequences[i].flags.HasFlag(SequenceFlags.Stopping))
                {
                    _sequences[i].OnComplete();
                    _sequences.RemoveAt(i--);
                }
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }

        public void AddSequence(Sequence sequence)
        {
            if (sequence == null)
                throw new NullReferenceException("sequence");
            _sequences.AddToQueue(sequence);
        }

        public void RemoveSequence(Sequence sequence)
        {
            for (int i = 0; i < _sequences.Length; i++)
            {
                if (_sequences[i] == sequence)
                {
                    sequence.flags |= SequenceFlags.Stopping;
                    return;
                }
            }
        }
    }
}
