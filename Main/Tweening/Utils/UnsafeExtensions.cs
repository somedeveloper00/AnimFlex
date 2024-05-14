using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
    /// <summary>
    /// Unsafe utility for easy access to tweeners. The references value will be used for tweening, and the component calling this 
    /// function will work as the safeguard (tweener will die when the component dies or gets disabled). If a new tweener is requested 
    /// on a value reference that already has another active tweener, the previous tweener will be killed, ensuring that there's always 
    /// only one tweener operating on a reference value. So you don't have to manually kill a previous tweener before starting a new one.
    /// </summary>
    public unsafe static class UnsafeExtensions
    {
        private static readonly Dictionary<nint, Tweener> s_tweeners = new();

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<float> AnimRef(this Component component, ref float value, float target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (float* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<float> AnimRef(this Component component, ref float value, float target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (float* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<float> AnimRef(Component component, float* ptr, float target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<float>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<int> AnimRef(this Component component, ref int value, int target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (int* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<int> AnimRef(this Component component, ref int value, int target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (int* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<int> AnimRef(Component component, int* ptr, int target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<int>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<uint> AnimRef(this Component component, ref uint value, uint target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (uint* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<uint> AnimRef(this Component component, ref uint value, uint target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (uint* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<uint> AnimRef(Component component, uint* ptr, uint target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<uint>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<long> AnimRef(this Component component, ref long value, long target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (long* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<long> AnimRef(this Component component, ref long value, long target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (long* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<long> AnimRef(Component component, long* ptr, long target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<long>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<ulong> AnimRef(this Component component, ref ulong value, ulong target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (ulong* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<ulong> AnimRef(this Component component, ref ulong value, ulong target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (ulong* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<ulong> AnimRef(Component component, ulong* ptr, ulong target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<ulong>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<decimal> AnimRef(this Component component, ref decimal value, decimal target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (decimal* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<decimal> AnimRef(this Component component, ref decimal value, decimal target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (decimal* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<decimal> AnimRef(Component component, decimal* ptr, decimal target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<decimal>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Vector2> AnimRef(this Component component, ref Vector2 value, Vector2 target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Vector2* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Vector2> AnimRef(this Component component, ref Vector2 value, Vector2 target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Vector2* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<Vector2> AnimRef(Component component, Vector2* ptr, Vector2 target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<Vector2>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Vector3> AnimRef(this Component component, ref Vector3 value, Vector3 target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Vector3* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Vector3> AnimRef(this Component component, ref Vector3 value, Vector3 target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Vector3* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<Vector3> AnimRef(Component component, Vector3* ptr, Vector3 target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<Vector3>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Vector4> AnimRef(this Component component, ref Vector4 value, Vector4 target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Vector4* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Vector4> AnimRef(this Component component, ref Vector4 value, Vector4 target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Vector4* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<Vector4> AnimRef(Component component, Vector4* ptr, Vector4 target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<Vector4>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Quaternion> AnimRef(this Component component, ref Quaternion value, Quaternion target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Quaternion* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Quaternion> AnimRef(this Component component, ref Quaternion value, Quaternion target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Quaternion* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<Quaternion> AnimRef(Component component, Quaternion* ptr, Quaternion target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<Quaternion>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Rect> AnimRef(this Component component, ref Rect value, Rect target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Rect* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Rect> AnimRef(this Component component, ref Rect value, Rect target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Rect* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<Rect> AnimRef(Component component, Rect* ptr, Rect target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<Rect>)tweener;
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Color> AnimRef(this Component component, ref Color value, Color target, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Color* ptr = &value)
                return AnimRef(component, ptr, target, EaseEvaluator.CUSTOM_ANIMATION_CURVE_EASE, curve, duration, delay, proxy);
        }

        /// <inheritdoc cref="UnsafeExtensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tweener<Color> AnimRef(this Component component, ref Color value, Color target, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null)
        {
            fixed (Color* ptr = &value)
                return AnimRef(component, ptr, target, ease, null, duration, delay, proxy);
        }

        internal static Tweener<Color> AnimRef(Component component, Color* ptr, Color target, Ease ease, AnimationCurve curve, float duration, float delay, AnimflexCoreProxy proxy)
        {
            if (s_tweeners.TryGetValue((nint)ptr, out var tweener) && tweener.IsActive())
            {
                tweener.Kill();
            }

            tweener = Tweener.Generate(
                () => *ptr,
                v => *ptr = v,
                target, duration, delay, ease, curve, () => component, proxy
            );
            s_tweeners[(nint)ptr] = tweener;
            return (Tweener<Color>)tweener;
        }
    }
}