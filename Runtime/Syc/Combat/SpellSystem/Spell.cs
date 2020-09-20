using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class Spell
	{
		[SerializeField]
		private SpellBehaviour spellBehaviour;
		
		public SpellBehaviour SpellBehaviour => spellBehaviour;
		
		public float CoolDownUntil { get; private set; }

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

		private void StartCoolDown(SpellCast spellCast) => CoolDownUntil = Time.time + spellBehaviour.CoolDown;
	}
}