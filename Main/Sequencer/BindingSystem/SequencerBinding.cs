using AnimFlex.Sequencer.Binding;
using UnityEngine;

namespace AnimFlex.Sequencer.BindingSystem {
    [RequireComponent(typeof(SequenceAnim))]
    internal class SequencerBinding : MonoBehaviour {

        [Tooltip("Binds to SequenceAnim on play")]
        [SerializeField] internal bool bindOnPlay = true;
        
        [Tooltip("Binds to SequenceAnim on every single time it's played")]
        [SerializeField] internal bool rebindOnEveryPlay = true;

        [SerializeReference]
        internal ClipFieldBinder[] clipFieldBinders;
        
        internal bool binded = false;
        SequenceAnim _sequencer;

        void Awake() {
            binded = false;
            _sequencer = GetComponent<SequenceAnim>();
            _sequencer.beforePlay += OnSequencerOnbeforePlay;
        }

        void OnDestroy() => _sequencer.beforePlay -= OnSequencerOnbeforePlay;

#if UNITY_EDITOR
        [CallMethodOnSequencePreview]
#endif
        void OnSequencerOnbeforePlay() {
            if (bindOnPlay && ( !binded || rebindOnEveryPlay )) { Bind(); }
        }

        /// <summary>
        /// Binds the values to the <see cref="SequenceAnim"/> attached to this gameobject
        /// </summary>
        public void Bind() {
            resolveSequencer();
            for (int i = 0; i < clipFieldBinders.Length; i++) {
                if (clipFieldBinders[i].Bind( _sequencer.sequence ) == false) {
                    Debug.LogError( $"Failed to bind {clipFieldBinders[i].name} (at index {i})" );
                }
            }
        }

        void resolveSequencer() => _sequencer ??= GetComponent<SequenceAnim>();
    }
}