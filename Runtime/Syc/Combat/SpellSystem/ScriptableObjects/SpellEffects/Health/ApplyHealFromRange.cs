using System.Collections.Generic;
using System.Linq;
using Syc.Combat.Auras;
using Syc.Combat.Auras.ScriptableObjects;
using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Health/AddStack Heal From Range")]
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
			var healAugments = !caster.System.Has(out AuraSystem auraSystem) 
				? new List<(HealAugmentAura, AuraState)>() 
				: auraSystem.ActiveAuras
					.Where(x => x.AuraType.GetType() == typeof(HealAugmentAura))
					.Select(auraState => ((HealAugmentAura)auraState.AuraType, auraState))
					.Where(x => x.Item1.AppliesTo(spell))
					.ToList();

			var attributeMultiplier = 1f;
			var isCriticalStrike = false;
			var healAmountFromRange = Random.Range(0f, 1f) * (healCeiling - healAmount) + healAmount;

			if (affectedBySpellPower)
				attributeMultiplier *= caster.System.AttributeSystem.SpellPower.Remap();
			
			if(canCrit)
			{
				if (Random.Range(0f, 1f) < caster.System.AttributeSystem.CriticalStrikeRating.Remap() 
					+ healAugments.Select(x => x.Item1.CalculateCritChanceAddition(x.Item2)).Sum())
				{
					isCriticalStrike = true;
					attributeMultiplier *= criticalStrikeMultiplier;
				}
			}

			var healingSource = caster.System.Origin.gameObject;
			
			var augmentMultiplier = !healAugments.Any()
				? 1
				: healAugments
					.Select(x => x.Item1.CalculateBaseHealMultiplier(x.Item2))
					.Aggregate((x, y) => x * y);

			foreach (var (healAugmentAura, auraState) in healAugments)
				healAugmentAura.Used(auraState);

			return new HealRequest(
				healAmountFromRange * attributeMultiplier * augmentMultiplier,
				isCriticalStrike,
				healingSource,
				HealRequest.DefaultHealCalculator);
		}
	}
}