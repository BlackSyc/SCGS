using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Health/Apply Heal From Range")]
	public class ApplyHealFromRange : ApplyHeal
	{
		public float HealCeiling => healCeiling;

		[SerializeField] protected float healCeiling;

		public virtual float CalculateHealCeilingWith(ICombatAttributes attributes)
		{
			return affectedBySpellPower
				? HealCeiling * attributes.SpellPower.Remap()
				: HealCeiling;
		}
		
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

			if (affectedBySpellPower)
				attributeMultiplier *= caster.System.AttributeSystem.SpellPower.Remap();
			
			if(canCrit)
			{
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