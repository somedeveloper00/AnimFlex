using System;
using UnityEngine;

namespace AnimFlex.Core
{
    internal class AnimFlexSettings : ScriptableObject
    {
        private static AnimFlexSettings m_instance;
        public static AnimFlexSettings Instance => m_instance;

        private void OnEnable() => m_instance = this;

        [Header("Tweener")] 
        [Tooltip("If a frame takes more time than this, Tweener will ignore it")]
        public float deltaTimeIgnoreThreshold = 0.2f;

        [Tooltip("Maximum amount of tweens (marked as Deleted) to actually delete per frame")]
        public int maxTweenDeletionPerFrame = 5;

        [Tooltip("Maximum amount of tweens at once")]
        public int maxTweenCount = 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2;

        [Space] 
        [Header("Ease")] 
        public int easeSampleCount = 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2;
        public float period = 0;
        public float overShoot = 1.70158f;
    }
}