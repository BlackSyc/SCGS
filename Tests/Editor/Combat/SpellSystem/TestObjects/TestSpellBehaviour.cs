using System;
using System.Collections.Generic;
using Syc.Combat.SpellSystem.ScriptableObjects;
using Syc.Combat.SpellSystem.ScriptableObjects.CastHandlers;
using Syc.Combat.SpellSystem.ScriptableObjects.CastingRequirements;
using Syc.Combat.SpellSystem.ScriptableObjects.SpellHitHandlers;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using UnityEngine;

namespace Tests.Editor.Combat.SpellSystem
{
	public class TestSpellBehaviour : SpellBehaviour
	{
		public string SpellName
		{
			set => spellName = value;
		}

		public string SpellDescription
		{
			set => spellDescription = value;
		}

		public float CastTime
		{
			set => castTime = value;
		}

		public float CoolDown
		{
			set => coolDown = value;
		}

		public TargetProvider TargetProvider
		{
			set => targetProvider = value;
		}

		public List<CastingRequirement> CastingRequirements
		{
			set => castingRequirements = value;
		}

		public List<CastHandler> StartCastHandlers
		{
			set => startCastHandlers = value;
		}

		public List<CastHandler> UpdateCastHandlers
		{
			set => updateCastHandlers = value;
		}

		public List<CastHandler> CompleteCastHandlers
		{
			set => completeCastHandlers = value;
		}

		public List<CastHandler> CancelCastHandlers
		{
			set => cancelCastHandlers = value;
		}

		public List<SpellHitHandler> SpellHitHandlers
		{
			set => spellHitHandlers = value;
		}

		public void OnEnable()
		{
			spellName = "Test Spell";
			spellDescription = "Test Spell Description";
		}
	}
}