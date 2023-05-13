using System;
using System.ComponentModel;
using AnimFlex.Sequencer;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AnimFlex {
    [DisplayName("Destroy Component")]
    [Category("GameObject/Component/Destroy")]
    [Serializable]
    public class ComponentDestroy : Clip {
        
        public Behaviour component;
        
        protected override void OnStart() {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                Object.DestroyImmediate( component );
            else
#endif
            Object.Destroy( component );
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [DisplayName("Set Component Active")]
    [Category("GameObject/Component/Set Active")]
    [Serializable]
    public class ComponentSetActive : Clip {
        
        public Behaviour component;
        [Tooltip("The active state of the component")]
        public bool active;
        
        protected override void OnStart() {
            component.enabled = false;
            PlayNext();
        }

        public override void OnEnd() { }
    }
}