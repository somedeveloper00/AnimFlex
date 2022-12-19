using UnityEngine;

namespace AnimFlex.Tweening
{
	internal static class MonobehaviourExtensions
	{
		/// <summary>
		/// Like GetComponentInChildren, but only returns the first component found.
		/// </summary>
		public static bool TryGetComponentInChildrenOnly<T>(this Component mb, out T result) {
			foreach ( Transform child in mb.transform ) {
				if ( child.TryGetComponent<T>( out result ) ) {
					return true;
				}
			}
			// look deeper
			foreach ( Transform child in mb.transform ) {
				if ( child.TryGetComponentInChildrenOnly<T>( out result ) )
					return true;
			}
			

			result = default;
			return false;
		}
	}
}