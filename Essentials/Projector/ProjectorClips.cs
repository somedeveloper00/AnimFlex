using System.ComponentModel;
using AnimFlex.Sequencer.Clips;

namespace AnimFlex.Essentials {
    [DisplayName("Projector Size")]
    [Category("Tweener/Projector/ Size")]
    public class CTweenerProjectorSize : CTweener<TweenerGeneratorProjectorSize> { }
    
    [DisplayName("Projector Aspect Ratio")]
    [Category("Tweener/Projector/ Aspect Ratio")]
    public class CTweenerProjectorAspectRatio : CTweener<TweenerGeneratorProjectorAspectRatio> { }

    [DisplayName("Projector Field Of View")]
    [Category("Tweener/Projector/ Field Of View")]
    public class CTweenerProjectorFieldOfView : CTweener<TweenerGeneratorProjectorFieldOfView> { }
}