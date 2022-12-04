using System.ComponentModel;
using AnimFlex.Essentials.TMP;
using AnimFlex.Sequencer.Clips;

namespace AnimFlex
{
	[DisplayName("UI Text")]
	[Category("Tweener/Text/UI")]
	public class CTweenerUiText : CTweener<TweenerGeneratorUiText> { }
	
	[DisplayName("Textmesh Text")]
	[Category("Tweener/Text/Textmesh")]
	public class CTweenerTMP_Text : CTweener<TweenerGeneratorTMP_Text> { }
}