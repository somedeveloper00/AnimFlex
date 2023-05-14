using System;
using UnityEngine;

namespace AnimFlex.Sequencer.BindingSystem {

    /// <summary>
    /// Binds a value to the selected fields of the given <see cref="Sequencer.SequenceAnim"/>
    /// </summary>
    [Serializable]
    public abstract class SequenceBinder {

        /// <summary>
        /// Sequence to bind the value to
        /// </summary>
        [Tooltip("Sequence to bind the value to")]
        public SequenceAnim sequenceAnim;
        
        /// <summary>
        /// Selections to bind the value to
        /// </summary>
        [Tooltip("Selections to bind the value to")]
        [SerializeField] internal FieldSelection[] selections;

        /// <summary>
        /// Binds the value to all the selections. retruns the success state
        /// </summary>
        public abstract bool Bind();

        internal abstract Type GetselectionValueType();
        
        [Serializable]
        internal sealed class FieldSelection {
            /// <summary>
            /// index of the <see cref="Clip"/> in the <see cref="Sequence"/>'s clip list
            /// </summary>
            public int clipIndex;

            /// <summary>
            /// name of the field in the <see cref="Clip"/> to bind
            /// </summary>
            public string fieldName;
        }
    }
    
    [Serializable]
    public abstract class SequenceBinder<T> : SequenceBinder {

        [Tooltip("Value to bind")]
        [SerializeField] internal T value;

        internal override Type GetselectionValueType() => typeof(T);

        public override bool Bind() {
            for (int i = 0; i < selections.Length; i++) {
                var selection = selections[i];
                if (selection.clipIndex < 0 || selection.clipIndex > sequenceAnim.sequence.nodes.Length)
                    return false;
                // bind 
                if (!BindingUtils.SetFieldValueForClip( sequenceAnim.sequence.nodes[selection.clipIndex].clip, selection.fieldName, value ))
                    return false;
            }

            return true;
        }

    }
}