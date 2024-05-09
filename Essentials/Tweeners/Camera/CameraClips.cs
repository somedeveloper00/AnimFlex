using System.ComponentModel;
using AnimFlex.Sequencer.Clips;
using UnityEngine;

namespace AnimFlex
{
	[DisplayName("Tweener Camera FieldOfView")]
	[Category("Tweener/Camera/Field Of View")]
	public class CTweenerCameraFieldOfView : CTweener<Camera, float, TweenerGeneratorCameraFieldOfView> { }
}