using System;

namespace AnimFlex.Sequencer
{
    [Serializable]
    public abstract class Clip
    {
        [NonSerialized] public ClipNode Node;

        protected void PlayNext(bool endSelf = true) => Node.StartNextClipNode( endSelf );
        protected void PlayIndex(int index) => Node.sequence?.StartClip( index );

        internal void Init(ClipNode node) {
            Node = node;
        }

        internal void Start() => OnStart();

        /// <summary>
        /// Executes when the clip plays. it should call to another clip node when finished (see other examples as reference)
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Editor-only
        /// </summary>
        public virtual void OnValidate() { }

        
#region Optional Interfaces

        /// <summary>
        /// clip node gets callback every Update
        /// </summary>
        public interface IHasTick
        {
            /// <summary>
            /// executes every Update time if <c>hasTick()</c> is true
            /// </summary>
            public void Tick(float deltaTime);
        }
        
        /// <summary>
        /// clip node gets callback on end
        /// </summary>
        public interface IHasEnd
        {
            /// <summary>
            /// executes when the clip is actually going to end (during end phase from <see cref="Sequence"/>
            /// </summary>
            public void OnEnd();
        }

#endregion
    }
    
}
