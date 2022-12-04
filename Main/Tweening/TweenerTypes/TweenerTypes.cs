using System;
using AnimFlex.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnimFlex.Tweening
{
	internal class TweenerFloat : Tweener<float>
	{
		internal override void Set(float t) => setter( startValue + (endValue - startValue) * t );
	}

	internal class TweenerInt : Tweener<int>
	{
		internal override void Set(float t) => setter( Mathf.RoundToInt( startValue + (endValue - startValue) * t ) );
	}

	internal class TweenerVector2 : Tweener<Vector2>
	{
		internal override void Set(float t) => setter( startValue + (endValue - startValue) * t );
	}


	internal class TweenerVector3 : Tweener<Vector3>
	{
		internal override void Set(float t) => setter( Vector3.LerpUnclamped( startValue, endValue, t ) );
	}

	internal class TweenerRect : Tweener<Rect>
	{
		internal override void Set(float t) =>
			setter( new Rect(
				startValue.min + (endValue.min - startValue.min) * t,
				startValue.max + (endValue.max - startValue.max) * t ) );
	}

	internal class TweenerQuaternion : Tweener<Quaternion>
	{
		internal override void Set(float t) => setter( Quaternion.LerpUnclamped( startValue, endValue, t ) );
	}

	internal class TweenerColor : Tweener<Color>
	{
		internal override void Set(float t) => setter( Color.LerpUnclamped( startValue, endValue, t ) );
	}

	internal class TweenerString : Tweener<string>
	{
		private static System.Random rand = new ( -1234567891 );
		
		internal override void Set(float t) {
			if ( t < 0 ) {
				rand = new(-1234567891);
				char[] s = new char[-Mathf.CeilToInt(t * startValue.Length)];
				for (int i = 0; i < s.Length; i++) 
					s[i] = startValue[rand.Next( startValue.Length )];
				setter( new string(s) + startValue );
			}
			else if ( t > 1 ) {
				rand = new(-1234567891);
				char[] s = new char[Mathf.FloorToInt( (t-1) * endValue.Length)];
				for (int i = 0; i < s.Length; i++) 
					s[i] = endValue[rand.Next( endValue.Length )];
				setter( endValue + new string(s) );
			}
			else if ( startValue.Length > endValue.Length ) {
				int ind = Mathf.FloorToInt(startValue.Length * t);
				setter( endValue.Substring( 0, Mathf.Max( 0, ind - (startValue.Length - endValue.Length) ) ) +
				        startValue.Substring( ind ) );
			}
			else {
				int ind = Mathf.FloorToInt(endValue.Length * t);
				var v = Mathf.Max( 0, ind - (endValue.Length - startValue.Length) );
				setter( endValue.Substring( 0, ind ) +
				        startValue.Substring( v, startValue.Length - v ));
					
			}
		}
	}
}