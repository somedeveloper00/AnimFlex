using System;
using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex
{
	[Serializable]
	public class TweenerGeneratorLightColor : TweenerGenerator<Light, Color>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimLightColorTo(target, duration, delay, ease, customCurve, proxy);
		}
	}

	[Serializable]
	public class TweenerGeneratorLightIntensity : TweenerGenerator<Light, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimLightIntensityTo(target, duration, delay, ease, customCurve, proxy);
		}
	}

	[Serializable]
	public class TweenerGeneratorLightRange : TweenerGenerator<Light, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimLightRangeTo(target, duration, delay, ease, customCurve, proxy);
		}
	}
}