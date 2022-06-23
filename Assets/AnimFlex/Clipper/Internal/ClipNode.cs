using System;
using UnityEngine;

namespace AnimFlex.Clipper.Internal
{
    [Serializable]
    public class ClipNode
    {
        public string name;
        public float delay;
        [SerializeReference] public Clip clip;

        public bool playNextAfterFinish = true;
        public int[] nextIndices;

        internal void Play(Action onEndCallback)
        {
            clip.Play(onEndCallback);
        }
    }
}