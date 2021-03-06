using UnityEngine;

namespace AnimFlex.Clipper.Clips
{
    [System.ComponentModel.DisplayName("Log")]
    public class CLog : Clip
    {
        public string message;

        protected override void OnStart()
        {
            Debug.Log(message);
            End();
        }
    }
}