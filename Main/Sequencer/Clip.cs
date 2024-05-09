using System;
using AnimFlex.Core.Proxy;

namespace AnimFlex.Sequencer
{
    [Serializable]
    public abstract class Clip
    {
        [NonSerialized] internal AnimflexCoreProxy proxy;
        [NonSerialized] public ClipNode Node;

        protected void PlayNext() => PlayNext(true);

        protected void PlayNext(bool endSelf)
        {
            Node.PlayNextClipNode();
            if (endSelf)
            {
                Node.End();
            }
        }

        protected void EndSelf() => Node.End();

        protected void PlayIndex(int index) => Node.PlayClipNode(index);

        protected void InjectVariable<T>(ref VariableFetch<T> variable)
        {
            if (variable.IsConstant)
            {
                return;
            }
            Node.InjectVariable(ref variable);
        }

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
        /// If the clip needs to complete immediately, this will be called. this should finish any time sensitive process
        /// immediately as if the process is done.
        /// It should NOT start other nodes.
        /// </summary>
        public abstract void OnEnd();

        /// <summary>
        /// if true, it'll receive <c>Tick()</c> callback every Update
        /// </summary>
        public virtual bool HasTick() => false;

        /// <summary>
        /// executes every Update time if <c>hasTick()</c> is true
        /// </summary>
        public virtual void Tick(float deltaTime) { }

        /// <summary>
        /// Editor-only
        /// </summary>
        public virtual void OnValidate() { }
    }
}
