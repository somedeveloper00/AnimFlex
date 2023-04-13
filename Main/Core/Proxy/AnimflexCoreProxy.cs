using UnityEngine;

namespace AnimFlex.Core.Proxy {
    public abstract class AnimflexCoreProxy : MonoBehaviour {

        AnimFlexCore m_core;

        internal AnimFlexCore core {
            get {
                if (m_core) return m_core;
                m_core = gameObject.AddComponent<AnimFlexCore>();
                return m_core;
            }
        }

        public abstract void LateUpdate();
    }
}