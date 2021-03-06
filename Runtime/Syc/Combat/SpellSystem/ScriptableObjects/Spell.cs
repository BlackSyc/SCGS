﻿using System.Collections.Generic;
using System.Linq;
using Syc.Combat.SpellSystem.ScriptableObjects.CastingRequirements;
using Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Spell")]
	public class Spell : ScriptableObject
	{
		public string SpellName => spellName;

		public Sprite Icon => icon;

		public string SpellDescription => spellDescription;
		
		public float CastTime => castTime;

		public float CoolDown => coolDown;

		public bool CastWhileMoving => castWhileMoving;

		public bool OnGlobalCooldown => onGlobalCooldown;

		public IEnumerable<SpellEffect> SpellEffects => spellEffects;
		
		[SerializeField] protected string spellName;

		[SerializeField] protected Sprite icon;

		[SerializeField]
		[TextArea(5,10)]
		protected string spellDescription;
		
		[Space]
		[Header("Properties")]
		[SerializeField]
		protected float castTime;

		[SerializeField] 
		protected float coolDown;

		[SerializeField] protected bool onGlobalCooldown;

		[SerializeField] protected bool castWhileMoving;
		
		[Space]
		[SerializeField]
		protected TargetProvider target;
		
		[SerializeField] protected List<CastingRequirement> castingRequirements = new List<CastingRequirement>();

		[SerializeField] protected List<SpellEffect> spellEffects = new List<SpellEffect>();


		/// <summary>
		/// Checks whether this spell could be cast at this moment.
		/// Returns a result object containing whether the spell can be cast.
		/// Additionally, returns a new instance of SpellCast that can be used to start the cast of this spell.
		/// </summary>
		/// <param name="spellCast">An instance of SpellCast that will be overridden with a new instance on success.</param>
		/// <param name="caster"></param>
		/// <param name="spellState"></param>
		/// <returns></returns>
		public CreateSpellCastResult TryCreateSpellCast(out SpellCast spellCast, ICaster caster, SpellState spellState)
		{
			spellCast = default;
			
			if(onGlobalCooldown && caster.GlobalCooldownIsActive)
				return CreateSpellCastResult.Failed(CastFailedReason.OnGlobalCoolDown, spellState);
			
			if (coolDown > 0 && spellState.CoolDownUntil > Time.time)
				return CreateSpellCastResult.Failed(CastFailedReason.OnCoolDown, spellState);
			
			var failedResult = castingRequirements?
				.Select(castingRequirement => castingRequirement.CanCast(caster, spellState))
				.FirstOrDefault(result => !result.Success);

			if (failedResult != null)
				return failedResult;

			Target spellTarget = default;

			if (target != null && !target.HasValidTarget(caster, out spellTarget))
				return CreateSpellCastResult.Failed(CastFailedReason.InvalidTarget, spellState);

			spellCast = new SpellCast(this, caster, spellTarget);
			
			return CreateSpellCastResult.Succeeded(spellState);
		}

		public void ExecuteAll(SpellEffectTrigger trigger, ICaster source, Target target, SpellCast spellCast = default, SpellObject spellObject = default)
		{
			foreach (var effect in spellEffects.Where(x => x.Triggers.Any(y => y == trigger)))
			{
				effect.Execute(source, target, this, spellCast, spellObject);
			}
		}

		/// <summary>
		///  Should be called from instance of SpellCast whenever the cast duration is completed.
		/// </summary>
		public void StartCast(SpellCast spellCast) => ExecuteAll(SpellEffectTrigger.OnCastStarted, spellCast.Caster, spellCast.Target, spellCast);
		
		public void UpdateCast(SpellCast spellCast) => ExecuteAll(SpellEffectTrigger.OnCastProgress, spellCast.Caster, spellCast.Target, spellCast);
		
		public void CompleteCast(SpellCast spellCast) => ExecuteAll(SpellEffectTrigger.OnCastCompleted, spellCast.Caster, spellCast.Target, spellCast);
		
		public void CancelCast(SpellCast spellCast) => ExecuteAll(SpellEffectTrigger.OnCastCancelled, spellCast.Caster, spellCast.Target, spellCast);
	}
}