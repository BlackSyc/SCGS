using System;
using UnityEngine;

namespace Syc.Combat.Auras.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Auras/Augment Auras/Damage Augment")]
	public class DamageAugmentAura : AugmentAura
	{
		[SerializeField] private OnUse onUse;
		[SerializeField] private float multiplyBaseDamage = 1;
		
		[SerializeField] private float addCritChance;

		public float CalculateCritChanceAddition(AuraState auraState)
		{
			return addCritChance * auraState.Stacks;
		}
		
		public float CalculateBaseDamageMultiplier(AuraState auraState)
		{
			return multiplyBaseDamage * auraState.Stacks;
		}

		public void Used(AuraState auraState)
		{
			switch (onUse)
			{
				case OnUse.RemoveStack:
					auraState.RemoveStack();
					break;
				case OnUse.Remove:
					auraState.Remove();
					break;
				case OnUse.DoNothing:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}