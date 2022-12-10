using System;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex
{
	[Serializable]
	public class TweenerGeneratorImageFill : TweenerGenerator<Image, float>
	{
		protected override Tweener GenerateTween(AnimationCurve curve) {
			return fromObject.AnimImageFillTo( target, ease, duration, delay, curve );
		}
	}
}