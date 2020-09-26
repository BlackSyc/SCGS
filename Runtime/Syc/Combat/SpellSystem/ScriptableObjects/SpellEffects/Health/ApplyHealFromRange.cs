using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Effects/Health/Apply Heal From Range")]
	public class ApplyHealFromRange : ApplyHeal
	{
		[SerializeField] protected float healCeiling;
		protected override HealRequest CreateHealRequest(
			ICaster caster, 
			Target target, 
			Spell spell, 
			SpellCast spellCast = default, 
			SpellObject spellObject = default)
		{
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;
			var healAmountFromRange = Random.Range(0f, 1f) * (healCeiling - healAmount) + healAmount;

			if (applyAttributeBias)
			{
				attributeMultiplier *= caster.System.AttributeSystem.SpellPower.Remap();

				if (Random.Range(0f, 1f) < caster.System.AttributeSystem.CriticalStrikeRating.Remap())
				{
					isCriticalStrike = true;
					attributeMultiplier *= criticalStrikeMultiplier;
				}
			}

			var healingSource = caster.System.Origin.gameObject;

			return new HealRequest(healAmountFromRange * attributeMultiplier,
				isCriticalStrike,
				healingSource,
				HealRequest.DefaultHealCalculator);
		}
	}
}