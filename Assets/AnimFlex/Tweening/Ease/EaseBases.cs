using System;
using System.Collections.Generic;

namespace AnimFlex.Tweening
{
    internal static partial class EasingEvaluator
    {
        private const int Resolution = 200;
        private static Dictionary<Type, float[]> _cachedEvaluations = new Dictionary<Type, float[]>();

        public static float Evaluate<T>(float t) where T : EasingFunc
        {
            if (!_cachedEvaluations.ContainsKey(typeof(T))) CreateCacheForFunc(easing, Resolution);
            return _cachedEvaluations[easing][(int)(t * _cachedEvaluations[easing].Length)];
        }

        private static void CreateCacheForFunc<T>(int resolution) where T : EasingFunc
        {
            var easingFunc = GetEasingFunc(key);
            _cachedEvaluations[key] = new float[resolution];
            var increament = 1f / resolution;

            for (int i = 0; i < resolution; i++)
            {
                _cachedEvaluations[key][i] = easingFunc.Evaluate(i * increament);
            }
            
            // just to ensure it's complete
            _cachedEvaluations[key][resolution - 1] = 1;
        }

    }

    [Serializable]
    internal abstract class EasingFunc
    {
        public abstract float Evaluate(float t);
    }
}