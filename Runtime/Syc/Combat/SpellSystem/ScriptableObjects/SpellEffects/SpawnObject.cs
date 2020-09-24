using System;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects
{
	[CreateAssetMenu(menuName = "SpellState System/Effects/Spawn Object")]
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
					newSpellObject.Source = source;
					newSpellObject.Target = target;
					newSpellObject.SpellCast = spellCast;
					newSpellObject.Spell = spell;
					break;
				case TargetType.SelfCastOrigin:
					newSpellObject = Instantiate(spawn, target.TargetObject.transform);
					newSpellObject.Source = source;
					newSpellObject.Target = target;
					newSpellObject.SpellCast = spellCast;
					newSpellObject.Spell = spell;
					break;
				case TargetType.SelfOrigin:
					newSpellObject = Instantiate(spawn, target.TargetObject.transform);
					newSpellObject.Source = source;
					newSpellObject.Target = target;
					newSpellObject.SpellCast = spellCast;
					newSpellObject.Spell = spell;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

	}
}