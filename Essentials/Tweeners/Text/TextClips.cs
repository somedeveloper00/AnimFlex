using System.ComponentModel;
using AnimFlex.Sequencer.Clips;
using UnityEngine.UI;

namespace AnimFlex
{
	[DisplayName("Tweener Textmesh Text")]
	[Category("Tweener/Text/Textmesh")]
	public class CTweenerTMP_Text : CTweener<TMPro.TMP_Text, string, TweenerGeneratorTMP_Text> { }

	[DisplayName("Tweener UI Text")]
	[Category("Tweener/Text/UI")]
	public class CTweenerUiText : CTweener<Text, string, TweenerGeneratorUiText> { }
}