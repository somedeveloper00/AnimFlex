using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Sequencer
{
	[Flags]
	internal enum SequenceFlags
	{
		Active = 1 << 1,
		Paused = 1 << 2,
		Stopping = 1 << 3
	}

	internal static class SequenceFlagsExtensions
	{
		public static bool HasFlagFast(this SequenceFlags value, SequenceFlags flag) => (value & flag) != 0;
	}

	[Serializable]
	public sealed partial class Sequence
	{
		/// <summary>
		/// executes when the sequence is played.
		/// </summary>
		public event Action onPlay = delegate { };

		/// <summary>
		/// executes when the sequence is completed or stopped
		/// </summary>
		public event Action onComplete = delegate { };

		[SerializeField] internal ClipNode[] nodes = Array.Empty<ClipNode>();

		internal SequenceFlags flags;


#region Public playback tools

		/// <summary>
		/// pauses the sequencer.
		/// </summary>
		public void Pause() => flags |= SequenceFlags.Paused;

		/// <summary>
		/// resumes the sequencer if it was paused.
		/// </summary>
		public void Resume() => flags &= ~SequenceFlags.Paused;

		/// <summary>
		/// stops the sequencer. note that the stopping process will not happen right away.
		/// </summary>
		public void Stop() {
			if ( !IsActive() ) {
				Debug.LogWarning( "Sequencer is not active, so there's nothing to stop." );
				return;
			}

			flags |= SequenceFlags.Stopping;
		}

		/// <summary>
		/// plays the sequence. if you're unsure if the sequencer is already active or not,
		/// call <see cref="PlayOrRestart"/> instead.
		/// </summary>
		public void Play() {
			if ( IsActive() ) {
				Debug.LogError(
					$"The sequence is already active. You cannot play an active sequencer. You can call {nameof(PlayOrRestart)} instead." );
			}

			if ( nodes.Length <= 0 ) return;
			SequenceController.Instance.AddNewSequence( this );
		}

		/// <summary>
		/// If the sequence is already active, this will restart it, or if it's not activated, it'll call <see cref="Play"/>
		/// automatically. <para/>
		/// Note that this will act a little bit slower than <see cref="Play"/> would
		/// </summary>
		public void PlayOrRestart() {
			if ( IsActive() == false ) // play
			{
				Play();
			}
			else // restart
			{
				Stop();
				SequenceController.delayedCall += Play;
			}
		}

#endregion


#region Internals

		internal void OnActivate() {
			for ( int i = 0; i < nodes.Length; i++ ) nodes[i].Init( this, i );
			StartClip( 0 );
			onPlay();
		}

		internal void OnStop() {
			flags = 0; // empty flags
			onComplete();
		}

		internal void Tick(float deltaTime) {
			// start phase
			for ( int i = 0; i < nodes.Length; i++ ) {
				if ( nodes[i].flags.HasFlagFast( ClipNodeFlags.PendingStart ) ) {
					nodes[i].Reset();
					nodes[i].flags = ClipNodeFlags.Active;
				}
			}

			// tick phase
			for ( int i = 0; i < nodes.Length; i++ ) {
				if ( nodes[i].flags.HasFlagFast( ClipNodeFlags.Active ) ) {
					nodes[i].Tick( deltaTime );
				}
			}

			// end phase
			for ( int i = 0; i < nodes.Length; i++ ) {
				if ( nodes[i].flags.HasFlagFast( ClipNodeFlags.PendingEnd ) ) {
					nodes[i].flags = ClipNodeFlags.None;
					nodes[i].onEnd();
				}
			}
		}

		internal void StartClip(int index) {
			if ( nodes[index].flags.HasFlagFast( ClipNodeFlags.Active ) ) {
				// hard end
				nodes[index].onEnd();
			}

			nodes[index].flags = ClipNodeFlags.PendingStart;
		}

		internal void EndClip(int index) {
			if ( nodes[index].flags.HasFlagFast( ClipNodeFlags.Active ) ||
			     nodes[index].flags.HasFlagFast( ClipNodeFlags.PendingStart ) ) {
				nodes[index].flags = ClipNodeFlags.PendingEnd;
			}
		}

		internal bool IsActive() => flags.HasFlagFast( SequenceFlags.Active );

		internal void EditorValidate() {
			foreach ( var node in nodes ) node.OnValidate();
		}

#endregion

#region Clip manipulations

		public void RemoveClipNodeAtIndex(int index) {
			var nodesList = nodes.ToList();
			nodesList.RemoveAt( index );
			nodes = nodesList.ToArray();
		}

		public void RemoveClipNode(ClipNode node) {
			RemoveClipNodeAtIndex( Array.IndexOf( nodes, node ) );
		}

		public void MoveClipNode(int fromIndex, int toIndex) {
			(nodes[fromIndex], nodes[toIndex]) = (nodes[toIndex], nodes[fromIndex]);
		}

		public void AddNewClipNode(Clip clip) {
			var tmp = nodes.ToList();
			tmp.Add( new ClipNode {
				clip = clip,
				name = $"Node {nodes.Length}"
			} );
			nodes = tmp.ToArray();
		}

		public void InsertNewClipAt(Clip clip, int index) {
			var tmp = nodes.ToList();
			tmp.Insert( index, new ClipNode {
				clip = clip,
			} );
			nodes = tmp.ToArray();
		}

#endregion
	}
}