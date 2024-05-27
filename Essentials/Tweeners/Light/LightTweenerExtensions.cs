using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
	public static class LightTweenerExtensions
	{
		public static Tweener<Color> AnimLightColorTo(this Light light, Color color, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimLightColorTo(light, color, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<Color> AnimLightColorTo(this Light light, Color color, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimLightColorTo(light, color, duration, delay, ease, null, proxy);

		public static Tweener<Color> AnimLightColorTo(this Light light, Color color, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
			return Tweener.Generate(
				() => light.color,
				(value) => light.color = value,
				color, duration, delay, ease, curve,
				() => light != null, proxy);
		}


		public static Tweener<float> AnimLightIntensityTo(this Light light, float intensity, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimLightIntensityTo(light, intensity, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<float> AnimLightIntensityTo(this Light light, float intensity, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimLightIntensityTo(light, intensity, duration, delay, ease, null, proxy);

		public static Tweener<float> AnimLightIntensityTo(this Light light, float intensity, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
			return Tweener.Generate(
				() => light.intensity,
				(value) => light.intensity = value,
				intensity, duration, delay, ease,
				curve, () => light != null, proxy);
		}

		public static Tweener<float> AnimLightRangeTo(this Light light, float range, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimLightRangeTo(light, range, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<float> AnimLightRangeTo(this Light light, float range, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimLightRangeTo(light, range, duration, delay, ease, null, proxy);

		public static Tweener<float> AnimLightRangeTo(this Light light, float range, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
			return Tweener.Generate(
				() => light.range,
				(value) => light.range = value,
				range, duration, delay, ease,
				curve, () => light != null, proxy);
		}
	}
}