using System.ComponentModel;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Misc/Empty")]
    public class CEmpty : Clip
    {
        protected override void OnStart()
        {
            PlayNext();
        }
    }
}