using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AnimFlex.Core
{
    internal class AnimFlexSettings : ScriptableObject
    {
        public static AnimFlexSettings Instance => AnimFlexCore.Instance.Settings;

        public static AnimFlexSettings Initialize()
        {
            var settings = Resources.Load<AnimFlexSettings>("AnimFlexSettings");
            if (settings == null)
            {
                // create asset
                settings = ScriptableObject.CreateInstance<AnimFlexSettings>();
#if UNITY_EDITOR
                settings.name = "AnimFlexSettings";

                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
	                AssetDatabase.CreateFolder("Assets", "Resources");
                }
                AssetDatabase.CreateAsset(settings, "Assets/Resources/AnimFlexSettings.asset");
                AssetDatabase.Refresh();
                Debug.LogWarning($"No settings found for AnimFlex: A new one was generated automatically");
#endif
            }

            return settings;
        }

        [Header("Sequence")]
        [Tooltip("Maximum capacity of active Sequences playing at the same time ( it will automatically double in size when filled)")]
        public int sequenceMaxCapacity = 1024;

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
