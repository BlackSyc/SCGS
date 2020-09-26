using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Effects/Health/Deal Damage")]
	public class DealDamage : SpellEffect
	{
		[SerializeField] 
		protected float damageAmount;

		[SerializeField] 
		protected float criticalStrikeMultiplier = 2;

		[SerializeField] 
		protected bool allowDamageMitigation;

		[SerializeField] 
		protected bool applyAttributeBias;
		
		public override void Execute(ICaster caster, Target target, Spell spell, SpellCast spellCast = default, SpellObject spellObject = default)
		{
			if (!target.IsCombatTarget)
				return;

			if (!target.CombatSystem.Has(out HealthSystem.HealthSystem healthComponent))
				return;

			healthComponent.Damage(CreateDamageRequest(caster, target, spell, spellCast, spellObject));
		}

		protected virtual DamageRequest CreateDamageRequest(
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

			var damageSource = caster.System.Origin.gameObject;

			return new DamageRequest(damageAmount * attributeMultiplier,
				isCriticalStrike,
				damageSource,
				allowDamageMitigation
					? DamageRequest.WithMitigation
					: DamageRequest.NoMitigation);
		}
	}
}