using System.ComponentModel;
using AnimFlex.Tweener;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweener/Multi/Transform/Position")]
    public class CMultiTweenerPosition : CTweener<MultiTweenerGeneratorPosition> { }
    [DisplayName("Tweener/Multi/Transform/LocalPosition")]
    public class CMultiTweenerLocalPosition : CTweener<MultiTweenerGeneratorLocalPosition> { }
    [DisplayName("Tweener/Multi/Transform/Rotation")]
    public class CMultiTweenerRotation : CTweener<MultiTweenerGeneratorRotation> { }
    [DisplayName("Tweener/Multi/Transform/LocalRotation")]
    public class CMultiTweenerLocalRotation : CTweener<MultiTweenerGeneratorLocalRotation> { }
    [DisplayName("Tweener/Multi/Transform/Scale")]
    public class CMultiTweenerScale : CTweener<MultiTweenerGeneratorScale> { }
    [DisplayName("Tweener/Multi/Transform/Transform")]
    public class CMultiTweenerTransform : CTweener<MultiTweenerGeneratorTransform> { }
    [DisplayName("Tweener/Multi/Fade/Graphic")]
    public class CMultiTweenerFadeGraphic : CTweener<MultiTweenerGeneratorFadeGraphic> { }
    [DisplayName("Tweener/Multi/Fade/Renderer")]
    public class CMultiTweenerFadeRenderer : CTweener<MultiTweenerGeneratorFadeRenderer> { }
    [DisplayName("Tweener/Multi/Fade/CanvasGroup")]
    public class CMultiTweenerFadeCanvasGroup : CTweener<MultiTweenerGeneratorFadeCanvasGroup> { }
    [DisplayName("Tweener/Multi/Color/Graphic")]
    public class CMultiTweenerColorGraphic : CTweener<MultiTweenerGeneratorColorGraphic> { }
    [DisplayName("Tweener/Multi/Color/Renderer")]
    public class CMultiTweenerColorRenderer : CTweener<MultiTweenerGeneratorColorRenderer> { }
}