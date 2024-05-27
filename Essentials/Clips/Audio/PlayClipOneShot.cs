using System;
using System.ComponentModel;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AnimFlex.Sequencer.Clips
{
    [Category("Audio/One Shot")]
    [DisplayName("Play Audio One Shot")]
    [Serializable]
    public sealed class PlayClipOneShot : Clip
    {
        [Tooltip("The audio source to play the clip on. If left empty, it'll try to find one")]
        public AudioSource audioSource;

        [Tooltip("The audio clip that plays")]
        public AudioClip audioClip;

        [Tooltip("The volume of the audio clip to play")]
        public float volume = 1;


        protected override void OnStart()
        {
            var source = audioSource;
            if (source == null)
            {
                source = Object.FindObjectOfType<AudioSource>();
            }
            source.PlayOneShot(audioClip, volume);
            PlayNext();
        }
        public override void OnEnd() { }
    }
}