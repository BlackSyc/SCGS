using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class Spell
	{
		public SpellBehaviour SpellBehaviour => spellBehaviour;

		public float CoolDownUntil => coolDownUntil;
		
		[SerializeField]
		private SpellBehaviour spellBehaviour;

		[SerializeField] private float coolDownUntil;

		public Spell(SpellBehaviour spellBehaviour)
		{
			this.spellBehaviour = spellBehaviour;
		}
		
		public GetSpellCastResult TryGetSpellCast(out SpellCast spellCast, ICaster caster)
		{
			if (spellBehaviour == default)
			{
				spellCast = null;
				return new GetSpellCastResult(false, spellBehaviour, CastFailedReason.SpellNotFound);
			}
			
			if (CoolDownUntil > Time.time)
			{
				spellCast = null;
				return new GetSpellCastResult(false, spellBehaviour, CastFailedReason.OnCoolDown); 
			}

			var result = spellBehaviour.TryGetSpellCast(out spellCast, caster);

			if (result.Success)
			{
				spellCast.OnSpellCompleted += StartCoolDown;
			}

			return result;
		}

		private void StartCoolDown(SpellCast spellCast) => coolDownUntil = Time.time + spellBehaviour.CoolDown;
	}
}