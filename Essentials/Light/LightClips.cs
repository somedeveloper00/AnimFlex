using System.ComponentModel;
using AnimFlex.Sequencer.Clips;

namespace AnimFlex
{
	[DisplayName("Tweener Light Color")]
	[Category("Tweener/Light/Color")]
	public class CTweenerLightColor : CTweener<TweenerGeneratorLightColor> { }

	[DisplayName("Tweener Light Intensity")]
	[Category("Tweener/Light/Intensity")]
	public class CTweenerLightIntensity : CTweener<TweenerGeneratorLightIntensity> { }

	[DisplayName("Tweener Light Range")]
	[Category("Tweener/Light/Range")]
	public class CTweenerLightRange : CTweener<TweenerGeneratorLightRange> { }
}