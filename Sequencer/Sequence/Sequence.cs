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
        Stopping = 1 << 2
    }

    [Serializable]
    public sealed partial class Sequence
    {
        [SerializeField] internal ClipNode[] nodes = Array.Empty<ClipNode>();

        public event Action onPlay = delegate { };
        public event Action onComplete = delegate { };

        internal void OnPlay() => onPlay();
        internal void OnComplete()
        {
	        onComplete();
        }

        internal SequenceFlags flags;

#region Public playback tools

        public void Pause() => flags |= SequenceFlags.Paused;

        public void Resume() => flags &= ~SequenceFlags.Paused;

        public void Stop() => flags |= SequenceFlags.Stopping;

        public void Play()
        {
	        if (nodes.Length <= 0) return;
	        for (int i = 0; i < nodes.Length; i++) nodes[i].Init(this, i);
	        SequenceController.Instance.AddSequence(this);
	        ActivateClip(0);
	        OnPlay();
        }

#endregion


#region Internal helpers

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

#endregion

#region Clip manipulations

        public void RemoveClipNodeAtIndex(int index)
        {
	        var nodesList = nodes.ToList();
	        nodesList.RemoveAt(index);
	        nodes = nodesList.ToArray();
        }

        public void RemoveClipNode(ClipNode node)
        {
	        RemoveClipNodeAtIndex(Array.IndexOf(nodes, node));
        }

        public void MoveClipNode(int fromIndex, int toIndex)
        {
	        (nodes[fromIndex], nodes[toIndex]) = (nodes[toIndex], nodes[fromIndex]);
        }

        public void AddNewClipNode(Clip clip)
        {
	        var tmp = nodes.ToList();
	        tmp.Add(new ClipNode
	        {
		        clip = clip,
		        name = $"Node {nodes.Length}"
	        });
	        nodes = tmp.ToArray();
        }

        public void InsertNewClipAt(Clip clip, int index)
        {
	        var tmp = nodes.ToList();
	        tmp.Insert(index, new ClipNode
	        {
		        clip = clip,
		        name = $"Node {index}",
	        });
	        nodes = tmp.ToArray();
        }

#endregion
    }
}
