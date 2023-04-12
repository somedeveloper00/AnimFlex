using System;
using UnityEngine;

namespace AnimFlex.Tweening {
    internal class TweenerFloat : Tweener<float> {
        internal override void Set(float t) => setter( startValue + ( endValue - startValue ) * t );

        public TweenerFloat(Func<float> getter, Action<float> setter, Func<bool> isValid, float endValue, Ease ease,
            float duration, float delay, AnimationCurve customCurve, bool useCurve) : base( getter, setter, isValid,
            endValue, ease, duration, delay, customCurve, useCurve ) { }
    }

    internal class TweenerInt : Tweener<int> {
        internal override void Set(float t) => setter( Mathf.RoundToInt( startValue + ( endValue - startValue ) * t ) );

        public TweenerInt(Func<int> getter, Action<int> setter, Func<bool> isValid, int endValue, Ease ease,
            float duration, float delay, AnimationCurve customCurve, bool useCurve) : base( getter, setter, isValid,
            endValue, ease, duration, delay, customCurve, useCurve ) { }
    }

    internal class TweenerVector2 : Tweener<Vector2> {
        internal override void Set(float t) => setter( startValue + ( endValue - startValue ) * t );

        public TweenerVector2(Func<Vector2> getter, Action<Vector2> setter, Func<bool> isValid, Vector2 endValue, Ease ease, 
            float duration, float delay, AnimationCurve customCurve, bool useCurve) : base( getter, setter,
            isValid, endValue, ease, duration, delay, customCurve, useCurve ) { }
    }


    internal class TweenerVector3 : Tweener<Vector3> {
        internal override void Set(float t) => setter( Vector3.LerpUnclamped( startValue, endValue, t ) );

        public TweenerVector3(Func<Vector3> getter, Action<Vector3> setter, Func<bool> isValid, Vector3 endValue, Ease ease, 
            float duration, float delay, AnimationCurve customCurve, bool useCurve) : base( getter, setter,
            isValid, endValue, ease, duration, delay, customCurve, useCurve ) { }
    }

    internal class TweenerRect : Tweener<Rect> {
        internal override void Set(float t) =>
            setter( new Rect(
                startValue.min + ( endValue.min - startValue.min ) * t,
                startValue.max + ( endValue.max - startValue.max ) * t ) );

        public TweenerRect(Func<Rect> getter, Action<Rect> setter, Func<bool> isValid, Rect endValue, Ease ease,
            float duration, float delay, AnimationCurve customCurve, bool useCurve) : base( getter, setter, isValid,
            endValue, ease, duration, delay, customCurve, useCurve ) { }
    }

    internal class TweenerQuaternion : Tweener<Quaternion> {
        internal override void Set(float t) => setter( Quaternion.LerpUnclamped( startValue, endValue, t ) );

        public TweenerQuaternion(Func<Quaternion> getter, Action<Quaternion> setter, Func<bool> isValid,
            Quaternion endValue, Ease ease, float duration, float delay, AnimationCurve customCurve, bool useCurve) :
            base( getter, setter, isValid, endValue, ease, duration, delay, customCurve, useCurve ) { }
    }

    internal class TweenerColor : Tweener<Color> {
        internal override void Set(float t) => setter( Color.LerpUnclamped( startValue, endValue, t ) );

        public TweenerColor(Func<Color> getter, Action<Color> setter, Func<bool> isValid, Color endValue, Ease ease,
            float duration, float delay, AnimationCurve customCurve, bool useCurve) : base( getter, setter, isValid,
            endValue, ease, duration, delay, customCurve, useCurve ) { }
    }

    internal class TweenerString : Tweener<string> {
        private static System.Random rand = new System.Random( -1234567891 );

        internal override void Set(float t) {
            if (t < 0) {
                char[] s = new char[-Mathf.CeilToInt( t * startValue.Length )];
                for (int i = 0; i < s.Length; i++)
                    s[i] = startValue[rand.Next( startValue.Length )];
                setter( new string( s ) + startValue );
            }
            else if (t > 1) {
                char[] s = new char[Mathf.FloorToInt( ( t - 1 ) * endValue.Length )];
                for (int i = 0; i < s.Length; i++)
                    s[i] = endValue[rand.Next( endValue.Length )];
                setter( endValue + new string( s ) );
            }
            else if (startValue.Length > endValue.Length) {
                int ind = Mathf.FloorToInt( startValue.Length * t );
                setter( endValue.Substring( 0, Mathf.Max( 0, ind - ( startValue.Length - endValue.Length ) ) ) +
                        startValue.Substring( ind ) );
            }
            else {
                int ind = Mathf.FloorToInt( endValue.Length * t );
                var v = Mathf.Max( 0, ind - ( endValue.Length - startValue.Length ) );
                setter( endValue.Substring( 0, ind ) +
                        startValue.Substring( v, startValue.Length - v ) );
            }
        }

        public TweenerString(Func<string> getter, Action<string> setter, Func<bool> isValid, string endValue, Ease ease,
            float duration, float delay, AnimationCurve customCurve, bool useCurve) : base( getter, setter, isValid,
            endValue, ease, duration, delay, customCurve, useCurve ) { }
    }
}