using System;
using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine.UI;

namespace AnimFlex
{
	[Serializable]
	public class TweenerGeneratorImageFill : TweenerGenerator<Image, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimImageFillTo(target, duration, delay, ease, customCurve, proxy);
		}
	}
}