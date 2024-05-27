using System.ComponentModel;
using AnimFlex.Tweening;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
	[DisplayName( "Tweener Audio Volume" )]
	[Category( "Tweener/Audio/Volume" )]
	public sealed class CTweenerAudioVolume : CTweener<AudioSource, float, TweenerGeneratorAudioVolume> { }
}