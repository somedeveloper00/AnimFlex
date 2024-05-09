using System.ComponentModel;
using AnimFlex.Sequencer.Clips;
using UnityEngine;

namespace AnimFlex
{
    [DisplayName("Tweener Projector Size")]
    [Category("Tweener/Projector/ Size")]
    public class CTweenerProjectorSize : CTweener<Projector, float, TweenerGeneratorProjectorSize> { }

    [DisplayName("Tweener Projector Aspect Ratio")]
    [Category("Tweener/Projector/ Aspect Ratio")]
    public class CTweenerProjectorAspectRatio : CTweener<Projector, float, TweenerGeneratorProjectorAspectRatio> { }

    [DisplayName("Tweener Projector Field Of View")]
    [Category("Tweener/Projector/ Field Of View")]
    public class CTweenerProjectorFieldOfView : CTweener<Projector, float, TweenerGeneratorProjectorFieldOfView> { }
}