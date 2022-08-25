using System;
using UnityEngine;
using UnityEngine.Events;

namespace AnimFlex.Tweener
{
    public sealed class TweenerAnim : MonoBehaviour
    {
        [SerializeField] internal GeneratorData generatorData;
        [SerializeField] internal bool playOnStart;

        private Tweener m_tweener;

        #region wrappers
        public float delay
        {
            get => generatorData.delay;
            set => generatorData.delay = value;
        }
        public float duration
        {
            get => generatorData.duration;
            set => generatorData.duration = value;
        }

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
            if (m_tweener != null)
                Kill(false, false);
            
            // generate new tweener if possible
            if (DataUtil.TryGenerateTweener(generatorData, out m_tweener))
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
            TweenerController.KillTweener(m_tweener, complete, onCompleteCallback);
        }
    }
}