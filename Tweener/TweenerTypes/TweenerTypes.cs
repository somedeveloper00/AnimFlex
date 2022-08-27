using AnimFlex.Core;
using UnityEngine;

namespace AnimFlex.Tweener
{
    internal class TweenerFloat : Tweener<float>
    {
        internal override void Set(float t) => setter(startValue + (endValue - startValue) * t);
    }

    internal class TweenerInt : Tweener<int>
    {
        internal override void Set(float t) => setter(Mathf.RoundToInt(startValue + (endValue - startValue) * t));
    }

    internal class TweenerVector2 : Tweener<Vector2>
    {
        internal override void Set(float t) => setter(startValue + (endValue - startValue) * t);
    }

    
    internal class TweenerVector3 : Tweener<Vector3>
    {
        internal override void Set(float t) => setter(Vector3.LerpUnclamped(startValue, endValue, t));
    }

    internal class TweenerRect : Tweener<Rect>
    {
        internal override void Set(float t) =>
            setter(new Rect(
                startValue.min + (endValue.min - startValue.min) * t,
                startValue.max + (endValue.max - startValue.max) * t));
    }

    internal class TweenerQuaternion : Tweener<Quaternion>
    {
        internal override void Set(float t) => setter(Quaternion.LerpUnclamped(startValue, endValue, t));
    }

    internal class TweenerColor : Tweener<Color>
    {
        internal override void Set(float t) => setter(Color.LerpUnclamped(startValue, endValue, t));
    }
}