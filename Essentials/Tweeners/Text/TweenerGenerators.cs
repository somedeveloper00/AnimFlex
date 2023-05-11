using System;
using AnimFlex.Core.Proxy;
using AnimFlex.Essentials.TMP;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex {
	[Serializable]
	public class TweenerGeneratorTMP_Text : TweenerGenerator<TMPro.TMP_Text, string> {
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
			return fromObject.AnimTextTo( target, duration, delay, ease, customCurve, proxy );
		}
	}

	[Serializable]
	public class TweenerGeneratorUiText : TweenerGenerator<Text, string> {
		protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
			return fromObject.AnimTextTo( target, duration, delay, ease, customCurve, proxy );
		}
	}
}