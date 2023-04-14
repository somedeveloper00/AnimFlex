using UnityEngine;

namespace AnimFlex.Core.Proxy {
    public class AnimFlexCoreProxyScaled : AnimflexCoreProxy {
        
        [Tooltip( "Sets this proxy as default for this type" )] 
        [SerializeField] bool setDefault = true;
        
        public static AnimFlexCoreProxyScaled Default { get; private set; }

        void OnEnable() { if (setDefault) Default = this; }
        
        protected override float GetDeltaTime() => Time.deltaTime;
    }
}