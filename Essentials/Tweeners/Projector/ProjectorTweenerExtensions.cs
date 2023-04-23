using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Essentials {
    public static class ProjectorTweenerExtensions {
        public static Tweener<float> AnimProjectorSizeTo(this Projector projector, float size, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimProjectorSizeTo(projector, size, duration, delay, Ease.Linear, curve, proxy);

        public static Tweener<float> AnimProjectorSizeTo(this Projector projector, float size, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimProjectorSizeTo(projector, size, duration, delay, ease, null, proxy);

        public static Tweener<float> AnimProjectorSizeTo(this Projector projector, float size, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => projector.orthographicSize,
                (value) => projector.orthographicSize = value,
                size, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => projector != null, proxy );
        }

        
        public static Tweener<float> AnimProjectorAspectRatioTo(this Projector projector, float aspectRatio, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimProjectorAspectRatioTo(projector, aspectRatio, duration, delay, Ease.Linear, curve, proxy);

        public static Tweener<float> AnimProjectorAspectRatioTo(this Projector projector, float aspectRatio, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimProjectorAspectRatioTo(projector, aspectRatio, duration, delay, ease, null, proxy);

        public static Tweener<float> AnimProjectorAspectRatioTo(this Projector projector, float aspectRatio, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => projector.aspectRatio,
                (value) => projector.aspectRatio = value,
                aspectRatio, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => projector != null, proxy );
        }

        public static Tweener<float> AnimProjectorFieldOfViewTo(this Projector projector, float fieldOfView, AnimationCurve curve, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimProjectorFieldOfViewTo(projector, fieldOfView, duration, delay, Ease.Linear, curve, proxy);

        public static Tweener<float> AnimProjectorFieldOfViewTo(this Projector projector, float fieldOfView, Ease ease = Ease.InOutSine, float duration = 1, float delay = 0, AnimflexCoreProxy proxy = null) =>
            AnimProjectorFieldOfViewTo(projector, fieldOfView, duration, delay, ease, null, proxy);

        public static Tweener<float> AnimProjectorFieldOfViewTo(this Projector projector, float fieldOfView, float duration, float delay, Ease ease, AnimationCurve curve, AnimflexCoreProxy proxy)
        {
            return Tweener.Generate(
                () => projector.fieldOfView,
                (value) => projector.fieldOfView = value,
                fieldOfView, duration: duration, delay: delay, ease: ease,
                customCurve: curve, isValid: () => projector != null, proxy );
        }
    }
}