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

        /// <summary>
        /// Executes when the clip plays. it should call to another clip node when finished (see other examples as reference)
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// if true, it'll receive <c>Tick()</c> callback every Update
        /// </summary>
        public virtual bool hasTick() => false;

        /// <summary>
        /// executes every Update time if <c>hasTick()</c> is true
        /// </summary>
        public virtual void Tick() { }

        /// <summary>
        /// Editor-only
        /// </summary>
        public virtual void OnValidate() { }
    }
}
