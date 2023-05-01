using System;
using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex
{
	[Serializable]
	public class TweenerGeneratorAudioVolume : TweenerGenerator<AudioSource, float>
	{
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
			return fromObject.AnimAudioVolumeTo( target, duration, delay, ease, customCurve, proxy );
		}
	}
}