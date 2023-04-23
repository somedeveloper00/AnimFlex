using System;
using AnimFlex.Core.Proxy;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Essentials {
    [Serializable]
    public class TweenerGeneratorProjectorSize : TweenerGenerator<Projector, float> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            return fromObject.AnimProjectorSizeTo(target, duration, delay, ease, customCurve, proxy);
        }
    }

    [Serializable]
    public class TweenerGeneratorProjectorAspectRatio : TweenerGenerator<Projector, float> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            return fromObject.AnimProjectorAspectRatioTo(target, duration, delay, ease, customCurve, proxy);
        }
    }

    [Serializable]
    public class TweenerGeneratorProjectorFieldOfView : TweenerGenerator<Projector, float> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            return fromObject.AnimProjectorFieldOfViewTo(target, duration, delay, ease, customCurve, proxy);
        }
    }
}