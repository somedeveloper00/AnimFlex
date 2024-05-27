using System;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnimFlex.Sequencer.Clips
{
    [Category("FX/Play Particle System")]
    [DisplayName("Play Particle System")]
    [Serializable]
    public sealed class PlayParticleSystem : Clip
    {
        [Tooltip("The Particle System to play")]
        public ParticleSystem particleSystem;

        [Tooltip("If Enabled, all children with Particle System will be played as well")]
        public bool playWithChildren = true;

        [Tooltip("If Enabled, the clip will wait until the particle system finishes playing before moving to the next clip")]
        public bool waitTillFinish = true;
        private float t;
        private float longestDuration;

        protected override void OnStart()
        {
            t = 0;
            longestDuration = 0;
            if (playWithChildren)
            {
                foreach (var ps in particleSystem.GetComponentsInChildren<ParticleSystem>())
                {
                    if (ps.main.duration > longestDuration)
                        longestDuration = ps.main.duration;
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        ps.useAutoRandomSeed = false;
                        ps.randomSeed = (uint)Random.Range(0, 100);
                    }
#endif
                }
            }
            else
            {
                longestDuration = particleSystem.main.duration;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    particleSystem.useAutoRandomSeed = false;
                    particleSystem.randomSeed = (uint)Random.Range(0, 100);
                }
#endif
            }

            particleSystem.Play(playWithChildren);
            if (!waitTillFinish) PlayNext(false);
        }

        public override bool HasTick() => true;

        public override void Tick(float deltaTime)
        {
            t += deltaTime;
#if UNITY_EDITOR
            // in-editor preview
            if (!Application.isPlaying)
            {
                particleSystem.Simulate(t, playWithChildren, true);
            }
#endif
            if (t > longestDuration)
            {
                if (waitTillFinish) PlayNext();
                else EndSelf();
            }
        }

        public override void OnEnd()
        {
            if (particleSystem) particleSystem.Stop();
        }
    }
}