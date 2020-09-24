using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects
{
	public abstract class SpellEffect : ScriptableObject
	{
		public SpellEffectType Types => types;
		
		[SerializeField]
		protected SpellEffectType types;

		public abstract void Execute(ICaster source, Target target, Spell spell, SpellCast spellCast = default, SpellObject spellObject = default);
	}
}