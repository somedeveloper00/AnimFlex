using UnityEngine;
using UnityEngine.Events;

namespace AnimFlex.Tweener
{
    public sealed class TweenerAnim : MonoBehaviour
    {
        [SerializeField] public GeneratorData generatorData;
        
        [Tooltip("If checked, it'll play right when the game object activates")]
        [SerializeField] internal bool playOnStart;

        private void Reset()
        {
            if (generatorData == null) generatorData = new GeneratorData();
            playOnStart = true;
            generatorData.fromObject = gameObject;
            generatorData.duration = 1;
            generatorData.delay = 0.5f;
        }

        private Tweener m_tweener;

        #region wrappers
        public UnityEvent onStart => generatorData.onStart;
        public UnityEvent onComplete => generatorData.onComplete;
        public UnityEvent onKill => generatorData.onKill;
        public UnityEvent onUpdate => generatorData.onUpdate;

        /// <summary>
        /// returns the last generated Tweener, if it's active.
        /// </summary>
        public bool TryGetTweener(out Tweener tweener)
        {
            if (m_tweener != null && 
                !m_tweener.flag.HasFlag(TweenerFlag.Deleting))
            {
                tweener = m_tweener;
                return true;
            }

            tweener = null;
            return false;
        }
        
        #endregion
        
        private void Start()
        {
            if (playOnStart)
            {
                PlayOrRestart();
            }
        }

        
        /// <summary>
        /// generates the tweener and plays it if it's not playing already. otherwise generates a new tweener and plays it.
        /// </summary>
        public void PlayOrRestart()
        {
            // kill if already running
            if (m_tweener != null && !m_tweener.flag.HasFlag(TweenerFlag.Deleting))
                Kill(false, false);
            
            // generate new tweener if possible
            if (GeneratorDataUtil.TryGenerateTweener(generatorData, out m_tweener))
            {
                if (m_tweener == null)
                {
                    Debug.LogError($"Unexpected Error happened while generating tweener on {generatorData.fromObject}!");
                }
            }
        }

        /// <summary>
        /// kills the tweener right away
        /// </summary>
        public void Kill(bool complete = true, bool onCompleteCallback = true)
        {
            TweenerController.Instance.KillTweener(m_tweener, complete, onCompleteCallback);
        }
    }
}