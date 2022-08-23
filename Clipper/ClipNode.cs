using System;
using UnityEngine;

namespace AnimFlex.Clipper
{
    [Serializable]
    public class ClipNode
    {
#if UNITY_EDITOR
        [SerializeField] private Color inspectorColor;
        [SerializeField] internal string groupName;
#endif
        public string name;
        public float delay;
        [SerializeReference] public Clip clip;

        public bool playNextAfterFinish = true;
        public int[] nextIndices = Array.Empty<int>();

        internal void Play(Action onEndCallback)
        {
            clip.Play(onEndCallback);
        }
    }
}