using UnityEngine;

namespace AnimFlex.Core.Proxy {
    public class AnimFlexCoreProxyUnscaled : AnimflexCoreProxy {
        
        [Tooltip( "Sets this proxy as default for this type" )] 
        public bool setDefault = true;
        
        public static AnimFlexCoreProxyUnscaled Default { get; private set; }

        void OnEnable() {
            if (setDefault) Default = this;
        }
        
        public override void LateUpdate() => core.Tick( Time.unscaledTime );
    }
}