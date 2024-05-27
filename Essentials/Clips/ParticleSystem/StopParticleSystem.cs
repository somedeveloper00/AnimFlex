using System;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [Category("FX/Stop Particle System")]
    [DisplayName("Stop Particle System")]
    [Serializable]
    public sealed class StopParticleSystem : Clip
    {
        [Tooltip("The Particle System to play")]
        public ParticleSystem particleSystem;

        [Tooltip("If Enabled, all children with Particle System will be stopped as well")]
        public bool withChildren = true;

        [Tooltip("Indicates the behavior to stop the particle system")]
        public ParticleSystemStopBehavior systemStopBehavior;

        [Tooltip("If Enabled, the clip will wait until the particle system finishes playing before moving to the next clip")]
        public bool waitTillFinished = false;

        protected override void OnStart()
        {
            particleSystem.Stop(withChildren, systemStopBehavior);
            if (!waitTillFinished) PlayNext();
        }

        public override bool HasTick() => true;

        public override void Tick(float deltaTime)
        {
            if (!particleSystem)
            {
                PlayNext();
                return;
            }
            if (!particleSystem.IsAlive(withChildren))
            {
                PlayNext();
                return;
            }
        }

        public override void OnEnd() { }
    }
}