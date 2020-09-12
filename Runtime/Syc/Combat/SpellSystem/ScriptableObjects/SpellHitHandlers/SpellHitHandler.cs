using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellHitHandlers
{
	public abstract class SpellHitHandler : ScriptableObject
	{
		public abstract void SpellHit(ICaster caster, Target target);
	}
}