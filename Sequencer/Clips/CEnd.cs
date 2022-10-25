using System.ComponentModel;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("End")]
    [Category("Misc/End")]
    public class CEnd : Clip
    {
        protected override void OnStart()
        {
            Node.sequence.Stop();
        }
        public override void OnEnd() { }
    }
}
