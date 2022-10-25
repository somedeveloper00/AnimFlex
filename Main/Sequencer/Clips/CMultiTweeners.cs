using System.ComponentModel;
using AnimFlex.Tweening;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Multi Tweener Position")]
    [Category("Tweener/Multi/Transform/Position")]
    internal class CMultiTweenerPosition : CTweener<MultiTweenerGeneratorPosition> { }
    [DisplayName("Multi Tweener LocalPosition")]
    [Category("Tweener/Multi/Transform/LocalPosition")]
    internal class CMultiTweenerLocalPosition : CTweener<MultiTweenerGeneratorLocalPosition> { }
    [DisplayName("Multi Tweener Rotation")]
    [Category("Tweener/Multi/Transform/Rotation")]
    internal class CMultiTweenerRotation : CTweener<MultiTweenerGeneratorRotation> { }
    [DisplayName("Multi Tweener LocalRotation")]
    [Category("Tweener/Multi/Transform/LocalRotation")]
    internal class CMultiTweenerLocalRotation : CTweener<MultiTweenerGeneratorLocalRotation> { }
    [DisplayName("Multi Tweener Scale")]
    [Category("Tweener/Multi/Transform/Scale")]
    internal class CMultiTweenerScale : CTweener<MultiTweenerGeneratorScale> { }
    [DisplayName("Multi Tweener Transform")]
    [Category("Tweener/Multi/Transform/Transform")]
    internal class CMultiTweenerTransform : CTweener<MultiTweenerGeneratorTransform> { }
    [DisplayName("Multi Tweener Fade (Graphic)")]
    [Category("Tweener/Multi/Fade/Graphic")]
    internal class CMultiTweenerFadeGraphic : CTweener<MultiTweenerGeneratorFadeGraphic> { }
    [DisplayName("Multi Tweener Fade (Renderer)")]
    [Category("Tweener/Multi/Fade/Renderer")]
    internal class CMultiTweenerFadeRenderer : CTweener<MultiTweenerGeneratorFadeRenderer> { }
    [DisplayName("Multi Tweener Fade (CanvasGroup)")]
    [Category("Tweener/Multi/Fade/CanvasGroup")]
    internal class CMultiTweenerFadeCanvasGroup : CTweener<MultiTweenerGeneratorFadeCanvasGroup> { }
    [DisplayName("Multi Tweener Color (Graphic)")]
    [Category("Tweener/Multi/Color/Graphic")]
    internal class CMultiTweenerColorGraphic : CTweener<MultiTweenerGeneratorColorGraphic> { }
    [DisplayName("Multi Tweener Color (Renderer)")]
    [Category("Tweener/Multi/Color/Renderer")]
    internal class CMultiTweenerColorRenderer : CTweener<MultiTweenerGeneratorColorRenderer> { }
}