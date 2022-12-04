using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Essentials.TMP
{
	public static class TMP_TweenerExtensions
	{
		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, AnimationCurve curve,
			float duration = 1, float delay = 0) =>
			AnimTextTo( tmp_text, text, Ease.Linear, duration, delay, curve );

		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0) =>
			AnimTextTo( tmp_text, text, ease, duration, delay );

		public static Tweener<string> AnimTextTo(this TMPro.TMP_Text tmp_text, string text, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0, AnimationCurve curve = null) {
			return Tweener.Generate(
				() => tmp_text.text,
				(value) => tmp_text.text = value,
				text, ease, duration, delay, curve,
				() => tmp_text != null );
		}

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, AnimationCurve curve,
			float duration = 1, float delay = 0) =>
			AnimTextTo( ui_text, text, Ease.Linear, duration, delay, curve );

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0) =>
			AnimTextTo( ui_text, text, ease, duration, delay );

		public static Tweener<string> AnimTextTo(this Text ui_text, string text, Ease ease = Ease.InOutSine,
			float duration = 1, float delay = 0, AnimationCurve curve = null) {
			return Tweener.Generate(
				() => ui_text.text,
				(value) => ui_text.text = value,
				text, ease, duration, delay, curve,
				() => ui_text != null );
		}
	}
}