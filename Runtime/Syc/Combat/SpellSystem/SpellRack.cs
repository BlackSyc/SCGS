using System;
using System.Collections.Generic;
using System.Linq;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class SpellRack : CastingSystem
	{
		public event Action<Spell, int> OnSpellAdded;
		public event Action<Spell, int> OnSpellRemoved;
		
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
			OnSpellAdded?.Invoke(newSpell, index);
			return newSpell;
		}

		public Spell AddSpell(SpellBehaviour spellBehaviour)
		{
			var newSpell = new Spell(spellBehaviour);
			spells.Add(newSpell);
			OnSpellAdded?.Invoke(newSpell, spells.IndexOf(newSpell));
			return newSpell;
		}

		public void RemoveAll(SpellBehaviour spellBehaviour)
		{
			var spellsToRemove = spells.Where(x => x.SpellBehaviour == spellBehaviour);

			foreach (var spell in spellsToRemove)
			{
				var index = spells.IndexOf(spell);
				spells.RemoveAt(index);
				OnSpellRemoved?.Invoke(spell, index);
			}
		}

		public void RemoveAllSpells()
		{
			var spellsCopy = new List<Spell>(spells);

			foreach (var spell in spellsCopy)
			{
				RemoveAll(spell.SpellBehaviour);
			}
		}

		public void Remove(int index)
		{
			var spellToRemove = spells[index];
			if (spellToRemove == default)
				return;
			
			spells[index] = default;
			OnSpellRemoved?.Invoke(spellToRemove, index);
		}
	}
}