using System;
using System.Diagnostics;
using AnimFlex.Tweener;
using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Core
{
    /// <summary>
    /// the first entry point for AnimFlex functionality
    /// </summary>
    internal sealed class AnimFlexInitializer : MonoBehaviour
    {
        [SerializeField] internal AnimFlexSettings m_animFlexSettings;

        public static AnimFlexInitializer Instance => m_instance;
        private static AnimFlexInitializer m_instance;

        public static event Action onTick = delegate { };
        public static event Action onInit = delegate { };

        private void Awake()
        {
            if (m_instance != null)
            {
                Debug.LogError(
                    $"There should be only one instance of AnimFlexInitializer in the game." +
                    $"the new instance will be destroyed!");
                Destroy(gameObject);
            }

            m_instance = this;
            onInit();
        }
        private void Update() => onTick();
    }
}