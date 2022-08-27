using System;
using System.Diagnostics;
using System.Dynamic;
using AnimFlex.Sequencer;
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
        public AnimFlexSettings Settings { get; private set; }
        public TweenerController TweenerController { get; private set; }
        public EaseEvaluator EaseEvaluator { get; private set; }
        public SequenceController SequenceController { get; private set; }
        
        public static AnimFlexCore Instance => m_instance;
        private static AnimFlexCore m_instance;

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

            InitializeCoreGameObject();
        }

        private static void InitializeCoreGameObject()
        {
            // setup the controller gameObject
            var go = new GameObject("_AnimFlex_mgr");
            go.isStatic = true;
            var core = go.AddComponent<AnimFlexCore>();
            
            // initializing locally
            {
                m_instance = core;
            
                // initialize systems in order
                core.Settings = AnimFlexSettings.Initialize();
                core.TweenerController = new TweenerController();
                core.EaseEvaluator = new EaseEvaluator();
                core.SequenceController = new SequenceController();
            }
            
#if UNITY_EDITOR
            if(Application.isPlaying)
#endif
            DontDestroyOnLoad(go);
        }

        public void Tick(float deltaTime)
        {
            SequenceController.Tick(deltaTime);
            TweenerController.Tick(deltaTime);
        }
    }
}