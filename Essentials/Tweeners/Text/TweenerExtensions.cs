using AnimFlex.Core.Proxy;
using UnityEngine;
using UnityEngine.UI;

#if RTLTMP // support for rtl tmp
using RTLTMPro;
#endif

namespace AnimFlex.Tweening
{
	public static class TMP_TweenerExtensions
	{
		public static Tweener<int> AnimNumberToInt(this TMPro.TMP_Text tmp_text, int end, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimNumberToInt(tmp_text, end, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<int> AnimNumberToInt(this TMPro.TMP_Text tmp_text, int end, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimNumberToInt(tmp_text, end, duration, delay, ease, null, proxy);

		public static Tweener<int> AnimNumberToInt(this TMPro.TMP_Text tmp_text, int end, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
#if RTLTMP // support for rtl tmp
			if (tmp_text is RTLTextMeshPro rtltmp) {
				string val = rtltmp.OriginalText;
				return Tweener.Generate(
					() => v,
					(value) =>
					{
						v = value;
						tmp_text.SetText("{0}", v);
					},
					end, duration, delay, ease, curve,
					() => tmp_text != null, proxy);
			}
			else
#endif
			int.TryParse(tmp_text.text, out var v);
			return Tweener.Generate(
				() => v,
				(value) =>
				{
					v = value;
					tmp_text.SetText("{0}", v);
				},
				end, duration, delay, ease, curve,
				() => tmp_text != null, proxy);
		}

		public static Tweener<float> AnimNumberTo(this TMPro.TMP_Text tmp_text, float end, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimNumberTo(tmp_text, end, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<float> AnimNumberTo(this TMPro.TMP_Text tmp_text, float end, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimNumberTo(tmp_text, end, duration, delay, ease, null, proxy);

		public static Tweener<float> AnimNumberTo(this TMPro.TMP_Text tmp_text, float end, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
#if RTLTMP // support for rtl tmp
			if (tmp_text is RTLTextMeshPro rtltmp) {
				string val = rtltmp.OriginalText;
				return Tweener.Generate(
					() => v,
					(value) =>
					{
						v = value;
						tmp_text.SetText("{0}", v);
					},
					end, duration, delay, ease, curve,
					() => tmp_text != null, proxy);
			}
			else
#endif
			float.TryParse(tmp_text.text, out var v);
			return Tweener.Generate(
				() => v,
				(value) =>
				{
					v = value;
					tmp_text.SetText("{0}", v);
				},
				end, duration, delay, ease, curve,
				() => tmp_text != null, proxy);
		}

		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo(tmp_text, text, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo(tmp_text, text, duration, delay, ease, null, proxy);

		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
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
				() => tmp_text != null, proxy);
		}

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo(ui_text, text, duration, delay, Ease.Linear, curve, proxy);

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
			AnimTextTo(ui_text, text, duration, delay, ease, null, proxy);

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
		{
			return Tweener.Generate(
				() => ui_text.text,
				(value) => ui_text.text = value,
				text, duration, delay, ease, curve,
				() => ui_text != null, proxy);
		}
	}
}