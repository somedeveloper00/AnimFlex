using System;
using System.Diagnostics;
using System.Dynamic;
using AnimFlex.Tweener;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace AnimFlex.Core
{
    /// <summary>
    /// the first entry point for AnimFlex functionality
    /// </summary>
    [AddComponentMenu("")]
    internal sealed class AnimFlexCore : MonoBehaviour
    {
        public static AnimFlexCore Instance => m_instance;
        private static AnimFlexCore m_instance;

        public static event Action onTick = delegate { };
        
        /// <summary>
        /// executes on Awake
        /// </summary>
        public static event Action onInit = delegate { };
        
        public static event Action onEnd = delegate { };

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            if (m_instance != null)
            {
                Debug.LogError(
                    $"There should be only one instance of AnimFlexInitializer in the game." +
                    $"the old instance will be destroyed!");
                Destroy(m_instance);
            }

            InitializeSettings();
            InitializeCoreGameObject();
        }

        private static void InitializeCoreGameObject()
        {
            // setup the controller gameObject
            var go = new GameObject("_AnimFlex_mgr");
            go.isStatic = true;
            m_instance = go.AddComponent<AnimFlexCore>();
            DontDestroyOnLoad(go);
        }

        private static void InitializeSettings()
        {
            var settings = Resources.Load<AnimFlexSettings>("AnimFlexSettings");
            if (settings == null)
            {
                // create asset 
                settings = ScriptableObject.CreateInstance<AnimFlexSettings>();
#if UNITY_EDITOR
                settings.name = "AnimFlexSettings";
                AssetDatabase.CreateAsset(settings, "Assets/Resources/AnimFlexSettings.asset");
                AssetDatabase.Refresh();
                Debug.LogWarning($"No settings found for AnimFlex: A new one was generated automatically");
#endif
            }
        }

        private void Awake()
        {
            m_instance = this; // just to be sure
            onInit();
        }

        private void Update() => onTick();

        private void OnDestroy() => onEnd();
    }
}