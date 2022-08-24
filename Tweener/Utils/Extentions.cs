using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweener
{
    public static class Extentions
    {
        #region Transform Extentions

        public static Tweener AnimPositionTo(this Transform transform, Vector3 endPosition, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => transform.position,
                (value) => transform.position = value,
                endPosition, ease, duration, delay);
        }

        public static Tweener AnimLocalPositionTo(this Transform transform, Vector3 endPosition,
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => transform.localPosition,
                (value) => transform.localPosition = value,
                endPosition, ease, duration, delay);
        }

        public static Tweener AnimRotationTo(this Transform transform, Quaternion endRotation,
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => transform.rotation,
                (value) => transform.rotation = value,
                endRotation, ease, duration, delay);
        }

        public static Tweener AnimLocalRotationTo(this Transform transform, Quaternion endRotation,
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => transform.localRotation,
                (value) => transform.localRotation = value,
                endRotation, ease, duration, delay);
        }

        public static Tweener AnimRotationTo(this Transform transform, Vector3 endRotation, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => transform.rotation,
                (value) => transform.rotation = value,
                Quaternion.Euler(endRotation), ease, duration, delay);
        }

        public static Tweener AnimLocalRotationTo(this Transform transform, Vector3 endRotation,
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => transform.localRotation,
                (value) => transform.localRotation = value,
                Quaternion.Euler(endRotation), ease, duration, delay);
        }

        public static Tweener AnimScaleTo(this Transform transform, Vector3 endScale,
            Ease ease = Ease.InOutSine, float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => transform.localScale,
                (value) => transform.localScale = value,
                endScale, ease, duration, delay);
        }

        
        #endregion

        #region Shader Extentions

        public static Tweener AnimFadeTo(this Graphic graphic, float endFade, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => graphic.color.a,
                (value) => graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, value),
                endFade, ease, duration, delay);
        }

        public static Tweener AnimFadeTo(this Material material, float endFade, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => material.color.a,
                (value) => material.color = new Color(material.color.r, material.color.g, material.color.b, value),
                endFade, ease, duration, delay);
        }

        public static Tweener AnimFadeTo(this Renderer renderer, float endFade, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => renderer.material.color.a,
                (value) =>
                {
                    var material = renderer.material;
                    material.color = new Color(material.color.r, material.color.g, material.color.b, value);
                },
                endFade, ease, duration, delay);
        }

        public static Tweener AnimFadeTo(this CanvasGroup canvasGroup, float endFade, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => canvasGroup.alpha,
                (value) => canvasGroup.alpha = value,
                endFade, ease, duration, delay);
        }

        
        public static Tweener AnimColorTo(this Graphic graphic, Color endColor, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => graphic.color,
                (value) => graphic.color = value,
                endColor, ease, duration, delay);
        }

        public static Tweener AnimColorTo(this Material material, Color endColor, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => material.color,
                (value) => material.color = value,
                endColor, ease, duration, delay);
        }

        public static Tweener AnimColorTo(this Renderer renderer, Color endColor, Ease ease = Ease.InOutSine,
            float duration = 1, float delay = 0)
        {
            return Tweener.Generate(
                () => renderer.material.color,
                (value) => renderer.material.color = value,
                endColor, ease, duration, delay);
        }

        #endregion
    }
}