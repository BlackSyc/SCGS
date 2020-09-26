namespace Syc.Core.Attributes
{
	/// <summary>
	/// An instance of this class represents an attribute modification by multiplication.
	/// </summary>
	public class AttributeMultiplier : AttributeModification
	{
		/// <summary>
		/// The amount that will be multiplied with to modify the value.
		/// </summary>
		public float Multiplier;

		/// <summary>
		/// Will multiply <see cref="Multiplier"/> with the value and return it.
		/// </summary>
		/// <param name="value">The value that will be multiplied.</param>
		/// <returns>The new value.</returns>
		protected override float Modify(float value) => value * Multiplier;

		/// <summary>
		/// Creates a new instance of <see cref="AttributeMultiplier"/>.
		/// </summary>
		/// <param name="multiplier">The amount that will be multiplied with the passed value in <see cref="Modify"/>.</param>
		/// <param name="referenceObject"></param>
		public AttributeMultiplier(float multiplier, object referenceObject) : base(referenceObject) => Multiplier = multiplier;
	}
}