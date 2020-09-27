using System.Collections;
using Syc.Combat.HealthSystem;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Modifiers/Effects/Deal Periodic Damage")]
	public class DealPeriodicDamage : ModifierEffect
	{
		public float DamageAmount => damageAmount;
		public bool CanCrit => canCrit;
		public float CriticalStrikeMultiplier => criticalStrikeMultiplier;
		public bool AffectedBySpellPower => affectedBySpellPower;
		public bool AllowDamageMitigation => allowDamageMitigation;

		[SerializeField] protected float damageAmount;
		[SerializeField] protected bool canCrit;
		[SerializeField] protected float criticalStrikeMultiplier = 2;
		[SerializeField] protected bool affectedBySpellPower;
		[SerializeField] protected bool allowDamageMitigation;
		[SerializeField] protected float timePeriod;
		
		public override void Execute(ICaster caster, ICombatSystem target, Modifier modifier, object referenceObject, float elapsedTime)
		{
			target.ExecuteCoroutine(PeriodicDamageCoroutine(caster, target, modifier, referenceObject, elapsedTime));
		}
		
		public virtual float CalculateDamageAmountWith(ICombatAttributes combatAttributes)
		{
			return affectedBySpellPower
				? DamageAmount * combatAttributes.SpellPower.Remap()
				: DamageAmount;
		}

		private IEnumerator PeriodicDamageCoroutine(ICaster caster, ICombatSystem target, Modifier modifier, object referenceObject, float elapsedTime)
		{
			if (!target.Has(out ModifierSystem modifierSystem))
				yield break;

			if (!target.Has(out HealthSystem.HealthSystem healthSystem))
				yield break;

			var modifierState = modifierSystem.GetModifier(modifier, referenceObject);

			while (modifierState.Stacks > 0)
			{
				healthSystem.Damage(CreateDamageRequest(caster, target, modifier, referenceObject, elapsedTime));
				yield return new WaitForSeconds(timePeriod);
			}
		}

		protected virtual DamageRequest CreateDamageRequest(
			ICaster caster, 
			ICombatSystem target, 
			Modifier modifier, 
			object referenceObject, 
			float elapsedTime)
		{
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;

			if (affectedBySpellPower)
				attributeMultiplier *= caster.System.AttributeSystem.SpellPower.Remap();
			
			if(canCrit){
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