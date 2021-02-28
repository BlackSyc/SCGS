using System.Collections.Generic;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects
{
	public abstract class SpellEffect : ScriptableObject
	{
		public List<SpellEffectTrigger> Triggers => triggers;
		
		[SerializeField]
		protected List<SpellEffectTrigger> triggers;

		public abstract void Execute(ICaster source, Target target, Spell spell, SpellCast spellCast = default, SpellObject spellObject = default);
	}
}