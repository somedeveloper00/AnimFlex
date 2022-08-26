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
            go.AddComponent<AnimFlexCore>();
            DontDestroyOnLoad(go);
        }

        private void Awake()
        {
            m_instance = this;
            
            // initialize systems in order
            Settings = AnimFlexSettings.Initialize();
            TweenerController = new TweenerController();
            EaseEvaluator = new EaseEvaluator();
            SequenceController = new SequenceController();
        }

        private void Update()
        {
            SequenceController.Tick();
            TweenerController.Tick();
        }
    }
}