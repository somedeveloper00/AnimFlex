using System.ComponentModel;
using AnimFlex.Tweener;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweener/Multi/Transform/Position")]
    internal class CMultiTweenerPosition : CTweener<MultiTweenerGeneratorPosition> { }
    [DisplayName("Tweener/Multi/Transform/LocalPosition")]
    internal class CMultiTweenerLocalPosition : CTweener<MultiTweenerGeneratorLocalPosition> { }
    [DisplayName("Tweener/Multi/Transform/Rotation")]
    internal class CMultiTweenerRotation : CTweener<MultiTweenerGeneratorRotation> { }
    [DisplayName("Tweener/Multi/Transform/LocalRotation")]
    internal class CMultiTweenerLocalRotation : CTweener<MultiTweenerGeneratorLocalRotation> { }
    [DisplayName("Tweener/Multi/Transform/Scale")]
    internal class CMultiTweenerScale : CTweener<MultiTweenerGeneratorScale> { }
    [DisplayName("Tweener/Multi/Transform/Transform")]
    internal class CMultiTweenerTransform : CTweener<MultiTweenerGeneratorTransform> { }
    [DisplayName("Tweener/Multi/Fade/Graphic")]
    internal class CMultiTweenerFadeGraphic : CTweener<MultiTweenerGeneratorFadeGraphic> { }
    [DisplayName("Tweener/Multi/Fade/Renderer")]
    internal class CMultiTweenerFadeRenderer : CTweener<MultiTweenerGeneratorFadeRenderer> { }
    [DisplayName("Tweener/Multi/Fade/CanvasGroup")]
    internal class CMultiTweenerFadeCanvasGroup : CTweener<MultiTweenerGeneratorFadeCanvasGroup> { }
    [DisplayName("Tweener/Multi/Color/Graphic")]
    internal class CMultiTweenerColorGraphic : CTweener<MultiTweenerGeneratorColorGraphic> { }
    [DisplayName("Tweener/Multi/Color/Renderer")]
    internal class CMultiTweenerColorRenderer : CTweener<MultiTweenerGeneratorColorRenderer> { }
}