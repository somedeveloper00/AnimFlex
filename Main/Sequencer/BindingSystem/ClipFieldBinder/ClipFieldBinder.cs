using System;
using System.Reflection;
using AnimFlex.Sequencer.BindingSystem;
using AnimFlex.Sequencer.Clips;
using UnityEngine;

namespace AnimFlex.Sequencer.Binding {

    /// <summary>
    /// binds clip fields with the given value
    /// </summary>
    [Serializable]
    public abstract class ClipFieldBinder {

        /// <summary>
        /// name to be reffered to by
        /// </summary>
        public string name;
        
        [SerializeField]
        internal FieldSelection[] selections;

        /// <summary>
        /// Binds the value to all the selections. retruns the success state
        /// </summary>
        internal abstract bool Bind(Sequence sequence);

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
    public abstract class ClipFieldBinder<T> : ClipFieldBinder {

        [Tooltip("value to bind")]
        [SerializeField] internal T value;

        internal override Type GetselectionValueType() => typeof(T);

        internal override bool Bind(Sequence sequence) {
            for (int i = 0; i < selections.Length; i++) {
                var selection = selections[i];
                if (selection.clipIndex < 0 || selection.clipIndex > sequence.nodes.Length)
                    return false;
                return BindingUtils.SetFieldValueForClip( sequence.nodes[selection.clipIndex].clip, selection.fieldName, value );
            }

            return true;
        }

    }
}