using System;
using AnimFlex.Core.Proxy;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweening {
    #region Transform

    [Serializable]
    public class TweenerGeneratorPosition : TweenerGenerator<Transform, Vector3> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            var toPos = target;
            if (relative) toPos += fromObject.position;
            return fromObject.AnimPositionTo( toPos, duration, delay, ease, customCurve, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorLocalPosition : TweenerGenerator<Transform, Vector3> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            var toPos = target;
            if (relative) toPos += fromObject.localPosition;
            return fromObject.AnimLocalPositionTo( toPos, duration, delay, ease, customCurve, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorRotation : TweenerGenerator<Transform, Vector3> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            Vector3 toRot = target;
            if (relative) toRot += fromObject.rotation.eulerAngles;
            Vector3 startRot = fromObject.rotation.eulerAngles;
            float t = 0;
            return Tweener.Generate(
                () => t,
                (value) => {
                    t = value;
                    fromObject.rotation = Quaternion.Euler( Vector3.LerpUnclamped( startRot, toRot, t ) );
                }, 1, duration, delay, ease, customCurve, () => fromObject != null, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorLocalRotation : TweenerGenerator<Transform, Vector3> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            Vector3 toRot = target;
            if (relative) toRot += fromObject.localRotation.eulerAngles;
            Vector3 startRot = fromObject.localRotation.eulerAngles;
            float t = 0;
            return Tweener.Generate(
                () => t,
                (value) => {
                    t = value;
                    fromObject.localRotation = Quaternion.Euler( Vector3.LerpUnclamped( startRot, toRot, t ) );
                }, 1, duration, delay, ease, customCurve, () => fromObject != null, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorTransform : TweenerGenerator<Transform, Transform> {
        [Tooltip( "Match position with the given Transform's")]
        public bool position;
        [Tooltip( "Match rotation with the given Transform's")]
        public bool rotation;
        [Tooltip( "Match scale with the given Transform's")]
        public bool scale;


        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            float t = 0;
            Action<float> onSet = null;

            if (position) {
                Vector3 startPos = fromObject.position;
                onSet += (val) => fromObject.position = Vector3.LerpUnclamped( startPos, target.position, val );
            }

            if (rotation) {
                Vector3 startRot = fromObject.rotation.eulerAngles;
                onSet += (val) =>
                    fromObject.rotation =
                        Quaternion.Euler( Vector3.LerpUnclamped( startRot, target.rotation.eulerAngles, val ) );
            }
            
            if (scale) {
                Vector3 startScl = fromObject.localScale;
                onSet += (val) => fromObject.localScale = Vector3.LerpUnclamped( startScl, target.localScale, val );
            }

            return Tweener.Generate(
                () => t,
                (value) => {
                    t = value;
                    onSet?.Invoke( t );
                }, 1, duration, delay, ease, customCurve, () => fromObject != null, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorScale : TweenerGenerator<Transform, Vector3> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            Vector3 toScl = target;
            if (relative) {
                var localScale = fromObject.localScale;
                toScl.x *= localScale.x;
                toScl.y *= localScale.y;
                toScl.z *= localScale.z;
            }

            return fromObject.AnimScaleTo( toScl, duration, delay, ease, customCurve, proxy );
        }
    }

    #endregion

    #region Fade

    [Serializable]
    public class TweenerGeneratorFadeGraphic : TweenerGenerator<Graphic, float> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            float toVal = target;
            if (relative) toVal += fromObject.color.a;
            return fromObject.AnimFadeTo( toVal, duration, delay, ease, customCurve, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorFadeRenderer : TweenerGenerator<Renderer, float> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            float toVal = target;
            if (relative) toVal += fromObject.material.color.a;
            return fromObject.AnimFadeTo( toVal, duration, delay, ease, customCurve, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorFadeCanvasGroup : TweenerGenerator<CanvasGroup, float> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            float toVal = target;
            if (relative) toVal += fromObject.alpha;
            return fromObject.AnimFadeTo( toVal, duration, delay, ease, customCurve, proxy );
        }
    }

    #endregion

    #region Color

    [Serializable]
    public class TweenerGeneratorColorGraphic : TweenerGenerator<Graphic, Color> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            var toVal = target;
            if (relative) toVal += fromObject.color;
            return fromObject.AnimColorTo( toVal, duration, delay, ease, customCurve, proxy );
        }
    }

    [Serializable]
    public class TweenerGeneratorColorRenderer : TweenerGenerator<Renderer, Color> {
        protected override Tweener GenerateTween(AnimflexCoreProxy proxy) {
            var toVal = target;
            if (relative) toVal += fromObject.material.color;
            return Tweener.Generate(
                () => fromObject.material.color,
                (val) => {
                    for (int i = 0; i < fromObject.materials.Length; i++) {
                        fromObject.materials[i].color = val;
                    }
                }, toVal, duration, delay, ease, customCurve, () => fromObject );
        }
    }

    #endregion
}