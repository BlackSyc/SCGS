﻿using System.Collections.Generic;
using System.Linq;
using Syc.Combat.Auras;
using Syc.Combat.Auras.ScriptableObjects;
using Syc.Combat.HealthSystem;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Health
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Health/Deal Damage")]
	public class DealDamage : SpellEffect
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

		public override void Execute(ICaster caster, Target target, Spell spell, SpellCast spellCast = default, SpellObject spellObject = default)
		{
			if (!target.IsCombatTarget)
				return;

			if (!target.CombatSystem.Has(out HealthSystem.HealthSystem healthComponent))
				return;

			healthComponent.Damage(CreateDamageRequest(caster, target, spell, spellCast, spellObject));
		}

		public virtual float CalculateDamageAmountWith(ICombatAttributes combatAttributes)
		{
			return affectedBySpellPower
				? DamageAmount * combatAttributes.SpellPower.Remap()
				: DamageAmount;
		}

		protected virtual DamageRequest CreateDamageRequest(
			ICaster caster, 
			Target target, 
			Spell spell, 
			SpellCast spellCast = default, 
			SpellObject spellObject = default)
		{
			var damageAugments = !caster.System.Has(out AuraSystem auraSystem) 
				? new List<(DamageAugmentAura, AuraState)>() 
				: auraSystem.ActiveAuras
					.Where(x => x.AuraType.GetType() == typeof(DamageAugmentAura))
					.Select(auraState => ((DamageAugmentAura)auraState.AuraType, auraState))
					.Where(x => x.Item1.AppliesTo(spell))
					.ToList();
			
			var attributeMultiplier = 1f;
			var isCriticalStrike = false;

			if (affectedBySpellPower)
				attributeMultiplier *= caster.System.AttributeSystem.SpellPower.Remap();
			
			if(canCrit){
				if (Random.Range(0f, 1f) < caster.System.AttributeSystem.CriticalStrikeRating.Remap() 
					+ damageAugments.Select(x => x.Item1.CalculateCritChanceAddition(x.Item2)).Sum())
				{
					isCriticalStrike = true;
					attributeMultiplier *= criticalStrikeMultiplier;
				}
			}

			var damageSource = caster.System.Origin.gameObject;

			var augmentMultiplier = !damageAugments.Any()
				? 1
				: damageAugments
					.Select(x => x.Item1.CalculateBaseDamageMultiplier(x.Item2))
					.Aggregate((x, y) => x * y);

			foreach (var (damageAugmentAura, auraState) in damageAugments)
				damageAugmentAura.Used(auraState);

			return new DamageRequest(
				damageAmount * attributeMultiplier * augmentMultiplier,
				isCriticalStrike,
				damageSource,
				allowDamageMitigation
					? DamageRequest.WithMitigation
					: DamageRequest.NoMitigation);
		}
	}
}