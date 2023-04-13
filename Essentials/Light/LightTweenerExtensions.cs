using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex
{
	public static class LightTweenerExtensions
	{

		public static Tweener<Color> AnimLightColorTo(this Light light, Color color, AnimationCurve curve,
			float duration = 1, float delay = 0) =>
			AnimLightColorTo(light, color, Ease.Linear, duration, delay, curve);

		public static Tweener<Color> AnimLightColorTo(this Light light, Color color, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0) =>
			AnimLightColorTo(light, color, ease, duration, delay, null);
	
		public static Tweener<Color> AnimLightColorTo(this Light light, Color color, Ease ease,
			float duration, float delay, AnimationCurve curve)
		{
			return Tweener.Generate(
				() => light.color,
				(value) => light.color = value,
				color, ease, duration, delay, curve,
				() => light != null);
		}


		public static Tweener<float> AnimLightIntensityTo(this Light light, float intensity, AnimationCurve curve,
			float duration = 1, float delay = 0) =>
			AnimLightIntensityTo(light, intensity, Ease.Linear, duration, delay, curve);

		public static Tweener<float> AnimLightIntensityTo(this Light light, float intensity, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0) =>
			AnimLightIntensityTo(light, intensity, ease, duration, delay, null);

		public static Tweener<float> AnimLightIntensityTo(this Light light, float intensity, Ease ease,
			float duration, float delay, AnimationCurve curve)
		{
			return Tweener.Generate(
				() => light.intensity,
				(value) => light.intensity = value,
				intensity, duration: duration, delay: delay, ease: ease,
				customCurve: curve, isValid: () => light != null );
		}

		public static Tweener<float> AnimLightRangeTo(this Light light, float range, AnimationCurve curve,
			float duration = 1, float delay = 0) =>
			AnimLightRangeTo(light, range, Ease.Linear, duration, delay, curve);

		public static Tweener<float> AnimLightRangeTo(this Light light, float range, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0) =>
			AnimLightRangeTo(light, range, ease, duration, delay, null);

		public static Tweener<float> AnimLightRangeTo(this Light light, float range, Ease ease,
			float duration, float delay, AnimationCurve curve)
		{
			return Tweener.Generate(
				() => light.range,
				(value) => light.range = value,
				range, duration: duration, delay: delay, ease: ease,
				customCurve: curve, isValid: () => light != null );
		}
	}
}