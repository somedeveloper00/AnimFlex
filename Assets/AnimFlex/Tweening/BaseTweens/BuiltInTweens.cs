using System;
using UnityEngine;

namespace AnimFlex.Tweening
{
    public static class BuiltInTweens
    {
        public static GoTween<float> GenerateTween(this MonoBehaviour component, float from, float to, float duration, Action<float> setter)
        {
            var tween = new GoTween<float>(component.gameObject, from, to, duration, Mathf.Lerp);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<int> GenerateTween(this MonoBehaviour component, int from, int to, float duration, Action<int> setter)
        {
            int Evaluator(int a, int b, float t) => (int)(a + (b - 1) * t);
            var tween = new GoTween<int>(component.gameObject, from, to, duration, Evaluator);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<double> GenerateTween(this MonoBehaviour component, double from, double to, float duration, Action<double> setter)
        {
            double Evaluator(double a, double b, float t) => a + (b - a) * t;
            var tween = new GoTween<double>(component.gameObject, from, to, duration, Evaluator);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<string> GenerateTween(this MonoBehaviour component, string from, string to, float duration, Action<string> setter)
        {
            string Evaluator(string a, string b, float t)
            {
                var progress = (int)(t * b.Length);
                var arr = new char[Mathf.Max(a.Length, progress)];
                for (int i = 0; i < progress; i++) arr[i] = b[i];
                for (int i = progress; i < a.Length; i++) arr[i] = a[i];
                return new string(arr);
            }

            var tween = new GoTween<string>(component.gameObject, from, to, duration, Evaluator);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<Rect> GenerateTween(this MonoBehaviour component, Rect from, Rect to, float duration, Action<Rect> setter)
        {
            Rect Evaluator(Rect a, Rect b, float t) =>
                new Rect(
                    a.x + (b.x - a.x) * t,
                    a.y + (b.y - a.y) * t,
                    a.width + (b.width - a.width) * t,
                    a.height + (b.height - a.height) * t);

            var tween = new GoTween<Rect>(component.gameObject, from, to, duration, Evaluator);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<(Vector3 position, Quaternion rotation)> GenerateTween(this MonoBehaviour component,Transform from, Transform to, float duration, Action<(Vector3 position, Quaternion rotation)> setter)
        {
            (Vector3 position, Quaternion rotation) Evaluator((Vector3 position, Quaternion rotation) a,(Vector3 position, Quaternion rotation) b, float t)
            {
                return (Vector3.Lerp(a.position, b.position, t), Quaternion.Lerp(a.rotation, b.rotation, t));
            }

            var tween = new GoTween<(Vector3 position, Quaternion rotation)>(component.gameObject, (from.position, from.rotation), (to.position, to.rotation), duration, Evaluator);
            
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<Vector3> GenerateTween(this MonoBehaviour component, Vector3 from, Vector3 to, float duration, Action<Vector3> setter)
        {
            var tween = new GoTween<Vector3>(component.gameObject, from, to, duration, Vector3.Lerp);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<Vector2> GenerateTween(this MonoBehaviour component, Vector2 from, Vector2 to, float duration, Action<Vector2> setter)
        {
            var tween = new GoTween<Vector2>(component.gameObject, from, to, duration, Vector2.Lerp);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<Quaternion> GenerateTween(this MonoBehaviour component, Quaternion from, Quaternion to, float duration, Action<Quaternion> setter)
        {
            var tween = new GoTween<Quaternion>(component.gameObject, from, to, duration, Quaternion.Lerp);
            tween.AddOnUpdate(setter);
            return tween;
        }

        public static GoTween<Color> GenerateTween(this MonoBehaviour component, Color from, Color to, float duration, Action<Color> setter)
        {
            var tween = new GoTween<Color>(component.gameObject, from, to, duration, Color.Lerp);
            tween.AddOnUpdate(setter);
            return tween;
        }
    }
}