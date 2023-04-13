using System;
using UnityEngine;
using UnityEngine.Events;

namespace AnimFlex.Tweening
{
	public abstract class TweenerComponent : MonoBehaviour
	{
		public abstract Ease ease { get; set; }
		public abstract float duration { get; set; }
		public abstract float delay { get; set; }
		public abstract UnityEvent onStart { get; }
		public abstract UnityEvent onComplete { get; }
		public abstract UnityEvent onKill { get; }
		public abstract UnityEvent onUpdate { get; }
		public abstract bool TryGetTweener(out Tweener tweener);
		public abstract void PlayOrRestart();
		public abstract void Kill(bool complete = true, bool onCompleteCallback = true);
	}

	public abstract class TweenerComponent<T> : TweenerComponent where T : TweenerGenerator, new()
	{
		public T generator = new T();

		[Tooltip( "If checked, it'll play right when the game object activates" )] 
		[SerializeField] internal bool playOnStart;

		private void Reset() {
			if ( generator == null ) generator = new T();
			generator.Reset( gameObject );
		}

		private Tweener m_tweener;


#region wrappers

		public override Ease ease {
			get => generator.ease;
			set => generator.ease = value;
		}

		public override float duration {
			get => generator.duration;
			set => generator.duration = value;
		}

		public override float delay {
			get => generator.delay;
			set => generator.delay = value;
		}

		public override UnityEvent onStart => generator.onStart;

		public TweenerComponent<T> OnStart(Action onStart) {
			generator.onStart.AddListener( onStart.Invoke );
			return this;
		}

		public override UnityEvent onComplete => generator.onComplete;

		public TweenerComponent<T> OnComplete(Action onComplete) {
			generator.onComplete.AddListener( onComplete.Invoke );
			return this;
		}

		public override UnityEvent onKill => generator.onKill;

		public TweenerComponent<T> OnKill(Action onKill) {
			generator.onKill.AddListener( onKill.Invoke );
			return this;
		}

		public override UnityEvent onUpdate => generator.onUpdate;

		public TweenerComponent<T> OnUpdate(Action onUpdate) {
			generator.onUpdate.AddListener( onUpdate.Invoke );
			return this;
		}

#endregion

		/// <summary>
		/// returns the last generated Tweener, if it's active.
		/// </summary>
		public override bool TryGetTweener(out Tweener tweener) {
			if ( m_tweener != null &&
			     !m_tweener.flag.HasFlag( TweenerFlag.Deleting ) ) {
				tweener = m_tweener;
				return true;
			}

			tweener = null;
			return false;
		}

		private void Start() {
			if ( playOnStart ) {
				PlayOrRestart();
			}
		}


		/// <summary>
		/// generates the tweener and plays it if it's not playing already. otherwise generates a new tweener and plays it.
		/// </summary>
		public override void PlayOrRestart() {
			// kill if already running
			if ( m_tweener != null && !m_tweener.flag.HasFlag( TweenerFlag.Deleting ) )
				Kill( false, false );

			// generate new tweener if possible
			if ( generator.TryGenerateTween( out m_tweener ) ) {
				if ( m_tweener == null ) {
					Debug.LogError( $"Unexpected Error happened while generating tweener!" );
				}
			}
		}

		/// <summary>
		/// kills the tweener right away
		/// </summary>
		public override void Kill(bool complete = true, bool onCompleteCallback = true) {
			TweenerController.Instance.KillTweener( m_tweener, complete, onCompleteCallback );
		}
	}
}