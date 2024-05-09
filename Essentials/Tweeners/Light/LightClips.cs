using System.ComponentModel;
using AnimFlex.Sequencer.Clips;
using UnityEngine;

namespace AnimFlex
{
	[DisplayName("Tweener Light Color")]
	[Category("Tweener/Light/Color")]
	public class CTweenerLightColor : CTweener<Light, Color, TweenerGeneratorLightColor> { }

	[DisplayName("Tweener Light Intensity")]
	[Category("Tweener/Light/Intensity")]
	public class CTweenerLightIntensity : CTweener<Light, float, TweenerGeneratorLightIntensity> { }

	[DisplayName("Tweener Light Range")]
	[Category("Tweener/Light/Range")]
	public class CTweenerLightRange : CTweener<Light, float, TweenerGeneratorLightRange> { }
}