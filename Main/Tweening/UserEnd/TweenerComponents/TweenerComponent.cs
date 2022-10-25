using UnityEngine;
using UnityEngine.Events;

namespace AnimFlex.Tweening
{
    public abstract class TweenerComponent : MonoBehaviour { }
    public abstract class TweenerComponent<T> : TweenerComponent where T : TweenerGenerator, new()
    {
        
        public T generator;
        
        [Tooltip("If checked, it'll play right when the game object activates")]
        [SerializeField] internal bool playOnStart;

        private void Reset()
        {
            if (generator == null) generator = new T();
            generator.Reset(gameObject);
        }

        private Tweener m_tweener;

        #region wrappers
        public UnityEvent onStart => generator.onStart;
        public UnityEvent onComplete => generator.onComplete;
        public UnityEvent onKill => generator.onKill;
        public UnityEvent onUpdate => generator.onUpdate;

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
            if (generator.TryGenerateTween(out m_tweener))
            {
                if (m_tweener == null)
                {
                    Debug.LogError($"Unexpected Error happened while generating tweener!");
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