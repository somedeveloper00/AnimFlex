using System;
using System.ComponentModel;
using AnimFlex.Sequencer;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AnimFlex {
    [DisplayName("Destroy GameObject")]
    [Category("GameObject/Destroy")]
    [Serializable]
    sealed class GameObjectDestroy : Clip {
        
        public UnityEngine.GameObject gameObject;
        
        protected override void OnStart() {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                try {
                    Object.DestroyImmediate( gameObject );
                    // ReSharper disable once EmptyGeneralCatchClause
                } catch { } // for complex errors i.e. destroying objects inside prefab
            else
#endif
                Object.Destroy( gameObject );
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [DisplayName("Change Parent")]
    [Category("GameObject/Change Parent")]
    [Serializable]
    sealed class GameObjectReParent : Clip {
        
        public UnityEngine.GameObject gameObject;
        public Transform newParent;
        public bool worldPositionStays = true;
        
        protected override void OnStart() {
            gameObject.transform.SetParent( newParent, worldPositionStays );
            PlayNext();
        }
        public override void OnEnd() { }
    }

    [DisplayName("Set Active")]
    [Category("GameObject/Set Active")]
    [Serializable]
    sealed class GameObjectSetActive : Clip {
        
        public UnityEngine.GameObject gameObject;
        
        [Tooltip("Active state to set to")]
        public bool active = false;
        
        protected override void OnStart() {
            gameObject.SetActive( active );
            PlayNext();
        }
        public override void OnEnd() { }
    }

    public abstract class GameObjectInstantiate : Clip {
        
        public UnityEngine.GameObject gameObject;
        
        [Tooltip("If true, it'll set the instantiated object to active")]
        public bool andSetActive = true;

        [Tooltip( "If true, it'll play the attacjed sequence of the instantiated object" )]
        public bool andPlayItsSequence = false;

        protected override void OnStart() {
            var obj = instantiate();
            if (andSetActive) obj.SetActive( true );
            if (andPlayItsSequence) obj.GetComponent<SequenceAnim>()?.PlaySequence();
            PlayNext();
        }

        protected abstract UnityEngine.GameObject instantiate();
        public override void OnEnd() { }
    }

    [DisplayName( "Instanciate ()" )]
    [Category( "GameObject/Instantiate/()" )]
    [Serializable]
    sealed class GameObjectInstantiate_Simple : GameObjectInstantiate {
        
        protected override UnityEngine.GameObject instantiate() => Object.Instantiate( gameObject );
    }

    [DisplayName( "Instanciate (parent)" )]
    [Category( "GameObject/Instantiate/(Parent)" )]
    [Serializable]
    sealed class GameObjectInstantiate_Par : GameObjectInstantiate {
        
        public Transform parent;
        public bool worldPositionStays = true;

        protected override UnityEngine.GameObject instantiate() =>
            Object.Instantiate( gameObject, parent, worldPositionStays );
    }

    [DisplayName( "Instanciate (position, rotation, parent)" )]
    [Category( "GameObject/Instantiate/(position, rotation, Parent)" )]
    [Serializable]
    sealed class GameObjectInstantiate_PosRotPar : GameObjectInstantiate {
        
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;

        protected override UnityEngine.GameObject instantiate() =>
            Object.Instantiate( gameObject, position, rotation, parent );
    }
}