using System;
using UnityEngine;

namespace AnimFlex.Core
{
    public class AnimFlexInitializer : MonoBehaviour
    {
        [SerializeField]
        private AnimFlexSettings m_animFlexSettings;
        
        private static AnimFlexInitializer m_instance;

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
        }
    }
}