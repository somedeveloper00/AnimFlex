using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine.UI;

namespace AnimFlex.Sequencer.Clips
{
	[DisplayName("Tweener Textmesh Text Number Int")]
	[Category("Tweener/Text/Textmesh")]
	public sealed class CTweenerTMP_NumberInt : CTweener<TMPro.TMP_Text, int, TweenerGeneratorTMP_NumberInt> { }

	[DisplayName("Tweener Textmesh Text Number")]
	[Category("Tweener/Text/Textmesh")]
	public sealed class CTweenerTMP_Number : CTweener<TMPro.TMP_Text, float, TweenerGeneratorTMP_Number> { }

	[DisplayName("Tweener Textmesh Text")]
	[Category("Tweener/Text/Textmesh")]
	public sealed class CTweenerTMP_Text : CTweener<TMPro.TMP_Text, string, TweenerGeneratorTMP_Text> { }

	[DisplayName("Tweener UI Text")]
	[Category("Tweener/Text/UI")]
	public sealed class CTweenerUiText : CTweener<Text, string, TweenerGeneratorUiText> { }
}