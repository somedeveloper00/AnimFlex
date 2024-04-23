using UnityEngine;

namespace AnimFlex.Core.Proxy
{
    public abstract class AnimflexCoreProxy : MonoBehaviour
    {
        private AnimFlexCore m_core;

        internal AnimFlexCore core
        {
            get
            {
                if (m_core) return m_core;
                m_core = gameObject.AddComponent<AnimFlexCore>();
                return m_core;
            }
        }

        private void OnDestroy()
        {
            if (m_core) Destroy(m_core);
        }

        private void LateUpdate() => core.Tick(GetDeltaTime());

        protected abstract float GetDeltaTime();


        #region Generating a default instance on startup of application

        /// <summary>
        /// The default AnimFlex core proxy that gets generated on application startup and lives until the end.
        /// It's always safe to use it.
        /// </summary>
        public static AnimFlexCoreProxyScaled MainDefault { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void Initialize()
        {
            if (MainDefault != null)
            {
                Debug.LogError($"Another MainDefault exists. This is an unexpected error, to resolve it we're creating a new one" +
                                $" and deleting the previous one.");
                Destroy(MainDefault);
            }

            InitializeCoreGameObject();
        }

        private static void InitializeCoreGameObject()
        {
            // initialize settings
            AnimFlexSettings.Initialize();

            // setup the default gameObject
            var go = new GameObject("_AnimFlex_mgr");
            go.isStatic = true;
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
                DontDestroyOnLoad(go);
            MainDefault = go.AddComponent<AnimFlexCoreProxyScaled>();
        }

        #endregion

    }
}