using System;
using System.Collections.Generic;
using System.Linq;
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
		public float CurrentValue
		{
			get
			{
				var currentValue = baseValue + _additionChain.Sum(addition => addition.Addition);

				if (_multiplierChain.Any())
				{
					foreach (var multiplier in _multiplierChain)
					{
						currentValue *= multiplier.Multiplier;
					}
				}
				
				return currentValue;
			}
		}

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
		[SerializeField] private float horizontalOffset;

		[SerializeField] private float verticalOffset;
		
		#endregion
		
		#region Private Fields
		
		/// <summary>
		/// The root element of the multiplication chain that is used to calculate the 
		/// <see cref="CurrentValue"/> by feeding the <see cref="BaseValue"/> through the chain.
		/// </summary>
		private List<AttributeMultiplier> _multiplierChain = new List<AttributeMultiplier>();
		
		/// <summary>
		/// The root element of the addition chain that is used to calculate the 
		/// <see cref="CurrentValue"/> by feeding the <see cref="BaseValue"/> through the chain.
		/// </summary>
		private List<AttributeAddition> _additionChain = new List<AttributeAddition>();
		
		#endregion
		
		#region Public Methods

		public void Add(float addition, object referenceObject)
		{
			var activeAddition = _additionChain.FirstOrDefault(x => x.ReferenceObject == referenceObject);
			if (activeAddition != null)
			{
				activeAddition.Addition += addition;
				return;
			}
			
			var attributeAddition = new AttributeAddition(addition, referenceObject);
			_additionChain.Add(attributeAddition);
		}

		public void RemoveAddition(object referenceObject)
		{
			_additionChain.RemoveAll(x => x.ReferenceObject == referenceObject);
		}

		public void Multiply(float multiplier, object referenceObject)
		{
			var activeMultiplication = _multiplierChain.FirstOrDefault(x => x.ReferenceObject == referenceObject);
			if (activeMultiplication != null)
			{
				activeMultiplication.Multiplier *= multiplier;
				return;
			}
			
			var attributeMultiplication = new AttributeMultiplier(multiplier, referenceObject);
			_multiplierChain.Add(attributeMultiplication);
		}

		public void RemoveMultiplier(object referenceObject)
		{
			_multiplierChain.RemoveAll(x => x.ReferenceObject == referenceObject);
		}

		public float Remap() => (float) (remapMultiplier * Math.Pow(CurrentValue + horizontalOffset, remapExponent)) + verticalOffset;

		public float Remap(Func<float, float> remappingFunction) => remappingFunction.Invoke(CurrentValue);
		
		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of <see cref="Attribute"/> with specified remap calculation components.
		/// </summary>
		/// <param name="baseValue">The base value of the newly created attribute.</param>
		/// <param name="remapMultiplier">The multiplication used to calculate the attributes <see cref="Remap()"/>.</param>
		/// <param name="remapExponent">The exponent used to calculate the attributes <see cref="Remap()"/>.</param>
		/// <param name="horizontalOffset">The addition u sed to calculate the attributes <see cref="Remap()"/>.</param>
		/// <param name="verticalOffset"></param>
		public Attribute(float baseValue = 10f, float remapMultiplier = 1f, float remapExponent = 1f, float horizontalOffset = 0f, float verticalOffset = 0f)
		{
			this.baseValue = baseValue;
			this.remapMultiplier = remapMultiplier;
			this.remapExponent = remapExponent;
			this.horizontalOffset = horizontalOffset;
			this.verticalOffset = verticalOffset;
		}

		private Attribute() { }

		#endregion
	}
}