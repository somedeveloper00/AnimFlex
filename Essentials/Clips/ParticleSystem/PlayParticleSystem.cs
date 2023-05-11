using System;
using System.ComponentModel;
using AnimFlex.Sequencer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnimFlex.Essentials.Clips.ParticleSystem {
    [Category("FX/Play Particle System")]
    [DisplayName("Play Particle System")]
    [Serializable]
    public class PlayParticleSystem : Clip {
        
        [Tooltip("The Particle System to play")]
        public UnityEngine.ParticleSystem particleSystem;
        
        [Tooltip("If Enabled, all children with Particle System will be played as well")]
        public bool playWithChildren = true;
        
        [Tooltip("If Enabled, the clip will wait until the particle system finishes playing before moving to the next clip")]
        public bool waitTillFinish = true;

#if UNITY_EDITOR
        float t = 0;
        float longestDuration = 0;
#endif
        
        protected override void OnStart() {
#if UNITY_EDITOR
            if (!Application.isPlaying) {
                t = 0;
                longestDuration = 0;
                if (playWithChildren) {
                    foreach (var ps in particleSystem.GetComponentsInChildren<UnityEngine.ParticleSystem>()) {
                        ps.useAutoRandomSeed = false;
                        ps.randomSeed = (uint)Random.Range( 0, 100 );
                        if (ps.main.duration > longestDuration)
                            longestDuration = ps.main.duration;
                    }
                }
                else {
                    particleSystem.useAutoRandomSeed = false;
                    particleSystem.randomSeed = (uint)Random.Range( 0, 100 );
                    longestDuration = particleSystem.main.duration;
                }
            }
#endif
            particleSystem.Play( playWithChildren );
            if (!waitTillFinish) PlayNext( false );
        }

        public override bool hasTick() => true;

        public override void Tick(float deltaTime) {
#if UNITY_EDITOR
            // in-editor preview
            if (!Application.isPlaying) {
                t += deltaTime;
                particleSystem.Simulate( t, playWithChildren, true );
                if (t > longestDuration) {
                    if (waitTillFinish) PlayNext();
                    else EndSelf();
                }

                return;
            }
#endif
            if (waitTillFinish && !particleSystem.IsAlive( playWithChildren )) PlayNext();

        }

        public override void OnEnd() {
            if (particleSystem.isPlaying) particleSystem.Stop();
        }
    }
}