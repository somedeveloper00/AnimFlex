using UnityEngine;

namespace AnimFlex.Core.Proxy {
    public class AnimFlexCoreProxyUnscaled : AnimflexCoreProxyBase {
        public override void LateUpdate() => core.Tick( Time.unscaledTime );
    }
}