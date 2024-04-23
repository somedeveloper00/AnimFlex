using System;
using System.Threading.Tasks;
using AnimFlex.Core.Proxy;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimFlex.Sequencer
{
	[AddComponentMenu("AnimFlex/Sequencer")]
	public class SequenceAnim : MonoBehaviour
	{

		[FormerlySerializedAs("playOnEnable")]
		[Tooltip("Plays the sequence everytime the game object gets enabled or created.")]
		[SerializeField] internal bool playOnStart = true;

		[Tooltip("If enabled, the sequencer will start playing without waiting for the queue first.\n" +
				 "this ensures that the sequencer plays in the same frame as it's called. Since it'll not wait for the phase," +
				 "you need to be careful to play the sequence from main thread and preferably from a Unity's phase")]
		[SerializeField] internal bool dontWaitInQueueToPlay = true;

		[Tooltip("Uses a Proxy as the core of this sequence. (Useful when you need custom tick update times, i.e. unscaled time or manual time). \n " +
				 "You must assign the proxy in order for the sequence to work.\n" +
				 "HAS NOTHING TO DO IN EDITOR")]
		[SerializeField] internal bool useProxyAsCore = false;

		[Tooltip("Whether or not to use the default core proxy. if you choose to use a default core proxy, you need to make sure " +
				  "that such core proxy exists")]
		[SerializeField] internal bool useDefaultCoreProxy;

		[Tooltip("The type of core proxy. You need to make sure that the selecetd default core proxy will be present at the" +
				  " time of playing.")]
		[SerializeField] internal string defaultCoreProxy;

		[Tooltip("The Proxy to use for the sequence.")]
		[SerializeField] internal AnimflexCoreProxy coreProxy;


		[Tooltip("Whether or not to reset the sequence before re-starting it.\n" +
				  "If you decide to reset on play, the previous state of the sequencer will be terminated immediately on restart, " +
				  "and the terminating Clip will be presumed completed.\n" +
				  "However if you decide to not reset on play, the previous state of the sequencer will continue updating itself. Be warned " +
				  "that each Clip has only ONE instance, and if the *new* (restarted) sequencer trys to play a Clip that's already being played, " +
				  "the Clip's progress will be reset; the newest call to a Clip will always be most respected.+")]
		[SerializeField]
		internal bool resetOnPlay = true;

		[Tooltip("when true, it won't wait for the next Tick (frame) to activate the next clip.")]
		[SerializeField] internal bool activateNextClipsASAP = true;


		public Sequence sequence = new Sequence();

		internal event Action beforePlay;

		private void Start()
		{
			if (playOnStart) PlaySequence();
		}

		private void OnDisable()
		{
			if (sequence.IsActive())
				sequence.Stop();
		}

		private void OnValidate() => sequence.EditorValidate(this);

		public Task AwaitComplete() => sequence.AwaitComplete();

		public void PlaySequence()
		{
			beforePlay?.Invoke();
#if UNITY_EDITOR
			var proxy = Application.isPlaying && useProxyAsCore
				? useDefaultCoreProxy
					? AnimFlexCoreProxyHelper.GetDefaultCoreProxy(defaultCoreProxy)
					: coreProxy
				: null;
#else
			var proxy = useProxyAsCore
				? useDefaultCoreProxy
					? AnimFlexCoreProxyHelper.GetDefaultCoreProxy( defaultCoreProxy )
					: coreProxy
				: null;
#endif
			foreach (var node in sequence.nodes) node.clip.proxy = proxy;
			// ReSharper disable once Unity.NoNullPropagation
			sequence.sequenceController = (proxy ? proxy : AnimflexCoreProxy.MainDefault).core.SequenceController;
			sequence.activateNextClipsASAP = activateNextClipsASAP;
			sequence.PlayOrRestart(dontWaitInQueueToPlay);
		}

		public void StopSequence() => sequence.Stop();

		public bool IsPlaying() => sequence.IsPlaying();
	}
}

