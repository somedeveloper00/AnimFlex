using System;
using AnimFlex.Sequencer;
using AnimFlex.Tweening;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Core
{
    /// <summary>
    /// the first entry point for AnimFlex functionality. NOT TO BE USED MANUALLY
    /// </summary>
    internal sealed class AnimFlexCore : MonoBehaviour {
        public AnimFlexSettings Settings { get; private set; } = AnimFlexSettings.Instance;
        public EaseEvaluator EaseEvaluator { get; } = new EaseEvaluator();
        public TweenerController TweenerController { get; } = new TweenerController();
        public SequenceController SequenceController { get; } = new SequenceController();

        public static AnimFlexCore Instance => m_instance;
        private static AnimFlexCore m_instance;

        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSceneLoad )]
        internal static void Initialize() {
            if (m_instance != null) {
                Debug.LogError(
                    $"There should be only one instance of AnimFlexCore in the game." +
                    $"the old instance will be destroyed!" );
                Destroy( m_instance );
            }

            InitializeCoreGameObject();
        }

        private static void InitializeCoreGameObject() {
            // setup the controller gameObject
            var go = new GameObject( "_AnimFlex_mgr" );
            go.isStatic = true;
            AnimFlexSettings.Initialize();
            var core = go.AddComponent<AnimFlexCore>();
            m_instance = core;

#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
                DontDestroyOnLoad( go );
        }
        
#if UNITY_EDITOR
        private void OnValidate() {
            if (!Application.isPlaying && this != m_instance) {
                EditorApplication.delayCall += () => {
                    if (gameObject != null) DestroyImmediate(gameObject);
                };
            }
        }
#endif

        void LateUpdate() {
            Tick( Time.deltaTime );
        }

        public void Tick(float deltaTime) {
            SequenceController.Tick( deltaTime );
            TweenerController.Tick( deltaTime );
        }
    }
}
