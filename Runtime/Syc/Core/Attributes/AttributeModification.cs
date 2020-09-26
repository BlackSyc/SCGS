using UnityEngine;

namespace Syc.Core.Attributes
{
	/// <summary>
	/// Base class for attribute modifications.
	/// An instance of this class should be part of a chain.
	/// The instance will remove itself from the chain upon calling <see cref="DestroyModification"/>.
	/// </summary>
	public abstract class AttributeModification
	{
		public object ReferenceObject { get; }

		protected AttributeModification(object referenceObject)
		{
			ReferenceObject = referenceObject;
		}

		protected abstract float Modify(float value);
	}
}