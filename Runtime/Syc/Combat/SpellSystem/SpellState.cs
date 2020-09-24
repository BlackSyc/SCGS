using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class SpellState
	{
		public Spell Spell => spell;

		public float CoolDownUntil => coolDownUntil;
		
		[SerializeField]
		private Spell spell;

		[SerializeField] private float coolDownUntil;

		public SpellState(Spell spell)
		{
			this.spell = spell;
		}
		
		public CreateSpellCastResult TryCreateSpellCast(out SpellCast spellCast, ICaster caster)
		{
			if (spell == default)
			{
				spellCast = null;
				return CreateSpellCastResult.Failed(CastFailedReason.SpellNotFound, this);
			}

			var result = spell.TryCreateSpellCast(out spellCast, caster, this);

			if (result.Success)
			{
				spellCast.OnSpellCompleted += StartCoolDown;
			}

			return result;
		}

		private void StartCoolDown(SpellCast spellCast) => coolDownUntil = Time.time + spell.CoolDown;
	}
}