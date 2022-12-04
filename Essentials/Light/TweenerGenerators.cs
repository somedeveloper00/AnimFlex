using System;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex
{
	[Serializable]
	public class TweenerGeneratorLightColor : TweenerGenerator<Light, Color>
	{
		protected override Tweener GenerateTween(AnimationCurve curve)
		{
			return fromObject.AnimLightColorTo(target, ease, duration, delay, curve);
		}
	}

	[Serializable]
	public class TweenerGeneratorLightIntensity : TweenerGenerator<Light, float>
	{
		protected override Tweener GenerateTween(AnimationCurve curve)
		{
			return fromObject.AnimLightIntensityTo(target, ease, duration, delay, curve);
		}
	}

	[Serializable]
	public class TweenerGeneratorLightRange : TweenerGenerator<Light, float>
	{
		protected override Tweener GenerateTween(AnimationCurve curve)
		{
			return fromObject.AnimLightRangeTo(target, ease, duration, delay, curve);
		}
	}
}