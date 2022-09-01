using System;
using System.Linq;

namespace AnimFlex.Sequencer
{
    public sealed partial class Sequence
    {
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

        public void Pause() => flags |= SequenceFlags.Paused;
        public void Resume() => flags &= ~SequenceFlags.Paused;
        internal void Complete() => flags |= SequenceFlags.Deleting;
    }
}