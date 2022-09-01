﻿using System;
using AnimFlex.Tweener;
using UnityEngine;

namespace AnimFlex.Sequencer.UserEnd
{
    public class SequenceAnim : MonoBehaviour
    {
        [SerializeField] private bool playOnStart = true;
        public Sequence sequence = new();

        private void Start()
        {
            if(playOnStart)
                PlaySequence();
        }

        private void OnValidate()
        {
            sequence.EditorValidate();
        }

        public void PlaySequence()
        {
            sequence.Play();
        }

        private void OnDestroy()
        {
            sequence.Complete();
        }
    }
}