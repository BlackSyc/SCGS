namespace Syc.Core.Attributes
{
	/// <summary>
	/// Base class for attribute modifications.
	/// An instance of this class should be part of a chain.
	/// The instance will remove itself from the chain upon calling <see cref="DestroyModification"/>.
	/// </summary>
	public abstract class AttributeModification : IModificationHandle
	{
		private AttributeModification _child;
		private AttributeModification _parent;

		protected abstract float Modify(float value);
		
		public float FeedThrough(float value)
		{
			return _child?.FeedThrough(Modify(value)) ?? Modify(value);
		}
		
		public void Put(AttributeModification newAttributeModification)
		{
			if (_child == default)
			{
				_child = newAttributeModification;
				_child._parent = this;
				return;
			}
			
			_child.Put(newAttributeModification);
		}

		public bool IsDestroyed => _parent == default;

		public void DestroyModification()
		{
			if(_parent != default)
				_parent._child = _child;
			
			if(_child != default)
				_child._parent = _parent;
		}
	}
}