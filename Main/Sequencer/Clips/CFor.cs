using System;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [Category("Branch/For")]
    [DisplayName("For")]
    [Serializable]
    public class CFor : Clip
    {

        [Tooltip("The node index to play after this node")]
        public int index;

        [Tooltip("The amount of times to play the node")]
        [Min(0)] public int count;

        [Tooltip("The delay between each iteration")]
        public float inbetweenDelay;
        private int lastIterationIndex = -1;
        private float t;

        protected override void OnStart()
        {
            t = 0;
            lastIterationIndex = -1;
        }

        public override bool hasTick() => true;

        public override void Tick(float deltaTime)
        {
            t += deltaTime;
            for (int i = lastIterationIndex + 1; i < count; i++)
            {
                if (t >= i * inbetweenDelay)
                {
                    // locking finishing of the whole sequence from that branch 
                    Node.sequence.sequenceStopLock++;
                    PlayIndex(index);
                    if (i == count - 1)
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