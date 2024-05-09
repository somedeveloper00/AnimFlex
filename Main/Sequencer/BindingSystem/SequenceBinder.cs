using System;
using UnityEngine;

namespace AnimFlex.Sequencer.BindingSystem
{
    /// <summary>
    /// Binds a value to the selected fields of the given <see cref="SequenceAnim"/>
    /// </summary>
    [Serializable]
    public abstract class SequenceBinder
    {
        [Tooltip("Sequence to bind the value to")]
        public SequenceAnim sequenceAnim;

        [Tooltip("Selections to bind the value to")]
        [SerializeField] internal int variableIndex;

        /// <summary>
        /// Binds the value to all the selections. retruns the success state
        /// </summary>
        public abstract bool Bind();

        internal abstract Type GetselectionValueType();
    }

    [Serializable]
    public abstract class SequenceBinder<T> : SequenceBinder
    {
        [Tooltip("Value to bind")]
        public T value;

        internal override Type GetselectionValueType() => typeof(T);

        public override bool Bind()
        {
            if (sequenceAnim.sequence.variables[variableIndex] is Variable<T> v)
            {
                v.Value = value;
                return true;
            }
            return false;
        }
    }
}