using System.Linq;
using Syc.Combat.HealthSystem;
using Syc.Combat.SpellSystem.ScriptableObjects.Augments;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Health/AddStack Heal")]
	public class ApplyHeal : SpellEffect
	{
		public float HealAmount => healAmount;
		public bool CanCrit => canCrit;
		public float CriticalStrikeMultiplier => criticalStrikeMultiplier;
		public bool AffectedBySpellPower => affectedBySpellPower;

		[SerializeField] protected float healAmount;
		[SerializeField] protected bool canCrit;
		[SerializeField] protected float criticalStrikeMultiplier = 2;
		[SerializeField] protected bool affectedBySpellPower;
		
		public override void Execute(ICaster caster, Target target, Spell spell, SpellCast spellCast = default,
			SpellObject spellObject = default)
		{
			if (!target.IsCombatTarget)
				return;

			if (!target.CombatSystem.Has(out HealthSystem.HealthSystem healthComponent))
				return;

			healthComponent.Heal(CreateHealRequest(caster, target, spell, spellCast, spellObject));
		}
		
		public virtual float CalculateHealAmountWith(ICombatAttributes combatAttributes)
		{
			return affectedBySpellPower
				? HealAmount * combatAttributes.SpellPower.Remap()
				: HealAmount;
		}
		
		protected virtual HealRequest CreateHealRequest(
			ICaster caster, 
			Target target, 
			Spell spell, 
			SpellCast spellCast = default, 
			SpellObject spellObject = default)
		{
			var healAugments = caster.Augments
				.OfType<HealAugment>()
				.Where(x => x.AppliesTo(spell))
				.ToList();
			
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;

			if (affectedBySpellPower)
				attributeMultiplier *= caster.System.AttributeSystem.SpellPower.Remap();

			if(canCrit)
			{
				if (Random.Range(0f, 1f) < caster.System.AttributeSystem.CriticalStrikeRating.Remap() + healAugments.Select(x => x.AddCritChance).Sum())
				{
					isCriticalStrike = true;
					attributeMultiplier *= criticalStrikeMultiplier;
				}
			}

			var healingSource = caster.System.Origin.gameObject;
			
			var augmentMultiplier = !healAugments.Any()
				? 1
				: healAugments
					.Select(x => x.MultiplyBaseHeal)
					.Aggregate((x, y) => x * y);
			
			caster.RemoveAllAugments(healAugments.Where(x => x.RemoveOnUse));

			return new HealRequest(
				healAmount * attributeMultiplier * augmentMultiplier,
				isCriticalStrike,
				healingSource,
				HealRequest.DefaultHealCalculator);
		}
	}
}