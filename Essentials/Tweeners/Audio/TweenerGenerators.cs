using System;
using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
    [Serializable]
	public sealed class TweenerGeneratorAudioVolume : TweenerGenerator<AudioSource, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
		{
			return fromObject.AnimAudioVolumeTo(target, duration, delay, ease, customCurve, proxy);
		}
	}
}