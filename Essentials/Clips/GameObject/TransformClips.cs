using System;
using System.ComponentModel;
using AnimFlex.Sequencer;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimFlex {
    [DisplayName("Set Sibling")]
    [Category("GameObject/Transform/Set Sibling Index")]
    [Serializable]
    sealed class CTransformSetSibling : Clip {

        public Transform transform;
        public int siblingIndex;
        
        protected override void OnStart() {
            transform.SetSiblingIndex( siblingIndex );
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [DisplayName("Reset Local Transforms")]
    [Category("GameObject/Transform/Reset Local Transforms")]
    [Serializable]
    sealed class CTransformsLocalReset : Clip {

        public AFSelection<Transform>[] transforms;
        public bool localPosition = true;
        public bool localRotation = true;
        public bool localScale = true;

        protected override void OnStart() {
            foreach (var transform in AFSelection.GetSelectedObjects( transforms )) {
                if (!transform) continue;
                if (localPosition) transform.localPosition = Vector3.zero;
                if (localRotation) transform.localRotation = Quaternion.identity;
                if (localScale) transform.localScale = Vector3.one;
            }
            PlayNext();
        }

        public override void OnEnd() { }
    }
}