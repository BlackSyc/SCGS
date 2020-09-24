using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects
{
	[CreateAssetMenu(menuName = "SpellState System/Effects/Deal Damage")]
	public class DealDamage : SpellEffect
	{
		[SerializeField] 
		private float damageAmount;

		[SerializeField] 
		private float criticalStrikeMultiplier = 2;

		[SerializeField] 
		private bool allowDamageMitigation;

		[SerializeField] 
		private bool applyAttributeBias;
		
		public override void Execute(ICaster caster, Target target, Spell spell, SpellCast spellCast = default, SpellObject spellObject = default)
		{
			if (!target.IsCombatTarget)
				return;

			if (!target.CombatSystem.Has(out HealthSystem.HealthSystem healthComponent))
				return;

			if (applyAttributeBias)
			{
				healthComponent.Damage(
					new DamageRequest(
						CalculateDamageWithAttributeBias(damageAmount, caster), 
						caster.System.Origin.gameObject,
						allowDamageMitigation ? DamageRequest.WithMitigation : DamageRequest.NoMitigation));
			}
			else
			{
				healthComponent.Damage(new DamageRequest(damageAmount, caster.System.Origin.gameObject, allowDamageMitigation ? DamageRequest.WithMitigation : DamageRequest.NoMitigation));
			}
		}

		protected virtual float CalculateDamageWithAttributeBias(float amount, ICaster caster)
		{
			var spellPowerFactor = caster.System.AttributeSystem.SpellPower.Remap();
			var damageWithSpellPowerFactorApplied = spellPowerFactor * amount;

			var criticalStrikeChance = caster.System.AttributeSystem.CriticalStrikeRating.Remap();
			var randomFloat = Random.Range(0, 100);

			return randomFloat > criticalStrikeChance
				? damageWithSpellPowerFactorApplied
				: damageWithSpellPowerFactorApplied * criticalStrikeMultiplier;
		}
	}
}