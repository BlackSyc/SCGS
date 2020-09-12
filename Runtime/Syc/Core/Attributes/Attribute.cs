using System;
using UnityEngine;

namespace Syc.Core.Attributes
{
	/// <summary>
	/// An instance of this class can represent a characters attribute such as Stamina, Haste or Armor.
	/// </summary>
	[Serializable]
	public class Attribute
	{
		#region Properties

		/// <summary>
		/// Current value of this attribute where the addition modifications are applied to the base value first,
		/// followed by the multiplication modifications.
		/// </summary>
		public float CurrentValue => _multiplierChain.FeedThrough(_additionChain.FeedThrough(baseValue));

		/// <summary>
		/// Gets the base value of this attribute.
		/// </summary>
		public float BaseValue => baseValue;

		#endregion

		#region Serialized Fields

		/// <summary>
		/// The starting value of this attribute.
		/// </summary>
		[SerializeField]
		private float baseValue = 10f;

		/// <summary>
		/// The multiplication used to calculate the attributes <see cref="Remap()"/>.
		/// Default value is 1.
		/// </summary>
		[SerializeField]
		private float remapMultiplier = 1f;

		/// <summary>
		/// The exponent used to calculate the attributes <see cref="Remap()"/>.
		/// Default value is 1.
		/// </summary>
		[SerializeField] 
		private float remapExponent = 1f;

		/// <summary>
		/// The addition used to calculate the attributes <see cref="Remap()"/>
		/// Default value is 0.
		/// </summary>
		[SerializeField] 
		private float remapAddition;
		
		#endregion
		
		#region Private Fields
		
		/// <summary>
		/// The root element of the multiplication chain that is used to calculate the 
		/// <see cref="CurrentValue"/> by feeding the <see cref="BaseValue"/> through the chain.
		/// </summary>
		private AttributeMultiplier _multiplierChain = new AttributeMultiplier(1);
		
		/// <summary>
		/// The root element of the addition chain that is used to calculate the 
		/// <see cref="CurrentValue"/> by feeding the <see cref="BaseValue"/> through the chain.
		/// </summary>
		private AttributeAddition _additionChain = new AttributeAddition(0);
		
		#endregion
		
		#region Public Methods

		/// <summary>
		/// Add a specified amount to this attributes <see cref="CurrentValue"/>.
		/// Returns a handle to the created addition as an out parameter so that it can be removed by calling
		/// <see cref="IModificationHandle.DestroyModification"/>.
		/// </summary>
		/// <param name="addition">The amount that will be added to the attributes <see cref="BaseValue"/>.
		/// in the <see cref="CurrentValue"/> calculation.</param>
		/// <param name="modificationHandle">A handle to the created addition so that it can be removed by calling
		/// <see cref="IModificationHandle.DestroyModification"/>.</param>
		/// <returns>A handle to the new addition.</returns>
		public void Add(float addition, out IModificationHandle modificationHandle)
		{
			var attributeAddition = new AttributeAddition(addition);
			_additionChain.Put(attributeAddition);
			modificationHandle = attributeAddition;
		}

		/// <summary>
		/// Multiplies a specified amount with this attributes <see cref="BaseValue"/>.
		/// Returns a handle to the created multiplier as an out parameter so that it can be removed by calling
		/// <see cref="IModificationHandle.DestroyModification"/>.
		/// </summary>
		/// <param name="multiplier">The amount that will be multiplied with the attributes <see cref="BaseValue"/>.
		/// in the <see cref="CurrentValue"/> calculation.</param>
		/// <param name="modificationHandle">A handle to the created multiplier so that it can be removed by calling
		/// <see cref="IModificationHandle.DestroyModification"/>.</param>
		/// <returns>A handle to the new multiplier.</returns>
		public void Multiply(float multiplier, out IModificationHandle modificationHandle)
		{
			var attributeMultiplier = new AttributeMultiplier(multiplier);
			_multiplierChain.Put(attributeMultiplier);
			modificationHandle = attributeMultiplier;
		}

		public float Remap() => (float) (remapMultiplier * Math.Pow(CurrentValue, remapExponent)) + remapAddition;

		public float Remap(Func<float, float> remappingFunction) => remappingFunction.Invoke(CurrentValue);
		
		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of <see cref="Attribute"/> with specified remap calculation components.
		/// </summary>
		/// <param name="baseValue">The base value of the newly created attribute.</param>
		/// <param name="remapMultiplier">The multiplication used to calculate the attributes <see cref="Remap()"/>.</param>
		/// <param name="remapExponent">The exponent used to calculate the attributes <see cref="Remap()"/>.</param>
		/// <param name="remapAddition">The addition u sed to calculate the attributes <see cref="Remap()"/>.</param>
		public Attribute(float baseValue = 10f, float remapMultiplier = 1f, float remapExponent = 1f, float remapAddition = 0f)
		{
			this.baseValue = baseValue;
			this.remapMultiplier = remapMultiplier;
			this.remapExponent = remapExponent;
			this.remapAddition = remapAddition;
		}

		private Attribute() { }

		#endregion
	}
}