namespace AnimFlex.Core.Proxy
{
    public class AnimFlexCoreProxyManual : AnimflexCoreProxy
    {
        private float nextDeltaTime = 0;

        protected override float GetDeltaTime()
        {
            var t = nextDeltaTime;
            nextDeltaTime = 0;
            return t;
        }

        /// <summary>
        /// Ticks the core with the given deltatime
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Tick(float deltaTime) => nextDeltaTime += deltaTime;
    }
}