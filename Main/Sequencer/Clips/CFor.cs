using System;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [Category("Branch/For")]
    [DisplayName("For")]
    [Serializable]
    public sealed class CFor : Clip
    {
        [Tooltip("The node index to play after this node")]
        public VariableFetch<NodeSelection> index;

        [Tooltip("The amount of times to play the node")]
        public VariableFetch<uint> count;

        [Tooltip("The delay between each iteration")]
        public VariableFetch<float> inbetweenDelay;
        private int lastIterationIndex = -1;
        private float t;

        protected override void OnStart()
        {
            InjectVariable(ref index);
            InjectVariable(ref count);
            InjectVariable(ref inbetweenDelay);
            t = 0;
            lastIterationIndex = -1;
        }

        public override bool HasTick() => true;

        public override void Tick(float deltaTime)
        {
            t += deltaTime;
            for (int i = lastIterationIndex + 1; i < count.value; i++)
            {
                if (t >= i * inbetweenDelay.value)
                {
                    // locking finishing of the whole sequence from that branch 
                    Node.sequence.sequenceStopLock++;
                    PlayIndex(index.value.index);
                    if (i == count.value - 1)
                    {
                        PlayNext();
                        return;
                    }
                    lastIterationIndex = i;
                }
            }
        }

        public override void OnEnd() { }
    }
}