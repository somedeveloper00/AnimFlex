using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Log")]
    [Category("Misc/Log")]
    public sealed class CLog : Clip
    {
        public VariableFetch<string> message;

        protected override void OnStart()
        {
            InjectVariable(ref message);
            Debug.Log(message);
            PlayNext();
        }
        public override void OnEnd() { }
    }
}
