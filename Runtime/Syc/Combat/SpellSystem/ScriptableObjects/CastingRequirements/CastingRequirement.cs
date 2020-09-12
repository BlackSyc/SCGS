using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.CastingRequirements
{
	public abstract class CastingRequirement : ScriptableObject
	{
		public abstract GetSpellCastResult CanCast(ICaster caster, SpellBehaviour spell);
	}
}