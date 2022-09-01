using System;

namespace AnimFlex.Sequencer
{
    [Serializable]
    public abstract class Clip
    {
        [NonSerialized] public ClipNode Node;

        protected void PlayNext() => Node.PlayNextClipNode();
        protected void PlayIndex(int index) => Node.PlayClipNode(index);
        
        internal void Init(ClipNode node)
        {
            Node = node;
        }
        
        internal void Play() => OnStart();

        protected abstract void OnStart();
        public virtual bool hasTick() => false;
        public virtual void Tick() { }
        
        /// <summary>
        /// Editor-only
        /// </summary>
        public virtual void OnValidate() { }
    }
}