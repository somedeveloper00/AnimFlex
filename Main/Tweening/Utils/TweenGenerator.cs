using System;
using UnityEngine;

namespace AnimFlex.Tweening
{
    /// <summary>
    /// low level extensions
    /// </summary>
    public partial class Tweener
    {
        public static Tweener<float> Generate(Func<float> getter, Action<float> setter, float endValue,
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null) 
        {
            var tweener = new TweenerFloat( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null );
            return tweener;
        }

        public static Tweener<int> Generate(Func<int> getter, Action<int> setter, int endValue, 
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null)
        {
            var tweener = new TweenerInt( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null);
            return tweener;
        }

        public static Tweener<Vector2> Generate(Func<Vector2> getter, Action<Vector2> setter, Vector2 endValue, 
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null)
        {
            var tweener = new TweenerVector2( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null);
            return tweener;
        }

        public static Tweener<Vector3> Generate(Func<Vector3> getter, Action<Vector3> setter, Vector3 endValue, 
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null)
        {
            var tweener = new TweenerVector3( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null);
            return tweener;
        }

        public static Tweener<Quaternion> Generate(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion endValue, 
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null)
        {
            var tweener = new TweenerQuaternion( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null);
            return tweener;
        }

        public static Tweener<Rect> Generate(Func<Rect> getter, Action<Rect> setter, Rect endValue, 
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null)
        {
            var tweener = new TweenerRect( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null);
            return tweener;
        }

        public static Tweener<Color> Generate(Func<Color> getter, Action<Color> setter, Color endValue, 
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null)
        {
            var tweener = new TweenerColor( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null);
            return tweener;
        }

        public static Tweener<string> Generate(Func<string> getter, Action<string> setter, string endValue, 
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimationCurve customCurve = null, Func<bool> isValid = null)
        {
            var tweener = new TweenerString( getter, setter, isValid, endValue, ease, duration, delay, customCurve, customCurve != null);
            return tweener;
        }
    }
}
