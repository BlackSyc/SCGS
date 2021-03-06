﻿using System;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Spawn Object")]
	public class SpawnObject : SpellEffect
	{
		
		[SerializeField] private SpellObject spawn;
		[SerializeField] private TargetType at;

		public override void Execute(ICaster source, Target target, Spell spell,  SpellCast spellCast = default, SpellObject spellObject = default)
		{
			if (spawn == default)
				return;

			SpellObject newSpellObject = default;

			switch (at)
			{
				case TargetType.Target:
					newSpellObject = Instantiate(spawn, target.TargetObject.transform);
					newSpellObject.transform.position = target.Position;
					newSpellObject.Source = source;
					newSpellObject.Target = target;
					newSpellObject.SpellCast = spellCast;
					newSpellObject.Spell = spell;
					newSpellObject.Initialize();
					break;
				case TargetType.SelfCastOrigin:
					newSpellObject = Instantiate(spawn, source.CastOrigin);
					newSpellObject.Source = source;
					newSpellObject.Target = target;
					newSpellObject.SpellCast = spellCast;
					newSpellObject.Spell = spell;
					newSpellObject.Initialize();
					break;
				case TargetType.SelfOrigin:
					newSpellObject = Instantiate(spawn, source.System.Origin);
					newSpellObject.Source = source;
					newSpellObject.Target = target;
					newSpellObject.SpellCast = spellCast;
					newSpellObject.Spell = spell;
					newSpellObject.Initialize();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

	}
}