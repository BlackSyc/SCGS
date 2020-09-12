using Syc.Combat.SpellSystem.ScriptableObjects;

namespace Syc.Combat.SpellSystem
{
	public class GetSpellCastResult
	{
		public GetSpellCastResult(bool success, SpellBehaviour spell, CastFailedReason reason)
		{
			Success = success;
			Spell = spell;
			Reason = reason;
		}
		
		public GetSpellCastResult(bool success, SpellBehaviour spell)
		{
			Success = success;
			Spell = spell;
			Reason = default;
		}

		public bool Success { get; }

		public CastFailedReason? Reason { get; }
		
		public SpellBehaviour Spell { get; }
	}
}