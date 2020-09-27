using System.Collections;
using Syc.Combat.HealthSystem;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Modifiers/Effects/Apply Periodic Heal")]
	public class ApplyPeriodicHeal : ModifierEffect
	{
		public float HealAmount => healAmount;
		public bool CanCrit => canCrit;
		public float CriticalStrikeMultiplier => criticalStrikeMultiplier;
		public bool AffectedBySpellPower => affectedBySpellPower;

		[SerializeField] protected float healAmount;
		[SerializeField] protected bool canCrit;
		[SerializeField] protected float criticalStrikeMultiplier = 2;
		[SerializeField] protected bool affectedBySpellPower;
		[SerializeField] protected float timePeriod;
		
		public override void Execute(ICaster caster, ICombatSystem target, Modifier modifier, object referenceObject, float elapsedTime)
		{
			target.ExecuteCoroutine(PeriodicDamageCoroutine(caster, target, modifier, referenceObject, elapsedTime));
		}
		
		public virtual float CalculateDamageAmountWith(ICombatAttributes combatAttributes)
		{
			return affectedBySpellPower
				? HealAmount * combatAttributes.SpellPower.Remap()
				: HealAmount;
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
				healthSystem.Heal(CreateHealRequest(caster, target, modifier, referenceObject, elapsedTime, modifierState.Stacks));
				yield return new WaitForSeconds(timePeriod);
			}
		}

		protected virtual HealRequest CreateHealRequest(
			ICaster caster, 
			ICombatSystem target, 
			Modifier modifier, 
			object referenceObject, 
			float elapsedTime,
			int currentModifierStacks)
		{
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;

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

			return new HealRequest(healAmount * currentModifierStacks * attributeMultiplier,
				isCriticalStrike,
				healingSource,
				HealRequest.DefaultHealCalculator);
		}
	}
}