using System;
using System.ComponentModel;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Destroy Component")]
    [Category("GameObject/Component/Destroy")]
    [Serializable]
    public sealed class ComponentDestroy : Clip
    {
        public Behaviour component;

        protected override void OnStart()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                Object.DestroyImmediate(component);
            else
#endif
                Object.Destroy(component);
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [DisplayName("Set Component Active")]
    [Category("GameObject/Component/Set Active")]
    [Serializable]
    public sealed class ComponentSetActive : Clip
    {
        public Behaviour component;
        [Tooltip("The active state of the component")]
        public bool active;

        protected override void OnStart()
        {
            component.enabled = active;
            PlayNext();
        }

        public override void OnEnd() { }
    }
}