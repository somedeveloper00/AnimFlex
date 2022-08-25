using UnityEngine;

namespace AnimFlex.Core
{
    internal class AnimFlexSettings : ScriptableObject
    {
        public static AnimFlexSettings Instance => AnimFlexInitializer.Instance.m_animFlexSettings;
        
        [Header("Tweener")]
        
        [Tooltip("If a frame takes more time than this, Tweener will ignore it")]
        public float deltaTimeIgnoreThreshold = 0.2f;

        [Tooltip("Maximum amount of tweens (marked as Deleted) to actually delete per frame")]
        public int maxTweenDeletionPerFrame = 5;

        [Tooltip("Maximum amount of tweens at once")]
        public int maxTweenCount = 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2;

        [Space]
        [Header("Ease Quality")]
        public int easeLowQualitySampleCount = 50;
        public int easeMediumQualitySampleCount = 100;
        public int easeHighQualitySampleCount = 200;
    }
    
    
}