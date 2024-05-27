using System;
using AnimFlex.Core.Proxy;
using UnityEngine.UI;

namespace AnimFlex.Tweening
{
	[Serializable]
	public sealed class TweenerGeneratorImageFill : TweenerGenerator<Image, float>
	{
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) => fromObject.AnimImageFillTo(target, duration, delay, ease, customCurve, proxy);
    }
}