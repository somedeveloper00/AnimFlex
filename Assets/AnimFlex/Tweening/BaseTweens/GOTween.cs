using System;
using UnityEngine;

namespace AnimFlex.Tweening
{
    /// <summary>
    /// tween specifically made for GameObject-based manipulations
    /// </summary>
    public class GoTween<T> : Tween<T>
    {
        private readonly GameObject _instanceObject;

        public GoTween(GameObject instanceObject, T fromValue, T toValue, float duration, Func<T, T, float, T> evaluator)
        {
            this._instanceObject = instanceObject;
            FromValue = fromValue;
            ToValue = toValue;
            Evaluator = evaluator;
        }

        internal override bool ShouldCancel() => _instanceObject == null || !_instanceObject.activeSelf;
    }
}