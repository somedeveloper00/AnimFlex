using System;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Essentials {
    [Serializable]
    public class TweenerGeneratorProjectorSize : TweenerGenerator<Projector, float> {
        protected override Tweener GenerateTween(AnimationCurve curve) {
            return fromObject.AnimProjectorSizeTo(target, ease, duration, delay, curve);
        }
    }

    [Serializable]
    public class TweenerGeneratorProjectorAspectRatio : TweenerGenerator<Projector, float> {
        protected override Tweener GenerateTween(AnimationCurve curve) {
            return fromObject.AnimProjectorAspectRatioTo(target, ease, duration, delay, curve);
        }
    }

    [Serializable]
    public class TweenerGeneratorProjectorFieldOfView : TweenerGenerator<Projector, float> {
        protected override Tweener GenerateTween(AnimationCurve curve) {
            return fromObject.AnimProjectorFieldOfViewTo(target, ease, duration, delay, curve);
        }
    }
}