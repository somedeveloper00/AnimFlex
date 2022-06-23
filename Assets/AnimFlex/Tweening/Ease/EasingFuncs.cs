using UnityEngine;

namespace AnimFlex.Tweening
{
    public static partial class EasingFuncs
    {
        [EasingIdentifier(10)]
        public static float Linear(float x) => x;

        [EasingIdentifier(11)]
        public static float InSine(float x) => x;
    }
}