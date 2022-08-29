using System;
using UnityEngine;

namespace AnimFlex.Sequencer
{
    [Serializable]
    public class ClipNode
    {
        public string name;
        public float delay;
        [SerializeReference] public Clip clip;

        public bool playNextAfterFinish = true;
        public int[] nextIndices = Array.Empty<int>();

        internal void Play(Action onEndCallback)
        {
            clip.Play(onEndCallback);
        }

        internal void OnValidate()
        {
            clip.OnValidate();
        }
    }
}