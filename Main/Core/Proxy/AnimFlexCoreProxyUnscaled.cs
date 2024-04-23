using UnityEngine;

namespace AnimFlex.Core.Proxy
{
    public class AnimFlexCoreProxyUnscaled : AnimflexCoreProxy
    {
        [Tooltip("Sets this proxy as default for this type")]
        [SerializeField] private bool setDefault = true;

        public static AnimFlexCoreProxyUnscaled Default { get; private set; }

        private void OnEnable()
        {
            if (setDefault) Default = this;
        }

        protected override float GetDeltaTime()
        {
            return Time.unscaledDeltaTime;
        }
    }
}