using System;
using UnityEngine;

namespace Syc.Combat.HealthSystem
{
	public class HealRequest
	{
		public float BaseAmount { get; }
		
		public GameObject Cause { get; }
		
		public float CalculatedAmount { get; private set; }
		
		public float AmountDealt { get; set; }
		
		public Func<ICombatAttributes, float, float> HealCalculator { get; }

		public HealRequest(float baseAmount, GameObject cause, Func<ICombatAttributes, float, float> healCalculator)
		{
			BaseAmount = baseAmount;
			Cause = cause;
			HealCalculator = healCalculator;
		}

		public HealRequest(float baseAmount, GameObject cause)
		{
			BaseAmount = baseAmount;
			Cause = cause;
			HealCalculator = DefaultHealCalculator;
		}

		public float CalculateAmount(ICombatAttributes attributeSystem)
		{
			CalculatedAmount = HealCalculator.Invoke(attributeSystem, BaseAmount);
			return CalculatedAmount;
		}

		public static Func<ICombatAttributes, float, float> DefaultHealCalculator =>
			(attributes, baseAmount) => baseAmount;
	}
}