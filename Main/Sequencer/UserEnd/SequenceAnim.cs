using AnimFlex.Core;
using AnimFlex.Core.Proxy;
using UnityEngine;

namespace AnimFlex.Sequencer.UserEnd {
	[AddComponentMenu( "AnimFlex/Sequencer" )]
	public class SequenceAnim : MonoBehaviour {
		[Tooltip( "Plays the sequence everytime the game object gets enabled or created." )] 
		[SerializeField] internal bool playOnEnable = true;

		[Tooltip("Uses a Proxy as the core of this sequence. (Useful when you need custom tick update times, i.e. unscaled time or manual time). \n " +
		         "You must assign the proxy in order for the sequence to work.\n" +
		         "HAS NOTHING TO DO IN EDITOR")]
		[SerializeField] internal bool useProxyAsCore = false;

		[Tooltip( "Whether or not to use the default core proxy. if you choose to use a default core proxy, you need to make sure " +
		          "that such core proxy exists" )]
		[SerializeField] internal bool useDefaultCoreProxy;

		[Tooltip( "The type of core proxy. You need to make sure that the selecetd default core proxy will be present at the" +
		          " time of playing." )]
		[SerializeField] internal string defaultCoreProxy;

		[Tooltip("The Proxy to use for the sequence.")]
		[SerializeField] internal AnimflexCoreProxy coreProxy;

		[Tooltip( "Whether or not to reset the sequence before re-starting it.\n" +
		          "If you decide to reset on play, the previous state of the sequencer will be terminated immediately on restart, " +
		          "and the terminating Clip will be presumed completed.\n" +
		          "However if you decide to not reset on play, the previous state of the sequencer will continue updating itself. Be warned " +
		          "that each Clip has only ONE instance, and if the *new* (restarted) sequencer trys to play a Clip that's already being played, " +
		          "the Clip's progress will be reset; the newest call to a Clip will always be most respected.+" )]
		[SerializeField]
		internal bool resetOnPlay = true;

		public Sequence sequence = new Sequence();

		private void Start() {
			if (playOnEnable) {
				PlaySequence();
			}
		}

		private void OnDisable() {
			if (sequence.IsActive())
				sequence.Stop();
		}

		private void OnValidate() {
			sequence.EditorValidate();
		}

		public void PlaySequence() {
			var proxy = useProxyAsCore
				? useDefaultCoreProxy
					? AnimFlexCoreProxyHelper.GetDefaultCoreProxy( defaultCoreProxy )
					: coreProxy
				: null;
			foreach (var node in sequence.nodes) node.clip.proxy = proxy;
			// ReSharper disable once Unity.NoNullPropagation
			sequence.sequenceController = proxy?.core.SequenceController ?? AnimflexCoreProxy.MainDefault.core.SequenceController;
			sequence.PlayOrRestart();
		}
	}
}

