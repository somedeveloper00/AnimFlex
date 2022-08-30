using System.ComponentModel;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Misc/End")]
    public class CEnd : Clip
    {
        protected override void OnStart()
        {
            Node.sequence.Complete();
        }
    }
}