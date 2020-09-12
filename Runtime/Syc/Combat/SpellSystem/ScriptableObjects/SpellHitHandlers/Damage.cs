using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellHitHandlers
{
	[CreateAssetMenu(menuName = "Spell System/Spell Hit Handlers/Damage")]
	public class Damage : SpellHitHandler
	{
		[SerializeField] 
		private float damageAmount;

		[SerializeField] 
		private float criticalStrikeMultiplier = 2;

		[SerializeField] 
		private bool allowDamageMitigation;

		[SerializeField] 
		private bool applyAttributeBias;
		
		public override void SpellHit(ICaster caster, Target target)
		{
			if (!target.IsCombatTarget)
				return;

			if (!target.CombatSystem.Has(out HealthComponent healthComponent))
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
			var randomFloat = Random.Range(0, 1);

			return randomFloat > criticalStrikeChance
				? damageWithSpellPowerFactorApplied
				: damageWithSpellPowerFactorApplied * criticalStrikeMultiplier;
		}
	}
}