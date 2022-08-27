using System;
using AnimFlex.Core;
using UnityEngine;

namespace AnimFlex.Sequencer
{
    internal class SequenceController
    {
        public static SequenceController Instance => AnimFlexCore.Instance.SequenceController;
        
        private Sequence[] _sequences;
        private int _activeSequencesCount = 0; // length of active sequences
        private int _queuedSequencesCount = 0; // sequences queued to be added to the list the next frame

        public SequenceController()
        {
            _sequences = new Sequence[AnimFlexSettings.Instance.sequenceMaxCapacity];
        }

        public void Tick(float deltaTime)
        {
            // adding queued sequences to the active list
            _activeSequencesCount += _queuedSequencesCount;
            _queuedSequencesCount = 0;

            init_phase:
            for (int i = 0; i < _activeSequencesCount; i++)
            {
                if (!_sequences[i].flags.HasFlag(SequenceFlags.Initialized))
                {
                    _sequences[i].OnPlay();
                }
            }

            tick_phase:
            for (int i = 0; i < _activeSequencesCount; i++)
            {
                if(!_sequences[i].flags.HasFlag(SequenceFlags.Paused))
                    _sequences[i].Tick(deltaTime);
            }
            
            remove_phase:
            for (int i = 0; i < _activeSequencesCount; i++)
            {
                if (_sequences[i].flags.HasFlag(SequenceFlags.Deleting))
                {
                    _sequences[i].OnKill();
                    // copy last active in the deleting one
                    _sequences[i] = _sequences[_activeSequencesCount - 1];
                    _activeSequencesCount--;

                    // copy last queued to the last copied one
                    _sequences[_activeSequencesCount] = _sequences[_activeSequencesCount + _queuedSequencesCount];
                }
            }
        }

        public void AddSequence(Sequence sequence)
        {
            if (sequence == null)
                throw new NullReferenceException(sequence.ToString());
            
            // capacity check
            if (_sequences.Length <= _activeSequencesCount + _queuedSequencesCount - 1)
            {
                Debug.LogWarning(
                    $"Sequences capacity reached: " +
                    $"automatically increasing capacity from {_sequences.Length} " +
                    $"to {_sequences.Length * 2}");
                var _tmp = new Sequence[_sequences.Length * 2];
                for (int i = 0; i < _sequences.Length; i++) _tmp[i] = _sequences[i];
                _sequences = _tmp;
            }

            _sequences[_activeSequencesCount + _queuedSequencesCount++] = sequence;
        }

        public void RemoveSequence(Sequence sequence)
        {
            for (int i = 0; i < _activeSequencesCount; i++)
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