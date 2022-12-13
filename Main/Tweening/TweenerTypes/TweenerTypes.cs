using System;
using AnimFlex.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnimFlex.Tweening
{
	internal class TweenerFloat : Tweener<float>
	{
		protected override float Evaluate(float t) => startValue + (endValue - startValue) * t;
	}

	internal class TweenerInt : Tweener<int>
	{
		protected override int Evaluate(float t) => Mathf.RoundToInt( startValue + (endValue - startValue) * t );
	}

	internal class TweenerVector2 : Tweener<Vector2>
	{
		protected override Vector2 Evaluate(float t) => startValue + (endValue - startValue) * t;
	}


	internal class TweenerVector3 : Tweener<Vector3>
	{
		protected override Vector3 Evaluate(float t) => Vector3.LerpUnclamped( startValue, endValue, t );
	}

	internal class TweenerRect : Tweener<Rect>
	{
		protected override Rect Evaluate(float t) => new Rect(
			startValue.min + (endValue.min - startValue.min) * t,
			startValue.max + (endValue.max - startValue.max) * t );
	}

	internal class TweenerQuaternion : Tweener<Quaternion>
	{
		protected override Quaternion Evaluate(float t) => Quaternion.LerpUnclamped( startValue, endValue, t );
	}

	internal class TweenerColor : Tweener<Color>
	{
		protected override Color Evaluate(float t) => Color.LerpUnclamped( startValue, endValue, t );
	}

	internal class TweenerString : Tweener<string>
	{
		private static System.Random rand = new System.Random( -1234567891 );

		protected override string Evaluate(float t) {
			if ( t < 0 ) {
				rand = new System.Random( -1234567891 );
				char[] s = new char[-Mathf.CeilToInt( t * startValue.Length )];
				for ( int i = 0; i < s.Length; i++ )
					s[i] = startValue[rand.Next( startValue.Length )];
				return new string( s ) + startValue;
			}
			else if ( t > 1 ) {
				rand = new System.Random( -1234567891 );
				char[] s = new char[Mathf.FloorToInt( (t - 1) * endValue.Length )];
				for ( int i = 0; i < s.Length; i++ )
					s[i] = endValue[rand.Next( endValue.Length )];
				return endValue + new string( s );
			}
			else if ( startValue.Length > endValue.Length ) {
				int ind = Mathf.FloorToInt( startValue.Length * t );
				return endValue.Substring( 0, Mathf.Max( 0, ind - (startValue.Length - endValue.Length) ) ) +
				       startValue.Substring( ind );
			}
			else {
				int ind = Mathf.FloorToInt( endValue.Length * t );
				var v = Mathf.Max( 0, ind - (endValue.Length - startValue.Length) );
				return endValue.Substring( 0, ind ) +
				       startValue.Substring( v, startValue.Length - v );
			}
		}
	}
}