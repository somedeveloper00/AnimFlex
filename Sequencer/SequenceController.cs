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
            Profiler.BeginSample("Sequencer Tick");
            _sequences.LetEveryoneIn();

            init_phase:
            for (int i = 0; i < _sequences.Length; i++)
            {
                if (!_sequences[i].flags.HasFlag(SequenceFlags.Initialized))
                {
                    _sequences[i].flags |= SequenceFlags.Initialized;
                    _sequences[i].OnPlay();
                }
            }

            tick_phase:
            for (int i = 0; i < _sequences.Length; i++)
            {
                if (!_sequences[i].flags.HasFlag(SequenceFlags.Paused))
                {
                    _sequences[i].Tick(deltaTime);
                }
            }
            
            remove_phase:
            for (int i = 0; i < _sequences.Length; i++)
            {
                if (_sequences[i].flags.HasFlag(SequenceFlags.Deleting))
                {
                    _sequences[i].OnComplete();
                    _sequences.RemoveAt(i--);
                }
            }
            
            Profiler.EndSample();
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
                    sequence.flags |= SequenceFlags.Deleting;
                    return;
                }
            }
        }
    }
}