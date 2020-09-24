using System;
using Syc.Combat.SpellSystem.ScriptableObjects;

namespace Syc.Combat.SpellSystem
{
	public class CreateSpellCastResult
	{
		private CreateSpellCastResult(bool success, SpellState spellState, CastFailedReason reason, string reasonText = default)
		{
			Success = success;
			SpellState = spellState;
			Reason = reason;
			ReasonText = reasonText ?? reason.ToString();
		}
		
		private CreateSpellCastResult(bool success, SpellState spellState)
		{
			Success = success;
			SpellState = spellState;
			Reason = default;
		}

		public bool Success { get; }

		public CastFailedReason? Reason { get; }
		
		public string ReasonText { get; }
		
		public SpellState SpellState { get; }

		public static CreateSpellCastResult Failed(CastFailedReason reason, SpellState spellState, string reasonText = default)
		{
			return new CreateSpellCastResult(false, spellState, reason, reasonText);
		}

		public static CreateSpellCastResult Succeeded(SpellState spellState)
		{
			return new CreateSpellCastResult(true, spellState);
		}
	}
}