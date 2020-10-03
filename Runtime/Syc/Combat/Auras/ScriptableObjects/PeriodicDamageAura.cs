using System.Collections;
using Syc.Combat.HealthSystem;
using UnityEngine;

namespace Syc.Combat.Auras.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Auras/Periodic Damage Aura")]
	public class PeriodicDamageAura : Aura
	{
		[Header("Periodic Damage Aura")]
		[Space]
		[SerializeField] protected float baseDamage;
		[SerializeField] protected bool allowMitigation;
		[SerializeField] protected bool affectedBySpellPower;
		[SerializeField] protected bool canCrit;
		[SerializeField] protected float critMultiplier;
		[SerializeField] protected float period;
		
		public override void Applied(AuraState state)
		{
			state.StartCoroutine(DamageCoroutine(state));
		}

		public override void Removed(AuraState state)
		{
			state.CoroutineShouldStop = true;
		}

		private IEnumerator DamageCoroutine(AuraState state)
		{
			if (!state.Target.Has(out HealthSystem.HealthSystem healthSystem))
				yield break;
			
			while (state.Stacks > 0)
			{
				healthSystem.Damage(CreateDamageRequest(state));
				yield return new WaitForSeconds(period);
			}
		}

		protected virtual DamageRequest CreateDamageRequest(AuraState state)
		{
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;

			if (affectedBySpellPower)
				attributeMultiplier *= state.Source.System.AttributeSystem.SpellPower.Remap();
			
			if(canCrit){
				if (Random.Range(0f, 1f) < state.Source.System.AttributeSystem.CriticalStrikeRating.Remap())
				{
					isCriticalStrike = true;
					attributeMultiplier *= critMultiplier;
				}
			}

			var damageSource = state.Source.System.Origin.gameObject;

			return new DamageRequest(baseDamage * attributeMultiplier * state.Stacks,
				isCriticalStrike,
				damageSource,
				allowMitigation
					? DamageRequest.WithMitigation
					: DamageRequest.NoMitigation);
		}
	}
}