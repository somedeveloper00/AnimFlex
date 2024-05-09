using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweener Position")]
    [Category("Tweener/Transform/Position")]
    public class CTweenerPosition : CTweener<Transform, Vector3, TweenerGeneratorPosition> { }
    [DisplayName("Tweener LocalPosition")]
    [Category("Tweener/Transform/LocalPosition")]
    public class CTweenerLocalPosition : CTweener<Transform, Vector3, TweenerGeneratorLocalPosition> { }
    [DisplayName("Tweener Rotation")]
    [Category("Tweener/Transform/Rotation")]
    public class CTweenerRotation : CTweener<Transform, Vector3, TweenerGeneratorRotation> { }
    [DisplayName("Tweener LocalRotation")]
    [Category("Tweener/Transform/LocalRotation")]
    public class CTweenerLocalRotation : CTweener<Transform, Vector3, TweenerGeneratorLocalRotation> { }
    [DisplayName("Tweener Scale")]
    [Category("Tweener/Transform/Scale")]
    public class CTweenerScale : CTweener<Transform, Vector3, TweenerGeneratorScale> { }
    [DisplayName("Tweener Transform")]
    [Category("Tweener/Transform/Transform")]
    public class CTweenerTransform : CTweener<Transform, Transform, TweenerGeneratorTransform> { }
    [DisplayName("Tweener Fade (Graphic)")]
    [Category("Tweener/Fade/Graphic")]
    public class CTweenerFadeGraphic : CTweener<Graphic, float, TweenerGeneratorFadeGraphic> { }
    [DisplayName("Tweener Fade (Renderer)")]
    [Category("Tweener/Fade/Renderer")]
    public class CTweenerFadeRenderer : CTweener<Renderer, float, TweenerGeneratorFadeRenderer> { }
    [DisplayName("Tweener Fade (CanvasGroup)")]
    [Category("Tweener/Fade/CanvasGroup")]
    public class CTweenerFadeCanvasGroup : CTweener<CanvasGroup, float, TweenerGeneratorFadeCanvasGroup> { }
    [DisplayName("Tweener Color (Graphic)")]
    [Category("Tweener/Color/Graphic")]
    public class CTweenerColorGraphic : CTweener<Graphic, Color, TweenerGeneratorColorGraphic> { }
    [DisplayName("Tweener Color (Renderer)")]
    [Category("Tweener/Color/Renderer")]
    public class CTweenerColorRenderer : CTweener<Renderer, Color, TweenerGeneratorColorRenderer> { }
}