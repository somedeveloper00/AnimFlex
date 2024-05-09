using System.ComponentModel;
using AnimFlex.Sequencer.Clips;
using UnityEngine.UI;

namespace AnimFlex
{
	[DisplayName("Tweener Image Fill")]
	[Category("Tweener/Image/Fill")]
	public class CTweenerImageFill : CTweener<Image, float, TweenerGeneratorImageFill> { }
}