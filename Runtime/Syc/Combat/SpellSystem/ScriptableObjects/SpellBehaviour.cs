﻿using System.Collections.Generic;
using System.Linq;
using Syc.Combat.SpellSystem.ScriptableObjects.CastHandlers;
using Syc.Combat.SpellSystem.ScriptableObjects.CastingRequirements;
using Syc.Combat.SpellSystem.ScriptableObjects.SpellHitHandlers;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Spell Behaviour")]
	public class SpellBehaviour : ScriptableObject
	{
		public float CastTime => castTime;

		public float CoolDown => coolDown;
		
		[Header("Spell Info")]
		[SerializeField] 
		private string spellName;

		[SerializeField]
		[TextArea(3,10)]
		private string spellDescription;
		
		[Header("Properties")]
		[SerializeField]
		private float castTime;

		[SerializeField] 
		private float coolDown;
		
		[Header("Before Cast")]
		[SerializeField]
		private TargetProvider targetProvider;

		[SerializeField]
		private List<CastingRequirement> castingRequirements;

		[Header("During Cast")]
		[SerializeField] 
		private List<CastHandler> startCastHandlers;

		[SerializeField] 
		private List<CastHandler> updateCastHandlers;
		
		[SerializeField] 
		private List<CastHandler> completeCastHandlers;
		
		[SerializeField] 
		private List<CastHandler> cancelCastHandlers;

		[Header("After Cast")]
		[SerializeField] 
		private List<SpellHitHandler> spellHitHandlers;

		/// <summary>
		/// Checks whether this spell could be cast at this moment.
		/// Returns a result object containing whether the spell can be cast.
		/// Additionally, returns a new instance of SpellCast that can be used to start the cast of this spell.
		/// </summary>
		/// <param name="spellCast">An instance of SpellCast that will be overridden with a new instance on success.</param>
		/// <param name="caster"></param>
		/// <returns></returns>
		public GetSpellCastResult TryGetSpellCast(out SpellCast spellCast, ICaster caster)
		{
			var failedResult = castingRequirements
				.Select(castingRequirement => castingRequirement.CanCast(caster, this))
				.FirstOrDefault(result => !result.Success);

			if (failedResult != null)
			{
				spellCast = null;
				return failedResult;
			}
			
			spellCast = new SpellCast(this, caster, targetProvider.GetTarget(caster));
			
			return new GetSpellCastResult(true, this);
		}

		/// <summary>
		///  Should be called from instance of SpellCast whenever the cast duration is completed.
		/// </summary>
		public void StartCast(SpellCast spellCast)
		{
			if (startCastHandlers == null || !startCastHandlers.Any())
				return;
			
			foreach (var startCastHandler in startCastHandlers)
			{
				startCastHandler.Handle(spellCast);
			}
		}

		public void UpdateCast(SpellCast spellCast)
		{
			if (updateCastHandlers == null || !updateCastHandlers.Any())
				return;
			
			foreach (var updateCastHandler in updateCastHandlers)
			{
				updateCastHandler.Handle(spellCast);
			}
		}

		public void CompleteCast(SpellCast spellCast)
		{
			if (completeCastHandlers == null || !completeCastHandlers.Any())
				return;
			
			foreach (var completeCastHandler in completeCastHandlers)
			{
				completeCastHandler.Handle(spellCast);
			}
		}

		public void CancelCast(SpellCast spellCast)
		{
			if (cancelCastHandlers == null || !cancelCastHandlers.Any())
				return;
			
			foreach (var cancelCastHandler in cancelCastHandlers)
			{
				cancelCastHandler.Handle(spellCast);
			}
		}

		public void SpellHit(ICaster caster, Target target)
		{
			if (spellHitHandlers == null || !spellHitHandlers.Any())
				return;

			foreach (var spellHitHandler in spellHitHandlers)
			{
				spellHitHandler.SpellHit(caster, target);
			}
		}
	}
}