using System;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    /// <summary>
    /// Used for <see cref="Clip"/>s that need node selection. It provides a suitable drawer.
    /// </summary>
    [Serializable]
    public struct NodeSelection
    {
        [InspectorName("Node")] public byte index;
    }
}