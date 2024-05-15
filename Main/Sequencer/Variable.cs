using System;
using UnityEngine;

namespace AnimFlex.Sequencer
{
	/// <summary>
	/// A base variable for <see cref="Sequence"/>. This is merely here so that Unity can 
	/// serialize a list of <see cref="Variable{T}"/>s with <see cref="SerializeReference"/> attribute. 
	/// In all cases, you should just cast this to an equivelant <see cref="Variable{T}"/> based on <see cref="Type"/>
	/// </summary>
	[Serializable]
	public abstract class Variable
	{
#if true
		[SerializeField] public string name;
#endif
		/// <summary>
		/// The type of variable. This will indicate what type this variable is of.
		/// </summary>
		public abstract Type Type { get; }
	}

	/// <summary>
	/// A variable for <see cref="Sequence"/>
	/// </summary>
	[Serializable]
	public abstract class Variable<T> : Variable
	{
		[SerializeField] public T Value;

		public override Type Type => typeof(T);
	}
}