using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex
{
	public static class ImageTweenerExtensions
	{
		public static Tweener<float> AnimImageFillTo(this Image image, float fill, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimImageFillTo(image, fill, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<float> AnimImageFillTo(this Image image, float fill, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimImageFillTo(image, fill, duration, delay, ease, null, proxy);
	
		public static Tweener<float> AnimImageFillTo(this Image image, float fill, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
			return Tweener.Generate(
				() => image.fillAmount,
				(value) => image.fillAmount = value,
				fill, duration, delay, ease,
				curve, () => image != null );
		}
	
	}
}