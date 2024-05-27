using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
    public static class CameraTweenerExtensions
	{
		public static Tweener<float> AnimCameraFieldOfViewTo(this Camera camera, float value, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimCameraFieldOfViewTo(camera, value, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<float> AnimCameraFieldOfViewTo(this Camera camera, float value, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimCameraFieldOfViewTo(camera, value, duration, delay, ease, null, proxy);

		public static Tweener<float> AnimCameraFieldOfViewTo(this Camera camera, float value, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
			return Tweener.Generate(
				() => camera.fieldOfView,
				(v) => camera.fieldOfView = v,
				value, duration, delay, ease,
				curve, () => camera != null, proxy);
		}
	}
}