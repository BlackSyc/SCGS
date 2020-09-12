namespace Syc.Core.Attributes
{
	/// <summary>
	/// An instance of this class represents a modification in an attributes modification chain.
	/// It can be used to remove it from the chain.
	/// </summary>
	public interface IModificationHandle
	{
		/// <summary>
		/// Returns whether the modification has been removed from the chain.
		/// </summary>
		bool IsDestroyed { get; }
		
		/// <summary>
		/// Destroys the modification, removing it from the chain.
		/// </summary>
		void DestroyModification();
	}
}