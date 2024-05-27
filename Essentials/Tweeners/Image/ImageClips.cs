using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine.UI;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweener Image Fill")]
	[Category("Tweener/Image/Fill")]
	public sealed class CTweenerImageFill : CTweener<Image, float, TweenerGeneratorImageFill> { }
}