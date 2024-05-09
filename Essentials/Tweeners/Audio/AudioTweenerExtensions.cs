using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex
{
	public static class AudioTweenerExtensions
	{
		public static Tweener<float> AnimAudioVolumeTo(this AudioSource audioSource, float volume, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimAudioVolumeTo(audioSource, volume, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<float> AnimAudioVolumeTo(this AudioSource audioSource, float volume, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimAudioVolumeTo(audioSource, volume, duration, delay, ease, null, proxy);

		public static Tweener<float> AnimAudioVolumeTo(this AudioSource audioSource, float volume, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
			return Tweener.Generate(
				() => audioSource.volume,
				(value) => audioSource.volume = value,
				volume, duration, delay, ease,
				curve, () => audioSource != null, proxy);
		}
	}
}