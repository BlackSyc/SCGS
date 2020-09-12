using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	public abstract class TargetProvider : ScriptableObject
	{
		public abstract Target GetTarget(ICaster caster);
	}
}