using System;
using System.Diagnostics;
using AnimFlex.Core;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Tweener
{
    internal static class EaseUtility
    {
        public const Ease CUSTOM_ANIMATION_CURVE_EASE = (Ease)35;

        private static float[][] _cachedEvals_low;
        private static float[][] _cachedEvals_medium;
        private static float[][] _cachedEvals_high;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            
            // cache everything...
            AnimFlexInitializer.onInit += () =>
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                // bad
                _cachedEvals_low = new float[22][];
                for (int i = 0; i < 22; i++)
                    CacheEase((Ease)i, AnimFlexSettings.Instance.easeLowQualitySampleCount, out _cachedEvals_low[i]);

                // medium
                _cachedEvals_medium = new float[22][];
                for (int i = 0; i < 22; i++)
                    CacheEase((Ease)i, AnimFlexSettings.Instance.easeMediumQualitySampleCount, out _cachedEvals_medium[i]);

                // hard
                _cachedEvals_high = new float[22][];
                for (int i = 0; i < 22; i++)
                    CacheEase((Ease)i, AnimFlexSettings.Instance.easeHighQualitySampleCount, out _cachedEvals_high[i]);
                
                stopWatch.Stop();
                Debug.Log(stopWatch.Elapsed.TotalMilliseconds);
            };
        }
        
        public static float EvaluateEase(Ease ease, EaseQuality quality, float t, AnimationCurve customCurve)
        {
            switch (quality)
            {
                case EaseQuality.Medium:
                    return _cachedEvals_medium[(int)ease][Mathf.RoundToInt(t * (_cachedEvals_medium[(int)ease].Length - 1))];
                case EaseQuality.Low:
                    return _cachedEvals_low[(int)ease][Mathf.RoundToInt(t * (_cachedEvals_low[(int)ease].Length - 1))];
                case EaseQuality.High:
                    return _cachedEvals_high[(int)ease][Mathf.RoundToInt(t * (_cachedEvals_high[(int)ease].Length - 1))];
            }
            return ExactEvaluateEase(ease, t, customCurve);
        }

        private static void CacheEase(Ease ease, int sampleCount, out float[] array)
        {
            array = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++) 
                array[i] = ExactEvaluateEase(ease, (float)i / sampleCount, null);
        }
        
        private static float ExactEvaluateEase(Ease ease, float t, AnimationCurve customCurve)
        {
            switch (ease)
            {
                case Ease.Linear:
                    return t / 1;
                case Ease.InSine:
                    return (float)(-Math.Cos(t / 1 * 1.57079637050629) + 1.0);
                case Ease.OutSine:
                    return (float)Math.Sin(t / 1 * 1.57079637050629);
                case Ease.InOutSine:
                    return (float)(-0.5 * (Math.Cos(3.14159274101257 * t / 1) - 1.0));
                case Ease.InQuad:
                    return (t /= 1) * t;
                case Ease.OutQuad:
                    return (float)(-(double)(t /= 1) * (t - 2.0));
                case Ease.InOutQuad:
                    return (t /= 1 * 0.5f) < 1.0
                        ? 0.5f * t * t
                        : (float)(-0.5 * (--t * (t - 2.0) - 1.0));
                case Ease.InCubic:
                    return (t /= 1) * t * t;
                case Ease.OutCubic:
                    return (float)((t = (float)(t / 1 - 1.0)) * (double)t *
                        t + 1.0);
                case Ease.InOutCubic:
                    return (t /= 1 * 0.5f) < 1.0
                        ? 0.5f * t * t * t
                        : (float)(0.5 * ((t -= 2f) * (double)t * t + 2.0));
                case Ease.InQuart:
                    return (t /= 1) * t * t * t;
                case Ease.OutQuart:
                    return (float)-((t = (float)(t / 1 - 1.0)) * (double)t *
                        t * t - 1.0);
                case Ease.InOutQuart:
                    return (t /= 1 * 0.5f) < 1.0
                        ? 0.5f * t * t * t * t
                        : (float)(-0.5 * ((t -= 2f) * (double)t * t * t - 2.0));
                case Ease.InQuint:
                    return (t /= 1) * t * t * t * t;
                case Ease.OutQuint:
                    return (float)((t = (float)(t / 1 - 1.0)) * (double)t *
                        t * t * t + 1.0);
                case Ease.InOutQuint:
                    return (t /= 1 * 0.5f) < 1.0
                        ? 0.5f * t * t * t * t * t
                        : (float)(0.5 * ((t -= 2f) * (double)t * t * t *
                            t + 2.0));
                case Ease.InExpo:
                    return t != 0.0
                        ? (float)Math.Pow(2.0, 10.0 * (t / 1 - 1.0))
                        : 0.0f;
                case Ease.OutExpo:
                    return t == 1
                        ? 1f
                        : (float)(-Math.Pow(2.0, -10.0 * t / 1) + 1.0);
                case Ease.InOutExpo:
                    if (t == 0.0)
                        return 0.0f;
                    if (t == 1)
                        return 1f;
                    return (t /= 1 * 0.5f) < 1.0
                        ? 0.5f * (float)Math.Pow(2.0, 10.0 * (t - 1.0))
                        : (float)(0.5 * (-Math.Pow(2.0, -10.0 * --t) + 2.0));
                case Ease.InCirc:
                    return (float)-(Math.Sqrt(1.0 - (t /= 1) * (double)t) - 1.0);
                case Ease.OutCirc:
                    return (float)Math.Sqrt(1.0 - (t = (float)(t / 1 - 1.0)) *
                        (double)t);
                case Ease.InOutCirc:
                    return (t /= 1 * 0.5f) < 1.0
                        ? (float)(-0.5 * (Math.Sqrt(1.0 - t * (double)t) - 1.0))
                        : (float)(0.5 * (Math.Sqrt(1.0 - (t -= 2f) * (double)t) + 1.0));

                case CUSTOM_ANIMATION_CURVE_EASE:
                    return customCurve.Evaluate(t);
            }

            Debug.LogWarning("Ease unknown. using linear instaed.");
            return t;
        }
    }
}