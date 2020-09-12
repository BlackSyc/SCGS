namespace Syc.Core.Attributes
{
	/// <summary>
	/// An instance of this class represents an attribute modification by addition.
	/// </summary>
	internal class AttributeAddition : AttributeModification
	{
		/// <summary>
		/// The amount that will be added to modify the value.
		/// </summary>
		public float Addition;

		/// <summary>
		/// Will add <see cref="Addition"/> to the value and return it.
		/// </summary>
		/// <param name="value">The value that will be added to.</param>
		/// <returns>The new value.</returns>
		protected override float Modify(float value) => value + Addition;

		/// <summary>
		/// Creates a new instance of <see cref="AttributeAddition"/>.
		/// </summary>
		/// <param name="addition">The amount that will be added to the passed value in <see cref="Modify"/>.</param>
		public AttributeAddition(float addition) => Addition = addition;
	}
}