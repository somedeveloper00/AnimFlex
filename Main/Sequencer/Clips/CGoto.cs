﻿using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Go to")]
    [Category("Branch/Go to")]
    public sealed class CGoto : Clip
    {
        [Tooltip("The node index to play after this node")]
        public VariableFetch<NodeSelection> index;

        protected override void OnStart()
        {
            InjectVariable(ref index);
            PlayIndex(index.value.index);
        }
        public override void OnEnd() { }
    }

    [DisplayName("Go to If")]
    [Category("Branch/Go to If")]
    public sealed class CGotoIf : Clip
    {
        [Tooltip("The node index to play after this node")]
        public VariableFetch<NodeSelection> index;

        [Tooltip("Only go to index if this condition is true")]
        public VariableFetch<bool> condition;

        [Tooltip("Invert the condition")]
        public bool invert;

        protected override void OnStart()
        {
            InjectVariable(ref index);
            InjectVariable(ref condition);
            if (condition.value ^ invert)
                PlayIndex(index.value.index);
            else
                PlayNext();
        }
        public override void OnEnd() { }
    }
}
