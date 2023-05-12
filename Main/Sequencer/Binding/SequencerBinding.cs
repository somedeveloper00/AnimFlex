using AnimFlex.Sequencer.UserEnd;
using UnityEngine;

namespace AnimFlex.Sequencer.Binding {
    [RequireComponent(typeof(SequenceAnim))]
    internal class SequencerBinding : MonoBehaviour {

        [SerializeReference]
        internal ClipFieldBinder[] clipFieldBinders;
        
        SequenceAnim _sequencer;

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