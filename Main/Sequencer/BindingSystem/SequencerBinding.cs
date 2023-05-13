using System.Linq;
using AnimFlex.Sequencer.Binding;
using UnityEngine;

namespace AnimFlex.Sequencer.BindingSystem {
    [RequireComponent(typeof(SequenceAnim))]
    [AddComponentMenu("AnimFlex/Sequencer Binding")]
    [ExecuteAlways]
    public class SequencerBinding : MonoBehaviour {

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
            if (bindOnPlay && ( !binded || rebindOnEveryPlay )) Bind();
        }

        /// <summary>
        /// Binds the values to the <see cref="SequenceAnim"/> attached to this gameobject
        /// </summary>
        public void Bind() {
            resolveSequencer();
            for (int i = 0; i < clipFieldBinders.Length; i++) {
                if (clipFieldBinders[i].Bind( _sequencer.sequence ) == false) {
                    Debug.LogError( $"Failed to bind \'{clipFieldBinders[i].name}\' (at index {i})" );
                }
            }
        }

        /// <summary>
        /// Set the value for the binding with the given name. If multiple binding with the same name exists, only
        /// the first one will take the value
        /// </summary>
        public void Assign<T>(string bindingName, T value) {
            for (int i = 0; i < clipFieldBinders.Length; i++) {
                if (clipFieldBinders[i].name == bindingName) {
                    var type = clipFieldBinders[i].GetselectionValueType();
                    if (type != typeof(T) && !type.IsAssignableFrom( typeof(T) )) {
                        throw new System.Exception( $"Value type mismatch for binding {bindingName}. Type is {type}" );
                    }
                    clipFieldBinders[i].AssignValue( value );
                    return;
                }
            }
        }

        void resolveSequencer() => _sequencer ??= GetComponent<SequenceAnim>();
    }
}