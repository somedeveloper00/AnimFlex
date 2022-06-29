using System;
using System.Linq;

namespace AnimFlex.Clipper
{
    public sealed partial class ClipSequence
    {
        public void RemoveClipNodeAtIndex(int index)
        {
            foreach (var node in nodes)
            {
                for (int i = 0; i < node.nextIndices.Length; i++)
                {
                    if (node.nextIndices[i] > index) node.nextIndices[i]--;
                    if (node.nextIndices[i] == index)
                    {
                        var tmp = node.nextIndices.ToList();
                        tmp.RemoveAt(i);
                        node.nextIndices = tmp.ToArray();
                        i--;
                    }
                }
            }

            var nodesList = nodes.ToList();
            nodesList.RemoveAt(index);
            nodes = nodesList.ToArray();
        }

        public void RemoveClipNode(ClipNode node) => RemoveClipNodeAtIndex(Array.IndexOf(nodes, node));

        public void MoveClipNode(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || nodes.Length <= fromIndex || 
                toIndex < 0 || nodes.Length <= toIndex)
            {
                return;
            }

            (nodes[fromIndex], nodes[toIndex]) = (nodes[toIndex], nodes[fromIndex]);
            foreach (var node in nodes)
            {
                for (var i = 0; i < node.nextIndices.Length; i++)
                {
                    if (node.nextIndices[i] == fromIndex) node.nextIndices[i] = toIndex;
                    if (node.nextIndices[i] == toIndex) node.nextIndices[i] = fromIndex;
                }
            }
        }
        public void AddNewClipNode(Clip clip)
        {
            var tmp = nodes.ToList();
            tmp.Add(new ClipNode()
            {
                clip = clip,
                name = $"Node {nodes.Length}"
            });
            nodes = tmp.ToArray();
        }
    }
}