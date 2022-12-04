using AnimFlex;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public static class LightTweenerExtentions
{

	public static Tweener<Color> AnimLightColorTo(this Light light, Color color, AnimationCurve curve,
		float duration = 1, float delay = 0) =>
		AnimLightColorTo(light, color, Ease.Linear, duration, delay, curve);

	public static Tweener<Color> AnimLightColorTo(this Light light, Color color, Ease ease = Ease.InOutSine,
		float duration = 1, float delay = 0) =>
		AnimLightColorTo(light, color, ease, duration, delay, null);
	
	public static Tweener<Color> AnimLightColorTo(this Light light, Color color, Ease ease = Ease.InOutSine,
		float duration = 1, float delay = 0, AnimationCurve curve = null)
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

	public static Tweener<float> AnimLightIntensityTo(this Light light, float intensity, Ease ease = Ease.InOutSine,
		float duration = 1, float delay = 0, AnimationCurve curve = null)
	{
		return Tweener.Generate(
			() => light.intensity,
			(value) => light.intensity = value,
			intensity, ease, duration, delay, curve,
			() => light != null);
	}

	public static Tweener<float> AnimLightRangeTo(this Light light, float range, AnimationCurve curve,
		float duration = 1, float delay = 0) =>
		AnimLightRangeTo(light, range, Ease.Linear, duration, delay, curve);

	public static Tweener<float> AnimLightRangeTo(this Light light, float range, Ease ease = Ease.InOutSine,
		float duration = 1, float delay = 0) =>
		AnimLightRangeTo(light, range, ease, duration, delay, null);

	public static Tweener<float> AnimLightRangeTo(this Light light, float range, Ease ease = Ease.InOutSine,
		float duration = 1, float delay = 0, AnimationCurve curve = null)
	{
		return Tweener.Generate(
			() => light.range,
			(value) => light.range = value,
			range, ease, duration, delay, curve,
			() => light != null);
	}
}