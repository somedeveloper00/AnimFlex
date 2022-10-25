using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Go to")]
    [Category("Branch/Go to")]
    public class CGoto : Clip
    {
        [Tooltip("The node index to play after this node")]
        public int index;

        protected override void OnStart()
        {
            PlayIndex(index);
        }
        public override void OnEnd() { }
    }
}
