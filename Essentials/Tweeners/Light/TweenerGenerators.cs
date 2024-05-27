using System;
using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
	[Serializable]
	public sealed class TweenerGeneratorLightColor : TweenerGenerator<Light, Color>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimLightColorTo(target, duration, delay, ease, customCurve, proxy);
		}
	}

	[Serializable]
	public sealed class TweenerGeneratorLightIntensity : TweenerGenerator<Light, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimLightIntensityTo(target, duration, delay, ease, customCurve, proxy);
		}
	}

	[Serializable]
	public sealed class TweenerGeneratorLightRange : TweenerGenerator<Light, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimLightRangeTo(target, duration, delay, ease, customCurve, proxy);
		}
	}
}