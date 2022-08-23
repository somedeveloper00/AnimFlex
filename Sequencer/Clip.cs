using System;

namespace AnimFlex.Sequencer
{
    [Serializable]
    public abstract class Clip
    {
        private Action _onEndCallback;
        protected abstract void OnStart();

        public void Play(Action onEndCallback)
        {
            _onEndCallback = onEndCallback;
            OnStart();
        }

        public void End()
        {
            _onEndCallback();
        }
    }
}