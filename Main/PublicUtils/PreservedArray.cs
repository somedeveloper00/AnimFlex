using System;
using UnityEngine;

namespace AnimFlex
{
    /// <summary>
    /// An array bending solution to preserve extra spaces and modify the array with the least calls to allocations.
    ///  
    /// it also supports queued elements, which will be useful for scheduling your pipelines
    /// </summary>
    public class PreservedArray<T>
    {
        private T[] values;

        public int Length { get; private set; } = 0;
        private int queueLength = 0;

        public PreservedArray(int preserveLength = 256)
        {
            values = new T[preserveLength];
        }

        public T this[int i]
        {
            get
            {
                RangeCheck(i);
                return values[i];
            }
            set
            {
                RangeCheck(i);
                values[i] = value;
            }
        }


        /// <summary>
        /// adds an active element to the end of the array
        /// </summary>
        public void AddActive(T value)
        {
            LengthExceedCheck();
            
            // move first queue to end of array
            values[Length + queueLength + 1] = values[Length + 1];
            
            // replace the old duplicate with new value
            values[Length++] = value;
        }

        /// <summary>
        /// adds the element to the queue list. order does not matter here
        /// </summary>
        public void AddToQueue(T value)
        {
            LengthExceedCheck();
            values[Length + queueLength] = value;
            queueLength++;
        }

        /// <summary>
        /// lets all queued elements in the active array range, and clears the queue list
        /// </summary>
        public void FlushQueueIn()
        {
            Length += queueLength;
            queueLength = 0;
        }

        /// <summary>
        /// removes an element from the array. it will NOT remove anything. it'll only do some
        ///  replacements to make sure the specified element will no longer be used
        /// </summary>
        public void RemoveAt(int index)
        {
            RangeCheck(index);
            
            // copy the last active element in place of the removing one
            values[index] = values[--Length];
            
            // copy the last queued element in place of the old duplicate one
            values[Length] = values[Length + queueLength];
        }

        private void RangeCheck(int i)
        {
            if (i >= Length) throw new IndexOutOfRangeException(this.ToString());
        }

        private void LengthExceedCheck()
        {
            if (values.Length <= Length + queueLength)
            {
                Debug.LogWarning(
                    $"Preserved array exceeded it's limit: automatically doubled from {values.Length} to {values.Length * 2}");
                // doubling the preserved length
                var tmp = new T[values.Length * 2];
                for (int i = 0; i < values.Length; i++) tmp[i] = values[i];
                values = tmp;
            }
        }
    }
}