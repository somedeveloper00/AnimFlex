using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Essentials.TMP
{
	public static class TMP_TweenerExtensions
	{
		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo( tmp_text, text, duration, delay, Ease.Linear, curve, proxy );

		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo( tmp_text, text, duration, delay, ease, null, proxy );

		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy) {
#if RTLTMP // support for rtl tmp
			if (tmp_text is RTLTextMeshPro rtltmp) {
				string val = rtltmp.OriginalText;
				return Tweener.Generate(
					() => rtltmp.OriginalText,
					(value) => rtltmp.text = value,
					text, duration, delay, ease, curve,
					() => tmp_text != null, proxy );
			}
			else
#endif
				return Tweener.Generate(
					() => tmp_text.text,
					(value) => tmp_text.text = value,
					text, duration, delay, ease, curve,
					() => tmp_text != null, proxy );
		}

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo( ui_text, text, duration, delay, Ease.Linear, curve, proxy );

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo( ui_text, text, duration, delay, ease, null, proxy );

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy) {
			return Tweener.Generate(
				() => ui_text.text,
				(value) => ui_text.text = value,
				text, duration, delay, ease, curve,
				() => ui_text != null, proxy );
		}
	}
}