using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimFlex.Sequencer
{
    [Flags]
    internal enum SequenceFlags
    {
        Initialized = 1 << 0, // for OnPlay event
        Paused = 1 << 1,
        Deleting = 1 << 2
    }

    [Serializable]
    public sealed partial class Sequence
    {
        [SerializeField] internal ClipNode[] nodes = Array.Empty<ClipNode>();

        public event Action onPlay = delegate { };
        public event Action onComplete = delegate { };

        internal void OnPlay() => onPlay();
        internal void OnComplete() => onComplete();

        internal SequenceFlags flags;


        public void Play()
        {
            if (nodes.Length <= 0) return;
            for (int i = 0; i < nodes.Length; i++) nodes[i].Init(this, i);
            SequenceController.Instance.AddSequence(this);
            ActivateClip(0);
            OnPlay();
        }

        internal void Tick(float deltaTime)
        {
	        // init phase
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].flags.HasFlag(ClipNodeFlags.PendingActive))
                {
                    nodes[i].Reset();
                    nodes[i].flags = ClipNodeFlags.Active;
                }
            }

            // tick phase
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].flags.HasFlag(ClipNodeFlags.Active))
                {
                    nodes[i].Tick(deltaTime);
                }
            }
        }

        internal void EditorValidate()
        {
            foreach (var node in nodes) node.OnValidate();
        }

        internal void ActivateClip(int index)
        {
            nodes[index].flags = ClipNodeFlags.PendingActive;
        }

        internal void DeactivateClipNode(ClipNode clipNode)
        {
            clipNode.flags = ClipNodeFlags.PendingDeactive;
        }
    }
}
