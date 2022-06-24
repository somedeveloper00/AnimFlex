using UnityEngine;
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable UnusedMember.Local

namespace AnimFlex.Tweening
{
    internal static class EasingFuncs
    {
        [EasingIdentifier(0, "Linear")]
        private static float Linear(float x) => x;

        [EasingIdentifier(20, "InSine")]
        private static float InSine(float x) => 1 - Mathf.Cos((x * Mathf.PI) / 2);

        [EasingIdentifier(30, "OutSine")]
        private static float OutSine(float x) => Mathf.Sin((x * Mathf.PI) / 2);

        [EasingIdentifier(40, "InOutSine")]
        private static float InOutSine(float x) => -(Mathf.Cos(Mathf.PI * x) - 1) / 2;

        [EasingIdentifier(50, "InQuad")]
        private static float InQuad(float x) => x * x;

        [EasingIdentifier(60, "OutQuad")]
        private static float OutQuad(float x) => 1 - (1 - x) * (1 - x);

        [EasingIdentifier(70, "InOutQuad")]
        private static float InOutQuad(float x) => x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;

        [EasingIdentifier(80, "InQubic")]
        private static float InQubic(float x) => x * x * x;

        [EasingIdentifier(90, "OutQubic")]
        private static float OutQubic(float x) => 1 - Mathf.Pow(1 - x, 3);

        [EasingIdentifier(100, "InOutQubic")]
        private static float InOutQubic(float x) => x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        [EasingIdentifier(110, "InExpo")]
        private static float InExpo(float x) => x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);

        [EasingIdentifier(120, "OutExpo")]
        private static float OutExpo(float x) => x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);

        [EasingIdentifier(130, "InOutExpo")]
        private static float InOutExpo(float x) => x == 0
                                                    ? 0
                                                    : x == 1
                                                        ? 1
                                                        : x < 0.5
                                                            ? Mathf.Pow(2, 20 * x - 10) / 2
                                                            : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;

        [EasingIdentifier(140, "InBack")]
        private static float InBack(float x)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;

            return c3 * x * x * x - c1 * x * x; 
        }

        [EasingIdentifier(150, "OutBack")]
        private static float OutBack(float x)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;

            // return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
            return (x) * x * ((1.70158f + 1) * x - 1.70158f);
        }

        [EasingIdentifier(160, "InOutBack")]
        private static float InOutBack(float x)
        { 
            const float c1 = 1.70158f;
            const float c2 = c1 + 1;

            return x < 0.5
                ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }
    }
}