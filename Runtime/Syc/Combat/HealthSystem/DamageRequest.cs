using System;
using UnityEngine;

namespace Syc.Combat.HealthSystem
{
	public class DamageRequest
	{
		public float BaseAmount { get; }
		
		public bool IsCriticalStrike { get; }
		
		public GameObject Cause { get; }
		
		public float CalculatedAmount { get; private set; }
		
		public float AmountDealt { get; set; }
		
		public Func<ICombatAttributes, float, float> DamageCalculator { get; }

		public DamageRequest(float baseAmount, bool isCriticalStrike, GameObject cause, Func<ICombatAttributes, float, float> damageCalculator)
		{
			BaseAmount = baseAmount;
			IsCriticalStrike = isCriticalStrike;
			Cause = cause;
			DamageCalculator = damageCalculator;
		}

		public float CalculateAmount(ICombatAttributes attributeSystem)
		{
			CalculatedAmount = DamageCalculator.Invoke(attributeSystem, BaseAmount);
			return CalculatedAmount;
		}

		public static Func<ICombatAttributes, float, float> WithMitigation =>
			(attributes, baseAmount) => 
				attributes?.Armor != null ? attributes.Armor.Remap() * baseAmount : baseAmount; 

		public static Func<ICombatAttributes, float, float> NoMitigation =>
			(attributes, baseAmount) => baseAmount;
	}
}