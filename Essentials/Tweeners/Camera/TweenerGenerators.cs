using System;
using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
    [Serializable]
	public sealed class TweenerGeneratorCameraFieldOfView : TweenerGenerator<Camera, float>
	{
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) => fromObject.AnimCameraFieldOfViewTo(target, duration, delay, ease, customCurve, proxy);
    }
}