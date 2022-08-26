using System.Diagnostics;
using AnimFlex.Core;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AnimFlex.Tweener
{
    internal class EaseEvaluator
    {
        public static EaseEvaluator Instance => AnimFlexCore.Instance.EaseEvaluator;
        
        public const Ease CUSTOM_ANIMATION_CURVE_EASE = (Ease)256;

        private float[][] _cachedEvals = new float[28][];

        public EaseEvaluator()
        {
            // cache everything...
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < _cachedEvals.Length; i++)
                CacheEase((Ease)i, AnimFlexSettings.Instance.easeSampleCount, out _cachedEvals[i]);

            stopWatch.Stop();
            Debug.Log($"Cached all ease in : {stopWatch.Elapsed.TotalMilliseconds}");
        }

        public float EvaluateEase(Ease ease, float t, AnimationCurve customCurve)
        {
            // var _ease_index = (int)ease;
            // float indx = t * (_cachedEvals[_ease_index].Length - 1);
            // int indx_floor = (int)indx;
            // int indx_ceil = Mathf.CeilToInt(indx);
            //
            // // linear interpolate between the closest two evaluations
            // var a = _cachedEvals[_ease_index][indx_floor];
            // var b = _cachedEvals[_ease_index][indx_ceil];
            // return Mathf.Lerp(a, b, Mathf.InverseLerp(indx_floor, indx_ceil, indx));

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
                    return t;
                case Ease.InSine:
                    return (float)(-Mathf.Cos(t * 1.57079637050629f) + 1.0);
                case Ease.OutSine:
                    return Mathf.Sin(t * 1.57079637050629f);
                case Ease.InOutSine:
                    return -0.5f * (Mathf.Cos(3.14159274101257f * t) - 1.0f);
                case Ease.InQuad:
                    return t * t;
                case Ease.OutQuad:
                    return -t * (t - 2.0f);
                case Ease.InOutQuad:
                    return t * 0.5f < 1.0f
                        ? 0.5f * t * t
                        : -0.5f * (--t * (t - 2.0f) - 1.0f);
                case Ease.InCubic:
                    return t * t * t;
                case Ease.OutCubic:
                    return (t -= 1.0f) * t *
                        t + 1.0f;
                case Ease.InOutCubic:
                    return t * 0.5f < 1.0f
                        ? 0.5f * t * t * t
                        : 0.5f * ((t -= 2f) * t * t + 2.0f);
                case Ease.InQuart:
                    return t * t * t * t;
                case Ease.OutQuart:
                    return -((t -= 1.0f) * t *
                        t * t - 1.0f);
                case Ease.InOutQuart:
                    return (t /= 0.5f) < 1.0f
                        ? 0.5f * t * t * t * t
                        : -0.5f * ((t -= 2f) * t * t * t - 2.0f);
                case Ease.InQuint:
                    return t * t * t * t * t;
                case Ease.OutQuint:
                    return (t -= 1.0f) * t *
                        t * t * t + 1.0f;
                case Ease.InOutQuint:
                    return (t /= 0.5f) < 1.0f
                        ? 0.5f * t * t * t * t * t
                        : 0.5f * ((t -= 2f) * t * t * t * t + 2.0f);
                case Ease.InExpo:
                    return t != 0.0f
                        ? Mathf.Pow(2.0f, 10.0f * (t - 1.0f))
                        : 0.0f;
                case Ease.OutExpo:
                    return t == 1
                        ? 1f
                        : -Mathf.Pow(2.0f, -10.0f * t) + 1.0f;
                case Ease.InOutExpo:
                    if (t == 0.0f)
                        return 0.0f;
                    if (t == 1)
                        return 1f;
                    return t * 0.5f < 1.0f
                        ? 0.5f * Mathf.Pow(2.0f, 10.0f * (t - 1.0f))
                        : 0.5f * (-Mathf.Pow(2.0f, -10.0f * --t) + 2.0f);
                case Ease.InCirc:
                    return -(Mathf.Sqrt(1f - t * t) - 1f);
                case Ease.OutCirc:
                    return Mathf.Sqrt(1f - (t -= 1f) * t);
                case Ease.InOutCirc:
                    return (t /= 0.5f) < 1.0f
                        ? -0.5f * (Mathf.Sqrt(1.0f - t * t) - 1.0f)
                        : 0.5f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1.0f);
                case Ease.InElastic:
                    if (t == 0.0f)
                        return 0.0f;
                    if (t == 1.0f)
                        return 1f;
                    if (AnimFlexSettings.Instance.period == 0.0f)
                        AnimFlexSettings.Instance.period = 0.3f;
                    float num1;
                    if (AnimFlexSettings.Instance.overShoot < 1.0f)
                    {
                        AnimFlexSettings.Instance.overShoot = 1f;
                        num1 = AnimFlexSettings.Instance.period / 4f;
                    }
                    else
                        num1 = AnimFlexSettings.Instance.period / 6.283185f * Mathf.Asin(1.0f / AnimFlexSettings.Instance.overShoot);

                    return -(AnimFlexSettings.Instance.overShoot * Mathf.Pow(2.0f, 10.0f * --t) *
                             Mathf.Sin((t - num1) * 6.28318548202515f / AnimFlexSettings.Instance.period));
                case Ease.OutElastic:
                    if (t == 0.0f)
                        return 0.0f;
                    if (t == 1.0f)
                        return 1f;
                    if (AnimFlexSettings.Instance.period == 0.0f)
                        AnimFlexSettings.Instance.period = 0.3f;
                    float num2;
                    if (AnimFlexSettings.Instance.overShoot < 1f)
                    {
                        AnimFlexSettings.Instance.overShoot = 1f;
                        num2 = AnimFlexSettings.Instance.period / 4f;
                    }
                    else
                        num2 = AnimFlexSettings.Instance.period / 6.283185f * Mathf.Asin(1.0f / AnimFlexSettings.Instance.overShoot);

                    return AnimFlexSettings.Instance.overShoot * Mathf.Pow(2f, -10f * t) *
                        Mathf.Sin((t - num2) * 6.28318548202515f / AnimFlexSettings.Instance.period) + 1f;
                case Ease.InOutElastic:
                    if (t == 0f)
                        return 0f;
                    if (t * 0.5f == 2f)
                        return 1f;
                    if (AnimFlexSettings.Instance.period == 0f)
                        AnimFlexSettings.Instance.period = 0.45f;
                    float num3;
                    if (AnimFlexSettings.Instance.overShoot < 1f)
                    {
                        AnimFlexSettings.Instance.overShoot = 1f;
                        num3 = AnimFlexSettings.Instance.period / 4f;
                    }
                    else
                        num3 = AnimFlexSettings.Instance.period / 6.283185f * Mathf.Asin(1f / AnimFlexSettings.Instance.overShoot);

                    return t < 1f
                        ? -0.5f * (AnimFlexSettings.Instance.overShoot * Mathf.Pow(2f, 10f * --t) *
                                   Mathf.Sin((t - num3) * 6.28318548202515f / AnimFlexSettings.Instance.period))
                        : AnimFlexSettings.Instance.overShoot * Mathf.Pow(2f, -10f * --t) *
                        Mathf.Sin((t - num3) * 6.28318548202515f / AnimFlexSettings.Instance.period) * 0.5f + 1f;
                case Ease.InBack:
                    return t * t * ((AnimFlexSettings.Instance.overShoot + 1f) * t - AnimFlexSettings.Instance.overShoot);
                case Ease.OutBack:
                    return (t -= 1f) * t * ((AnimFlexSettings.Instance.overShoot + 1f) * t + AnimFlexSettings.Instance.overShoot) + 1f;
                case Ease.InOutBack:
                    return (t /= 1 * 0.5f) < 1.0
                        ? (float)(0.5 * (t * (double)t * (((AnimFlexSettings.Instance.overShoot *= 1.525f) + 1.0) * t - AnimFlexSettings.Instance.overShoot)))
                        : (float)(0.5 * ((t -= 2f) * (double)t * (((AnimFlexSettings.Instance.overShoot *= 1.525f) + 1.0) * t + AnimFlexSettings.Instance.overShoot) + 2.0));

                case CUSTOM_ANIMATION_CURVE_EASE:
                    return customCurve.Evaluate(t);
            }

            Debug.LogWarning("Ease unknown. using linear instaed.");
            return t;
        }
    }
}