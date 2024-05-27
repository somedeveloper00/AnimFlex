using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweener Camera FieldOfView")]
	[Category("Tweener/Camera/Field Of View")]
	public sealed class CTweenerCameraFieldOfView : CTweener<Camera, float, TweenerGeneratorCameraFieldOfView> { }
}