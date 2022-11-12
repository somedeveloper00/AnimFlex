using System;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.UserEnd
{
    [AddComponentMenu("AnimFlex/Sequencer")]
    public class SequenceAnim : MonoBehaviour
    {
	    [Tooltip("Plays the sequence everytime the game object gets enabled or created.")]
        [SerializeField] internal bool playOnEnable = true;

	    [Tooltip("Whether or not to reset the sequence before re-starting it.\n" +
	             "If you decide to reset on play, the previous state of the sequencer will be terminated immediately on restart, " +
	             "and the terminating Clip will be presumed completed.\n" +
	             "However if you decide to not reset on play, the previous state of the sequencer will continue updating itself. Be warned " +
	             "that each Clip has only ONE instance, and if the *new* (restarted) sequencer trys to play a Clip that's already being played, " +
	             "the Clip's progress will be reset; the newest call to a Clip will always be most respected.+")]
        [SerializeField] internal bool resetOnPlay = true;

        public Sequence sequence = new Sequence();

        private void OnEnable()
        {
	        if (playOnEnable)
	        {
		        PlaySequence();
	        }
        }

        private void OnDisable()
        {
	        if(sequence.IsActive())
				sequence.Stop();
        }

        private void OnValidate()
        {
            sequence.EditorValidate();
        }

        public void PlaySequence()
        {
	        sequence.PlayOrRestart();
        }
    }
}

