using System;
using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Tweening
{
    [Serializable]
    public sealed class TweenerGeneratorProjectorSize : TweenerGenerator<Projector, float>
    {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
        {
            return fromObject.AnimProjectorSizeTo(target, duration, delay, ease, customCurve, proxy);
        }
    }

    [Serializable]
    public sealed class TweenerGeneratorProjectorAspectRatio : TweenerGenerator<Projector, float>
    {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
        {
            return fromObject.AnimProjectorAspectRatioTo(target, duration, delay, ease, customCurve, proxy);
        }
    }

    [Serializable]
    public sealed class TweenerGeneratorProjectorFieldOfView : TweenerGenerator<Projector, float>
    {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy)
        {
            return fromObject.AnimProjectorFieldOfViewTo(target, duration, delay, ease, customCurve, proxy);
        }
    }
}