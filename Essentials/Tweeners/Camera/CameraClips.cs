using System.ComponentModel;
using AnimFlex.Sequencer.Clips;

namespace AnimFlex
{
	[DisplayName( "Tweener Camera FieldOfView" )]
	[Category( "Tweener/Camera/Field Of View" )]
	public class CTweenerCameraFieldOfView : CTweener<TweenerGeneratorCameraFieldOfView> { }
	
	[DisplayName( "Tweener Camera Ortho Size" )]
	[Category( "Tweener/Camera/Ortho Size" )]
	public class CTweenerCameraOrthoSize : CTweener<TweenerGeneratorCameraOrthoSize> { }
}