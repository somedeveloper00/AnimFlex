using UnityEngine;

namespace AnimFlex.Core.Proxy {
    public class AnimFlexCoreProxyScaled : AnimflexCoreProxyBase {
        public override void LateUpdate() => core.Tick( Time.deltaTime );
    }
}