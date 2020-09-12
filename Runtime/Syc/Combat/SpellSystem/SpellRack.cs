using System;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class SpellRack : CastingSystem
	{
		[SerializeField]
		private Spell[] spells;

		public void CastSpell(int spellIndex) => CastSpell(spells[spellIndex]);
	}
}