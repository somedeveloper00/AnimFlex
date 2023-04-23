using System.ComponentModel;
using AnimFlex.Essentials.TMP;
using AnimFlex.Sequencer.Clips;

namespace AnimFlex
{
	[DisplayName("Tweener UI Text")]
	[Category("Tweener/Text/UI")]
	public class CTweenerUiText : CTweener<TweenerGeneratorUiText> { }
	
	[DisplayName("Tweener Textmesh Text")]
	[Category("Tweener/Text/Textmesh")]
	public class CTweenerTMP_Text : CTweener<TweenerGeneratorTMP_Text> { }
}