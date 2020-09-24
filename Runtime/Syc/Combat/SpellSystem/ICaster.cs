using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	public interface ICaster : ICombatSubSystem
	{
		event Action<SpellCast> OnNewSpellCast;
		event Action<Spell, CastFailedReason> OnCastFailed;
		
		Transform CastOrigin { get; }

		SpellCast CurrentSpellCast { get; }
	}
}