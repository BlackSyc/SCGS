using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Health/Apply Heal")]
	public class ApplyHeal : SpellEffect
	{
		[SerializeField] 
		protected float healAmount;

		[SerializeField] 
		protected float criticalStrikeMultiplier = 2;

		[SerializeField] 
		protected bool applyAttributeBias;
		
		public override void Execute(ICaster caster, Target target, Spell spell, SpellCast spellCast = default,
			SpellObject spellObject = default)
		{
			if (!target.IsCombatTarget)
				return;

			if (!target.CombatSystem.Has(out HealthSystem.HealthSystem healthComponent))
				return;

			healthComponent.Heal(CreateHealRequest(caster, target, spell, spellCast, spellObject));
		}
		
		protected virtual HealRequest CreateHealRequest(
			ICaster caster, 
			Target target, 
			Spell spell, 
			SpellCast spellCast = default, 
			SpellObject spellObject = default)
		{
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;

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

			return new HealRequest(healAmount * attributeMultiplier,
				isCriticalStrike,
				healingSource,
				HealRequest.DefaultHealCalculator);
		}
	}
}