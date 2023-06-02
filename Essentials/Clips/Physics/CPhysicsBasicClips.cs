using System;
using System.ComponentModel;
using System.Linq;
using AnimFlex.Sequencer;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex {
    [Serializable]
    [DisplayName( "Add Force" )]
    [Category( "Physics/Add Force" )]
    public class CAddForce : Clip {
        public Rigidbody[] rigidbodies;
        public Vector3 force = Vector3.zero;
        public ForceMode mode;

        protected override void OnStart() {
            foreach (var rigidbody in rigidbodies) rigidbody.AddForce( force, mode );
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Serializable]
    [DisplayName( "Add Force 2D" )]
    [Category( "Physics/Add Force 2D" )]
    public class CAddForce2D : Clip {
        public Rigidbody2D[] rigidbodies;
        public Vector3 force = Vector3.zero;
        public ForceMode2D mode;

        protected override void OnStart() {
            foreach (var rigidbody in rigidbodies) rigidbody.AddForce( force, mode );
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Serializable]
    [DisplayName( "Sleep" )]
    [Category( "Physics/Sleep" )]
    public class CRigidbodySleep : Clip {
        public Rigidbody[] rigidbodies;

        protected override void OnStart() {
            foreach (var rigidbody in rigidbodies) rigidbody.Sleep();
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Serializable]
    [DisplayName( "Sleep 2D" )]
    [Category( "Physics/Sleep 2D" )]
    public class CRigidbodySleep2D : Clip {
        public Rigidbody2D[] rigidbodies;

        protected override void OnStart() {
            foreach (var rigidbody in rigidbodies) rigidbody.Sleep();
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Serializable]
    [DisplayName( "Explosion" )]
    [Category( "Physics/Explosion" )]
    public class CRigidbodyExplosion : Clip {
        public AFSelection<Rigidbody>[] selections;
        public float explosionForce = 1;
        [Tooltip("The position of the explosion will be relative to this Transform. If it's empty, the position will " +
                 "be considered in World Space.")]
        public Transform explosionPositionPivot;
        public Vector3 explosionPosition = Vector3.zero;
        public float explosionRadius = 1;
        public float upwardsModifier = 1;
        public ForceMode mode;

        protected override void OnStart() {
            var pos = explosionPositionPivot
                ? explosionPositionPivot.TransformPoint( explosionPosition )
                : explosionPosition;
            foreach (var rigidbody in AFSelection.GetSelectedObjects( selections )) 
                rigidbody.AddExplosionForce( explosionForce, pos, explosionRadius, upwardsModifier, mode );
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Serializable]
    [DisplayName( "Set Velocity" )]
    [Category( "Physics/Set Velocity" )]
    public class CRigidbodySetVelocity : Clip {
        public AFSelection<Rigidbody>[] selections;
        public Vector3 velocity;

        protected override void OnStart() {
            foreach (var rigidbody in AFSelection.GetSelectedObjects( selections )) 
                rigidbody.velocity = velocity;
            PlayNext();
        }

        public override void OnEnd() { }
    }
}