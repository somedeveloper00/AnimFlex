using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Multi Tweener Position")]
    [Category("Tweener/Multi/Transform/Position")]
    internal class CMultiTweenerPosition : CMultiTweener<Transform, Vector3, MultiTweenerGeneratorPosition> { }
    [DisplayName("Multi Tweener LocalPosition")]
    [Category("Tweener/Multi/Transform/LocalPosition")]
    internal class CMultiTweenerLocalPosition : CMultiTweener<Transform, Vector3, MultiTweenerGeneratorLocalPosition> { }
    [DisplayName("Multi Tweener Rotation")]
    [Category("Tweener/Multi/Transform/Rotation")]
    internal class CMultiTweenerRotation : CMultiTweener<Transform, Vector3, MultiTweenerGeneratorRotation> { }
    [DisplayName("Multi Tweener LocalRotation")]
    [Category("Tweener/Multi/Transform/LocalRotation")]
    internal class CMultiTweenerLocalRotation : CMultiTweener<Transform, Vector3, MultiTweenerGeneratorLocalRotation> { }
    [DisplayName("Multi Tweener Scale")]
    [Category("Tweener/Multi/Transform/Scale")]
    internal class CMultiTweenerScale : CMultiTweener<Transform, Vector3, MultiTweenerGeneratorScale> { }
    [DisplayName("Multi Tweener Transform")]
    [Category("Tweener/Multi/Transform/Transform")]
    internal class CMultiTweenerTransform : CMultiTweener<Transform, Transform, MultiTweenerGeneratorTransform> { }
    [DisplayName("Multi Tweener Fade (Graphic)")]
    [Category("Tweener/Multi/Fade/Graphic")]
    internal class CMultiTweenerFadeGraphic : CMultiTweener<Graphic, float, MultiTweenerGeneratorFadeGraphic> { }
    [DisplayName("Multi Tweener Fade (Renderer)")]
    [Category("Tweener/Multi/Fade/Renderer")]
    internal class CMultiTweenerFadeRenderer : CMultiTweener<Renderer, float, MultiTweenerGeneratorFadeRenderer> { }
    [DisplayName("Multi Tweener Fade (CanvasGroup)")]
    [Category("Tweener/Multi/Fade/CanvasGroup")]
    internal class CMultiTweenerFadeCanvasGroup : CMultiTweener<CanvasGroup, float, MultiTweenerGeneratorFadeCanvasGroup> { }
    [DisplayName("Multi Tweener Color (Graphic)")]
    [Category("Tweener/Multi/Color/Graphic")]
    internal class CMultiTweenerColorGraphic : CMultiTweener<Graphic, Color, MultiTweenerGeneratorColorGraphic> { }
    [DisplayName("Multi Tweener Color (Renderer)")]
    [Category("Tweener/Multi/Color/Renderer")]
    internal class CMultiTweenerColorRenderer : CMultiTweener<Renderer, Color, MultiTweenerGeneratorColorRenderer> { }
}