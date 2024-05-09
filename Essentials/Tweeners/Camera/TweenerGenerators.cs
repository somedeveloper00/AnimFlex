using System;
using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex
{
	[Serializable]
	public class TweenerGeneratorCameraFieldOfView : TweenerGenerator<Camera, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimCameraFieldOfViewTo(target, duration, delay, ease, customCurve, proxy);
		}
	}
}