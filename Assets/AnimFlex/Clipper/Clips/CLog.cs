using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Clipper.Clips
{
    [DisplayName("Log")]
    public class CLog : Clip
    {
        public string Message;

        protected override void OnStart()
        {
            Debug.Log(Message);
            End();
        }
    }
}