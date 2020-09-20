using System;
using System.Collections.Generic;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class SpellRack : CastingSystem
	{
		[SerializeField]
		private List<Spell> spells;

		public void CastSpell(int spellIndex)
		{
			if (spellIndex > spells.Count)
				return;

			CastSpell(spells[spellIndex]);
		}

		public Spell AddSpell(SpellBehaviour spellBehaviour, int index)
		{
			var newSpell = new Spell(spellBehaviour);
			spells[index] = newSpell;
			return newSpell;
		}

		public Spell AddSpell(SpellBehaviour spellBehaviour)
		{
			var newSpell = new Spell(spellBehaviour);
			spells.Add(newSpell);
			return newSpell;
		}

		public void RemoveAll(SpellBehaviour spellBehaviour)
		{
			spells.RemoveAll(x => x.SpellBehaviour == spellBehaviour);
		}

		public void Remove(int index)
		{
			spells[index] = default;
		}
	}
}