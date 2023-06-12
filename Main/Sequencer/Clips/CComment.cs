using System;
using System.ComponentModel;

namespace AnimFlex.Sequencer.Clips {
    [Serializable]
    [DisplayName("Comment")]
    [Category("Comment")]
    public class CComment : Clip {
#if UNITY_EDITOR
        public string message;
#endif
        protected override void OnStart() => PlayNext();
        public override void OnEnd() { }
    }
}