using System;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Wait For Frames")]
    [Category("Misc/Wait Frames")]
    [Serializable]
    public class CWaitFrame : Clip
    {
        [Tooltip("The number of frames to wait before playing the next clip")]
        public VariableFetch<int> frames = new() { value = 1 };
        private int _f = 0;

        protected override void OnStart()
        {
            InjectVariable(ref frames);
            _f = frames.value;
        }

        public override void OnEnd() { }
        public override bool HasTick() => true;
        public override void Tick(float deltaTime)
        {
            if (_f-- <= 0)
            {
                PlayNext();
            }
        }
    }
}