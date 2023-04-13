using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex
{
	public static class ImageTweenerExtensions
	{
		public static Tweener<float> AnimImageFillTo(this Image image, float fill, AnimationCurve curve,
			float duration = 1, float delay = 0) =>
			AnimImageFillTo(image, fill, Ease.Linear, duration, delay, curve);

		public static Tweener<float> AnimImageFillTo(this Image image, float fill, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0) =>
			AnimImageFillTo(image, fill, ease, duration, delay, null);
	
		public static Tweener<float> AnimImageFillTo(this Image image, float fill, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0, AnimationCurve curve = null)
		{
			return Tweener.Generate(
				() => image.fillAmount,
				(value) => image.fillAmount = value,
				fill, duration: duration, delay: delay, ease: ease,
				customCurve: curve, isValid: () => image != null );
		}
	
	}
}