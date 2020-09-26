using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Health/Deal Damage From Range")]
	public class DealDamageFromRange : DealDamage
	{
		[SerializeField] protected float damageCeiling;
		
		protected override DamageRequest CreateDamageRequest(
			ICaster caster,
			Target target,
			Spell spell,
			SpellCast spellCast = default,
			SpellObject spellObject = default)
		{
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;
			var damageAmountFromRange = Random.Range(0f, 1f) * (damageCeiling - damageAmount) + damageAmount;

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

			return new DamageRequest(damageAmountFromRange * attributeMultiplier,
				isCriticalStrike,
				damageSource,
				allowDamageMitigation
					? DamageRequest.WithMitigation
					: DamageRequest.NoMitigation);
		}

	}
}