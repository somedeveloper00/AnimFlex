using System;
using UnityEngine;

namespace AnimFlex.Sequencer
{
    [Flags]
    public enum ClipNodeFlags
    {
        PendingActive = 1 << 1,
        Active = 1 << 2,
        PendingDeactive = 1 << 3
    }

    [Serializable]
    public class ClipNode
    {
        [SerializeField] internal string name;
        [SerializeField] internal float delay;
        [SerializeReference] internal Clip clip;

        /// <summary>
        /// index of this clip node inside the Sequencer
        /// </summary>
        public int Index { get; private set; }
        [field: NonSerialized] public Sequence sequence { get; private set; }

        private float t = 0;
        internal ClipNodeFlags flags = 0; // no flags on start
        private bool started = false;

        public void Deactivate() => sequence.DeactivateClipNode(this);
        public void PlayNextClipNode()
        {
	        if(!sequence.IsActive()) return;

            if(sequence.nodes.Length == Index + 1)
                sequence.Stop();
            else
                PlayClipNode(Index + 1);
        }

        public void PlayClipNode (int index)
        {
	        if(!sequence.IsActive()) return;
	        sequence.ActivateClip(index);
        }

        /// <summary>
        /// forcefully ends the clip
        /// </summary>
        internal void End()
        {
			clip.OnEnd();
        }

        /// <summary>
        /// initialization of the clip node. it injects variables
        /// </summary>
        /// <param name="sequence">The sequencer this ClipNode is attached to</param>
        /// <param name="index">The index of this ClipNode in the sequencer's <c>nodes</c> array</param>
        internal void Init(Sequence sequence, int index)
        {
            Index = index;
            this.sequence = sequence;
            clip.Init(this);
            Reset();
        }

        /// <summary>
        /// resets variables. used for re-playing the already played node
        /// </summary>
        internal void Reset()
        {
            t = 0;
            started = false;
        }

        /// <summary>
        /// Updates the ClipNode's Clip.</summary>
        /// <param name="deltaTime">The time interval from the last Tick to now</param>
        internal void Tick(float deltaTime)
        {
            t += deltaTime;
            if (t > delay)
            {
                // start of the clip
                if (!started)
                {
                    started = true; // first set this to true, so if clip.Play() threw errors,
                                    // they won't get executed in the next Tick
                    clip.Play();
                }
                // update/tick of the clip
                else
                {
                    if(clip.hasTick())
                        clip.Tick(deltaTime);
                }
            }
        }

        internal void OnValidate()
        {
            clip.OnValidate();
        }
    }
}
