namespace AnimFlex.Core.Proxy {
    public class AnimFlexCoreProxyManual : AnimflexCoreProxyBase {
        public override void LateUpdate() { }
        public void Tick(float deltaTime) => core.Tick( deltaTime );
    }
}