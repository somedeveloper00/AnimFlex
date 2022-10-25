using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Log")]
    [Category("Log")]
    public class CLog : Clip
    {
        public string message;

        protected override void OnStart()
        {
            Debug.Log(message);
            PlayNext();
        }
        public override void OnEnd() { }
    }
}
