using System.ComponentModel;
using AnimFlex.Tweener;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweener/Transform/Position")]
    public class CTweenerPosition : CTweener<TweenerGeneratorPosition> { }
    [DisplayName("Tweener/Transform/LocalPosition")]
    public class CTweenerLocalPosition : CTweener<TweenerGeneratorLocalPosition> { }
    [DisplayName("Tweener/Transform/Rotation")]
    public class CTweenerRotation : CTweener<TweenerGeneratorRotation> { }
    [DisplayName("Tweener/Transform/LocalRotation")]
    public class CTweenerLocalRotation : CTweener<TweenerGeneratorLocalRotation> { }
    [DisplayName("Tweener/Transform/Scale")]
    public class CTweenerScale : CTweener<TweenerGeneratorScale> { }
    [DisplayName("Tweener/Transform/Transform")]
    public class CTweenerTransform : CTweener<TweenerGeneratorTransform> { }
    [DisplayName("Tweener/Fade/Graphic")]
    public class CTweenerFadeGraphic : CTweener<TweenerGeneratorFadeGraphic> { }
    [DisplayName("Tweener/Fade/Renderer")]
    public class CTweenerFadeRenderer : CTweener<TweenerGeneratorFadeRenderer> { }
    [DisplayName("Tweener/Fade/CanvasGroup")]
    public class CTweenerFadeCanvasGroup : CTweener<TweenerGeneratorFadeCanvasGroup> { }
    [DisplayName("Tweener/Color/Graphic")]
    public class CTweenerColorGraphic : CTweener<TweenerGeneratorColorGraphic> { }
    [DisplayName("Tweener/Color/Renderer")]
    public class CTweenerColorRenderer : CTweener<TweenerGeneratorColorRenderer> { }
}