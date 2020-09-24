using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	public abstract class TargetProvider : ScriptableObject
	{
		public abstract bool HasValidTarget(ICaster caster, out Target target);
	}
}