using System;
using AnimFlex.Core.Proxy;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
	[Serializable]
	public sealed class TweenerGeneratorTMP_NumberInt : TweenerGenerator<TMPro.TMP_Text, int>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy) => fromObject.AnimNumberToInt(target, duration, delay, ease, customCurve, proxy);
	}

	[Serializable]
	public sealed class TweenerGeneratorTMP_Number : TweenerGenerator<TMPro.TMP_Text, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy) => fromObject.AnimNumberTo(target, duration, delay, ease, customCurve, proxy);
	}

	[Serializable]
	public sealed class TweenerGeneratorTMP_Text : TweenerGenerator<TMPro.TMP_Text, string>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy) => fromObject.AnimTextTo(target, duration, delay, ease, customCurve, proxy);
	}

	[Serializable]
	public sealed class TweenerGeneratorUiText : TweenerGenerator<Text, string>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy) => fromObject.AnimTextTo(target, duration, delay, ease, customCurve, proxy);
	}
}