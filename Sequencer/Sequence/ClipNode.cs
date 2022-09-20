using System;
using UnityEngine;

namespace AnimFlex.Sequencer
{
    [Flags]
    public enum ClipNodeFlags
    {
        PendingActive = 1 << 0,
        Active = 1 << 1,
        PendingDeactive = 1 << 2
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
        internal ClipNodeFlags flags;
        private bool started = false;

        public void Deactivate() => sequence.DeactivateClipNode(this);
        public void PlayNextClipNode()
        {
            if(sequence.nodes.Length == Index + 1)
                sequence.Stop();
            else
                PlayClipNode(Index + 1);
        }

        public void PlayClipNode (int index) => sequence.ActivateClip(index);


        internal void Init(Sequence sequence, int index)
        {
            Index = index;
            this.sequence = sequence;
            clip.Init(this);
        }

        internal void Reset()
        {
            t = 0;
            started = false;
        }

        internal void Tick(float deltaTime)
        {
            t += deltaTime;
            if (t > delay)
            {
                if (!started)
                {
                    // start of the clip
                    clip.Play();
                    started = true;
                }
                else
                {
                    // update/tick of the clip
                    if(clip.hasTick())
                        clip.Tick();
                }
            }
        }

        internal void OnValidate()
        {
            clip.OnValidate();
        }
    }
}
