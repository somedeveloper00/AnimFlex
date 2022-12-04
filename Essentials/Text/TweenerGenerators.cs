using System;
using AnimFlex.Essentials.TMP;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex
{
	[Serializable]
	public class TweenerGeneratorTMP_Text : TweenerGenerator<TMPro.TMP_Text, string>
	{
		protected override Tweener GenerateTween(AnimationCurve curve) {
			return fromObject.AnimTextTo( target, ease, duration, delay, curve );
		}
	}

	[Serializable]
	public class TweenerGeneratorUiText : TweenerGenerator<Text, string>
	{
		protected override Tweener GenerateTween(AnimationCurve curve) {
			return fromObject.AnimTextTo( target, ease, duration, delay, curve );
		}
	}
}