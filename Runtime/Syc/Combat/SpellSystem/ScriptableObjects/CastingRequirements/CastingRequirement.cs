using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.CastingRequirements
{
	public abstract class CastingRequirement : ScriptableObject
	{
		public abstract CreateSpellCastResult CanCast(ICaster caster, SpellState spellState);
	}
}