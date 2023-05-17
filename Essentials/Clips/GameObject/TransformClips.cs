using System;
using System.ComponentModel;
using AnimFlex.Sequencer;
using UnityEngine;

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
}