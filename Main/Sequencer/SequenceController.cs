﻿using System;
using AnimFlex.Core;
using UnityEngine;
using UnityEngine.Profiling;

namespace AnimFlex.Sequencer {
    internal class SequenceController {
        
        public event Action delayedCall;

        private PreservedArray<Sequence> _sequences;

        public SequenceController() {
            _sequences = new PreservedArray<Sequence>( AnimFlexSettings.Instance.sequenceMaxCapacity );
        }


        public void Tick(float deltaTime) {
#if UNITY_EDITOR
            Profiler.BeginSample( "Sequencer Tick" );
#endif
            _sequences.FlushQueueIn(); // flush the array

            // init phase
            for (int i = 0; i < _sequences.Length; i++) {
                if (_sequences[i].flags.HasFlagFast( SequenceFlags.Active ) == false) {
                    _sequences[i].flags |= SequenceFlags.Active;
                    _sequences[i].OnActivate();
                }
            }

            // tick phase
            for (int i = 0; i < _sequences.Length; i++) {
                if (!_sequences[i].flags.HasFlagFast( SequenceFlags.Paused )) {
                    _sequences[i].Tick( deltaTime );
                }
            }

            // remove phase
            for (int i = 0; i < _sequences.Length; i++) {
                if (_sequences[i].flags.HasFlagFast( SequenceFlags.Stopping )) {
                    _sequences[i].OnStop();
                    _sequences.RemoveAt( i-- );
                }
            }

            delayedCall?.Invoke();
            delayedCall = null;

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }

        public void AddNewSequence(Sequence sequence, bool dontWaitInQueueToPlay) {
            if (sequence == null)
            {
                throw new NullReferenceException( "sequence" );
            }

            if (sequence.flags.HasFlagFast( SequenceFlags.Active )) {
                Debug.LogError( $"Sequence was already active. You should stop it before playing it again." );
                return;
            }

            if (dontWaitInQueueToPlay) {
                sequence.flags |= SequenceFlags.Active;
                sequence.OnActivate();
                sequence.Tick( 0 );
            }
            
            _sequences.AddToQueue( sequence );
        }

        public void RemoveSequence(Sequence sequence) {
            for (int i = 0; i < _sequences.Length; i++) {
                if (_sequences[i] == sequence) {
                    sequence.flags |= SequenceFlags.Stopping;
                    return;
                }
            }
        }
    }
}
