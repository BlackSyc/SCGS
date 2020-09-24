using System.Collections.Generic;
using System.Linq;
using Syc.Combat.SpellSystem.ScriptableObjects.CastingRequirements;
using Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = "SpellState System/Spell")]
	public class Spell : ScriptableObject
	{
		public string SpellName => spellName;

		public string SpellDescription => spellDescription;
		
		public float CastTime => castTime;

		public float CoolDown => coolDown;
		
		[SerializeField] 
		protected string spellName;

		[SerializeField]
		[TextArea(5,10)]
		protected string spellDescription;
		
		[Space]
		[Header("Properties")]
		[SerializeField]
		protected float castTime;

		[SerializeField] 
		protected float coolDown;

		[SerializeField] protected bool globalCooldown;
		
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
			
			if(globalCooldown && caster.GlobalCooldownIsActive)
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

		public void ExecuteAll(SpellEffectType spellEffectType, ICaster source, Target target, SpellCast spellCast = default, SpellObject spellObject = default)
		{
			foreach (var behaviour in spellEffects.Where(x => x.Types.HasFlag(spellEffectType)))
			{
				behaviour.Execute(source, target, this, spellCast, spellObject);
			}
		}

		/// <summary>
		///  Should be called from instance of SpellCast whenever the cast duration is completed.
		/// </summary>
		public void StartCast(SpellCast spellCast) => ExecuteAll(SpellEffectType.OnStartCast, spellCast.Caster, spellCast.Target, spellCast);

		public void UpdateCast(SpellCast spellCast) => ExecuteAll(SpellEffectType.OnUpdateCast, spellCast.Caster, spellCast.Target, spellCast);

		public void CompleteCast(SpellCast spellCast) => ExecuteAll(SpellEffectType.OnCompleteCast, spellCast.Caster, spellCast.Target, spellCast);

		public void CancelCast(SpellCast spellCast) => ExecuteAll(SpellEffectType.OnCancelCast, spellCast.Caster, spellCast.Target, spellCast);
	}
}