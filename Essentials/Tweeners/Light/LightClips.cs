using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
	[DisplayName("Tweener Light Color")]
	[Category("Tweener/Light/Color")]
	public sealed class CTweenerLightColor : CTweener<Light, Color, TweenerGeneratorLightColor> { }

	[DisplayName("Tweener Light Intensity")]
	[Category("Tweener/Light/Intensity")]
	public sealed class CTweenerLightIntensity : CTweener<Light, float, TweenerGeneratorLightIntensity> { }

	[DisplayName("Tweener Light Range")]
	[Category("Tweener/Light/Range")]
	public sealed class CTweenerLightRange : CTweener<Light, float, TweenerGeneratorLightRange> { }
}