using UnityEngine;

namespace AnimFlex.Core.Proxy {
    public class AnimFlexCoreProxyScaled : AnimflexCoreProxy {
        
        [Tooltip( "Sets this proxy as default for this type" )] 
        public bool setDefault = true;
        
        public static AnimFlexCoreProxyScaled Default { get; private set; }

        void OnEnable() {
            if (setDefault) Default = this;
        }
        
        public override void LateUpdate() => core.Tick( Time.deltaTime );
    }
}