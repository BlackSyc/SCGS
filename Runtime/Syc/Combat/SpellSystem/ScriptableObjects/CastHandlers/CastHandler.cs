using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.CastHandlers
{
	public abstract class CastHandler : ScriptableObject
	{
		public abstract void Handle(SpellCast spellCast);
	}
}