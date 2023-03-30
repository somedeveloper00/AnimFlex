using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Essentials {
    public static class ProjectorTweenerExtensions {
        public static Tweener<float> AnimProjectorSizeTo(this Projector projector, float size, AnimationCurve curve,
            float duration = 1, float delay = 0) =>
            AnimProjectorSizeTo(projector, size, Ease.Linear, duration, delay, curve);

        public static Tweener<float> AnimProjectorSizeTo(this Projector projector, float size, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0) =>
            AnimProjectorSizeTo(projector, size, ease, duration, delay, null);

        public static Tweener<float> AnimProjectorSizeTo(this Projector projector, float size, Ease ease,
            float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => projector.orthographicSize,
                (value) => projector.orthographicSize = value,
                size, ease, duration, delay, curve,
                () => projector != null);
        }

        
        public static Tweener<float> AnimProjectorAspectRatioTo(this Projector projector, float aspectRatio, AnimationCurve curve,
            float duration = 1, float delay = 0) =>
            AnimProjectorAspectRatioTo(projector, aspectRatio, Ease.Linear, duration, delay, curve);

        public static Tweener<float> AnimProjectorAspectRatioTo(this Projector projector, float aspectRatio, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0) =>
            AnimProjectorAspectRatioTo(projector, aspectRatio, ease, duration, delay, null);

        public static Tweener<float> AnimProjectorAspectRatioTo(this Projector projector, float aspectRatio, Ease ease,
            float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => projector.aspectRatio,
                (value) => projector.aspectRatio = value,
                aspectRatio, ease, duration, delay, curve,
                () => projector != null);
        }

        public static Tweener<float> AnimProjectorFieldOfViewTo(this Projector projector, float fieldOfView, AnimationCurve curve,
            float duration = 1, float delay = 0) =>
            AnimProjectorFieldOfViewTo(projector, fieldOfView, Ease.Linear, duration, delay, curve);

        public static Tweener<float> AnimProjectorFieldOfViewTo(this Projector projector, float fieldOfView, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0) =>
            AnimProjectorFieldOfViewTo(projector, fieldOfView, ease, duration, delay, null);

        public static Tweener<float> AnimProjectorFieldOfViewTo(this Projector projector, float fieldOfView, Ease ease,
            float duration, float delay, AnimationCurve curve)
        {
            return Tweener.Generate(
                () => projector.fieldOfView,
                (value) => projector.fieldOfView = value,
                fieldOfView, ease, duration, delay, curve,
                () => projector != null);
        }
    }
}