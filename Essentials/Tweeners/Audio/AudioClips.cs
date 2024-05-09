using System.ComponentModel;
using AnimFlex.Sequencer.Clips;
using UnityEngine;

namespace AnimFlex
{
	[DisplayName( "Tweener Audio Volume" )]
	[Category( "Tweener/Audio/Volume" )]
	public class CTweenerAudioVolume : CTweener<AudioSource, float, TweenerGeneratorAudioVolume> { }
}