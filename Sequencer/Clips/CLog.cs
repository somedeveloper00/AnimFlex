using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [System.ComponentModel.DisplayName("Log")]
    public class CLog : Clip
    {
        public string message;

        protected override void OnStart()
        {
            End();
        }
    }
}