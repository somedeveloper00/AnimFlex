using UnityEngine;

namespace AnimFlex.Tweening
{
    internal sealed class TweenerFloat : Tweener<float>
    {
        internal override void Set(float t) => setter(startValue + (endValue - startValue) * t);
    }

    internal sealed class TweenerInt : Tweener<int>
    {
        internal override void Set(float t) => setter(Mathf.RoundToInt(startValue + (endValue - startValue) * t));
    }

    internal sealed class TweenerUInt : Tweener<uint>
    {
        internal override void Set(float t) => setter((uint)(startValue + (endValue - startValue) * (double)t));
    }

    internal sealed class TweenerLong : Tweener<long>
    {
        internal override void Set(float t) => setter((long)(startValue + (endValue - startValue) * (double)t));
    }

    internal sealed class TweenerULong : Tweener<ulong>
    {
        internal override void Set(float t) => setter((ulong)(startValue + (endValue - startValue) * (double)t));
    }

    internal sealed class TweenerDecimal : Tweener<decimal>
    {
        internal override void Set(float t) => setter(startValue + (endValue - startValue) * (decimal)t);
    }

    internal sealed class TweenerVector2 : Tweener<Vector2>
    {
        internal override void Set(float t) => setter(startValue + (endValue - startValue) * t);
    }

    internal sealed class TweenerVector3 : Tweener<Vector3>
    {
        internal override void Set(float t) => setter(Vector3.LerpUnclamped(startValue, endValue, t));
    }

    internal sealed class TweenerVector4 : Tweener<Vector4>
    {
        internal override void Set(float t) => setter(Vector4.LerpUnclamped(startValue, endValue, t));
    }

    internal sealed class TweenerRect : Tweener<Rect>
    {
        internal override void Set(float t) =>
            setter(new Rect(
                startValue.min + (endValue.min - startValue.min) * t,
                startValue.max + (endValue.max - startValue.max) * t));
    }

    internal sealed class TweenerQuaternion : Tweener<Quaternion>
    {
        internal override void Set(float t) => setter(Quaternion.LerpUnclamped(startValue, endValue, t));
    }

    internal sealed class TweenerColor : Tweener<Color>
    {
        internal override void Set(float t) => setter(Color.LerpUnclamped(startValue, endValue, t));
    }

    internal sealed class TweenerString : Tweener<string>
    {
        private static System.Random rand = new System.Random(-1234567891);

        internal override void Set(float t)
        {
            if (t < 0)
            {
                char[] s = new char[-Mathf.CeilToInt(t * startValue.Length)];
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = startValue[rand.Next(startValue.Length)];
                }

                setter(new string(s) + startValue);
            }
            else if (t > 1)
            {
                char[] s = new char[Mathf.FloorToInt((t - 1) * endValue.Length)];
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = endValue[rand.Next(endValue.Length)];
                }

                setter(endValue + new string(s));
            }
            else if (startValue.Length > endValue.Length)
            {
                int ind = Mathf.FloorToInt(startValue.Length * t);
                setter(endValue.Substring(0, Mathf.Max(0, ind - (startValue.Length - endValue.Length))) +
                        startValue.Substring(ind));
            }
            else
            {
                int ind = Mathf.FloorToInt(endValue.Length * t);
                var v = Mathf.Max(0, ind - (endValue.Length - startValue.Length));
                setter(endValue.Substring(0, ind) +
                        startValue.Substring(v, startValue.Length - v));
            }
        }
    }
}