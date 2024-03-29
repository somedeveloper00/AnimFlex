﻿using System;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips {
    
    [DisplayName("Wait For Frames")]
    [Category("Misc/Wait Frames")]
    [Serializable]
    public class CWaitFrame : Clip {

        [Tooltip( "The number of frames to wait before playing the next clip" )]
        public int frames = 1;

        int _f = 0;

        protected override void OnStart() => _f = frames;
        public override void OnEnd() { }
        public override bool hasTick() => true;
        public override void Tick(float deltaTime) {
            if (_f-- <= 0) PlayNext();
        }
    }
}