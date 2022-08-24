using UnityEngine;

namespace AnimFlex.Core
{
    internal class AnimFlexSettings : ScriptableObject
    {
        public static AnimFlexSettings Instance => AnimFlexInitializer.Instance.m_animFlexSettings;
        
        [Header("Tweener")]
        
        [Tooltip("If a frame takes more time than this, Tweener will ignore it")]
        public float deltaTimeIgnoreThreshold = 0.2f;
        
        [Tooltip("The maximum milliseconds possible in a tween frame update (tick)")]
        public int maxMillisecondsForTweenUpdate = 33;


    }
    
    
}