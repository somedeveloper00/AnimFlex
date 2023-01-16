﻿using System;
using UnityEngine;

namespace AnimFlex.Tweening
{
	[Flags]
	internal enum TweenerFlag
	{
		/// <summary>
		/// marked for deletion
		/// </summary>
		Deleting = 1 << 0,

		/// <summary>
		/// marked created so later on it can't be re-used
		/// </summary>
		Created = 1 << 1,

		/// <summary>
		/// whetehr or not the Init function has been called
		/// </summary>
		Initialized = 1 << 2,

		/// <summary>
		/// forces the tweener to not call to onComplete while completed
		/// </summary>
		ForceNoOnComplete = 1 << 3,
	}

	public abstract partial class Tweener
	{
		/// <summary>
		/// Checks whether or not the <see cref="Tweener"/> is valid. (e.g. could check if <see cref="GameObject"/> is <c>enabled</c>
		/// </summary>
		internal Func<bool> isValid;

		/// <summary>
		/// per-tween T value for evaluation
		/// </summary>
		internal float _t = 0;

		/// <summary>
		/// the flag representing the status of this tween. used mostly by TweenController
		/// </summary>
		internal TweenerFlag flag;

		/// <summary>
		/// the duration of which this tween will take to finish. this does NOT include the delay time
		/// </summary>
		internal float duration;

		/// <summary>
		/// the initial delay after the Initialization.
		/// during delay time, the tween will keep using setter to set to startValue
		/// </summary>
		internal float delay;

		/// <summary>
		/// the type of ease function
		/// </summary>
		internal Ease ease;

		/// <summary>
		/// custom curve used for special cases where user wants to use their specific curve as the Ease
		/// </summary>
		internal AnimationCurve customCurve;

		/// <summary>
		/// if true, the tweener will have a ping pong behaviour. note that the duration will stay the same
		/// </summary>
		internal bool pingPong = false;

		/// <summary>
		/// indicates the number of times the tweener will have to loop until it's completed.
		/// </summary>
		internal int loops = 0;

		/// <summary>
		/// the delay in between each loop
		/// </summary>
		internal float loopDelay = 0;

		/// <summary>
		/// indicates whether or not to swap the start and end value during initialization (next frame of a Play call)
		/// </summary>
		internal bool from = false;

		internal bool useCurve = false;
		
		/// <summary>
		/// resets the tweener to its initial state after it's completed
		/// </summary>
		internal bool resetOnComplete = false;

		internal abstract void Init();
		internal abstract void Set(float t);


		internal Tweener() => TweenerController.Instance.AddTweener( this );

#region events

		/// <summary>
		/// called on Init. the init happens as the first phase of each AnimFlex's Tick.
		/// </summary>
		public event Action onStart = delegate { };

		/// <summary>
		/// called on each AnimFlex's Tick
		/// </summary>
		public event Action onUpdate = delegate { };

		/// <summary>
		/// called on the final setter call when it reaches the endValue. will be called after onUpdate
		/// </summary>
		public event Action onComplete = delegate { };

		/// <summary>
		/// called on the last phase of AnimFlex's Tick. mostly at the same frame as onComplete, unless the tween is killed manually or by error
		/// </summary>
		public event Action onKill = delegate { };

		internal void OnStart() => onStart();
		internal void OnUpdate() => onUpdate();
		internal void OnComplete() => onComplete();

		internal void OnKill() => onKill();

#endregion

#region helpers

		/// <summary>
		/// Kills the tweener in the next phase of AnimFlex's Tick
		/// </summary>
		public void Kill(bool complete, bool onKillCallback) =>
			TweenerController.Instance.KillTweener( this, complete, onKillCallback );

		/// <summary>
		/// <para> checks if the tweener has valid setter/getters through the validator function <see cref="isValid"/></para>
		/// <para> if you want to casually check if the tweener is active, you can do null check</para>
		/// </summary>
		public bool IsValid() => !flag.HasFlag( TweenerFlag.Deleting ) && (isValid is null || isValid());

#endregion


#region operators

		public static bool operator ==(Tweener a, object b) {
			if ( a is null ) {
				if ( b is null ) return true;
				if ( b is Tweener tweener ) {
					return !tweener.IsValid();
				}
			}

			if ( b is null ) {
				if ( a is null ) return true;
				if ( a is Tweener tweener ) {
					return !tweener.IsValid();
				}
			}

			return ReferenceEquals( a, b );
		}

		public static bool operator !=(Tweener a, object b) => !(a == b);

#endregion
	}

	public abstract class Tweener<T> : Tweener
	{
		internal T startValue, endValue;
		internal Action<T> setter;
		internal Func<T> getter;

		internal override void Set(float t) {
			if ( t == 0 ) setter( startValue );
			else if (t == 1 ) setter( endValue );
			else setter( Evaluate( t ) );
		}

		protected abstract T Evaluate(float f);


		internal override void Init() {
			startValue = getter();
			if ( @from )
				(startValue, endValue) = (endValue, startValue);
		}
	}
}