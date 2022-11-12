using System.ComponentModel;
using AnimFlex.Sequencer.Clips;

namespace AnimFlex
{
	[DisplayName("Light Color")]
	[Category("Tweener/Light/Color")]
	public class CTweenerLightColor : CTweener<TweenerGeneratorLightColor> { }

	[DisplayName("Light Intensity")]
	[Category("Tweener/Light/Intensity")]
	public class CTweenerLightIntensity : CTweener<TweenerGeneratorLightIntensity> { }

	[DisplayName("Light Range")]
	[Category("Tweener/Light/Range")]
	public class CTweenerLightRange : CTweener<TweenerGeneratorLightRange> { }
}