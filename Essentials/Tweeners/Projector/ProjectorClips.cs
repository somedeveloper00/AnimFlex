using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweener Projector Size")]
    [Category("Tweener/Projector/ Size")]
    public sealed class CTweenerProjectorSize : CTweener<Projector, float, TweenerGeneratorProjectorSize> { }

    [DisplayName("Tweener Projector Aspect Ratio")]
    [Category("Tweener/Projector/ Aspect Ratio")]
    public sealed class CTweenerProjectorAspectRatio : CTweener<Projector, float, TweenerGeneratorProjectorAspectRatio> { }

    [DisplayName("Tweener Projector Field Of View")]
    [Category("Tweener/Projector/ Field Of View")]
    public sealed class CTweenerProjectorFieldOfView : CTweener<Projector, float, TweenerGeneratorProjectorFieldOfView> { }
}