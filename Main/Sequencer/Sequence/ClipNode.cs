using System;
using UnityEngine;

namespace AnimFlex.Sequencer
{
	[Flags]
	public enum ClipNodeFlags
	{
		None = 0,
		PendingStart = 1 << 0,
		Active = 1 << 1,
		PendingEnd = 1 << 2
	}

	public static class ClipNodeFlagsExtensions
	{
		public static bool HasFlagFast(this ClipNodeFlags value, ClipNodeFlags flag) => (value & flag) != 0;
	}

	[Serializable]
	public class ClipNode
	{
		[SerializeField] internal string name;
		[SerializeField] internal float delay;
		[SerializeReference] internal Clip clip;

		/// <summary>
		/// index of this clip node inside the Sequencer
		/// </summary>
		[field: NonSerialized]
		public int Index { get; private set; }

		[field: NonSerialized] public Sequence sequence { get; private set; }

		private float t = 0;
		internal ClipNodeFlags flags = ClipNodeFlags.None; // no flags on start
		private bool started = false;

		/// <summary>
		/// Marks the clip as ending
		/// </summary>
		public void End() {
			sequence.EndClip( Index );
		}

		public void StartNextClipNode(bool endSelf = true) {
			if ( !sequence.IsActive() ) return;

			if ( endSelf ) End();
			if ( sequence.nodes.Length == Index + 1 )
				sequence.Stop();
			else
				sequence.StartClip( Index + 1 );
		}

		/// <summary>
		/// on actually ending the clip node
		/// </summary>
		internal void onEnd() {
			if ( clip is Clip.IHasEnd endClip ) endClip.OnEnd();
		}

		/// <summary>
		/// initialization of the clip node. it injects variables
		/// </summary>
		/// <param name="sequence">The sequencer this ClipNode is attached to</param>
		/// <param name="index">The index of this ClipNode in the sequencer's <c>nodes</c> array</param>
		internal void Init(Sequence sequence, int index) {
			Index = index;
			this.sequence = sequence;
			clip.Init( this );
		}

		/// <summary>
		/// resets variables. used for re-playing the already played node
		/// </summary>
		internal void Reset() {
			t = 0;
			started = false;
		}

		/// <summary>
		/// Updates the ClipNode's Clip.</summary>
		/// <param name="deltaTime">The time interval from the last Tick to now</param>
		internal void Tick(float deltaTime) {
			t += deltaTime;
			if ( t > delay ) {
				// start of the clip
				if ( !started ) {
					started = true; // first set this to true, so if clip.Play() threw errors,
					// they won't get executed in the next Tick
					clip.Start();
				}
				// update/tick of the clip
				else {
					if ( clip is Clip.IHasTick tickClip ) {
						tickClip.Tick( deltaTime );
						Debug.Log( $"tick {Index}" );
					}
				}
			}
		}

		internal void OnValidate() {
			clip.OnValidate();
		}
	}
}