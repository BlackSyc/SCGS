using System;
using System.Collections.Generic;
using Syc.Combat.SpellSystem.ScriptableObjects;
using Syc.Combat.SpellSystem.ScriptableObjects.CastingRequirements;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using UnityEngine;

namespace Tests.Editor.Combat.SpellSystem
{
	public class TestSpell : Spell
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
			set => target = value;
		}

		public List<CastingRequirement> CastingRequirements
		{
			set => castingRequirements = value;
		}

		public void OnEnable()
		{
			spellName = "Test SpellState";
			spellDescription = "Test SpellState Description";
		}
	}
}