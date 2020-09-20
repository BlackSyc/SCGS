using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class Spell
	{
		private SpellBehaviour _spellBehaviour;
		
		public SpellBehaviour SpellBehaviour => _spellBehaviour;
		
		public float CoolDownUntil { get; private set; }

		public Spell(SpellBehaviour spellBehaviour)
		{
			_spellBehaviour = spellBehaviour;
		}
		
		public GetSpellCastResult TryGetSpellCast(out SpellCast spellCast, ICaster caster)
		{
			if (_spellBehaviour == default)
			{
				spellCast = null;
				return new GetSpellCastResult(false, _spellBehaviour, CastFailedReason.SpellNotFound);
			}
			
			if (CoolDownUntil > Time.time)
			{
				spellCast = null;
				return new GetSpellCastResult(false, _spellBehaviour, CastFailedReason.OnCoolDown); 
			}

			var result = _spellBehaviour.TryGetSpellCast(out spellCast, caster);

			if (result.Success)
			{
				spellCast.OnSpellCompleted += StartCoolDown;
			}

			return result;
		}

		private void StartCoolDown(SpellCast spellCast) => CoolDownUntil = Time.time + _spellBehaviour.CoolDown;
	}
}