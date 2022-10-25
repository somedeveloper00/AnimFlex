using System.ComponentModel;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Empty")]
    [Category("Misc/Empty")]
    public class CEmpty : Clip
    {
        protected override void OnStart()
        {
            PlayNext();
        }
        public override void OnEnd() { }
    }
}
