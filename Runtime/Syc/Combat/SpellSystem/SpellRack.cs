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
		public event Action<SpellState, int> OnSpellAdded;
		public event Action<SpellState, int> OnSpellRemoved;
		
		[SerializeField]
		private SpellState[] spells;

		public SpellState GetSpell(int index)
		{
			return index < spells.Length 
				? spells[index] 
				: default;
		}

		public void CastSpell(int spellIndex)
		{
			if (spellIndex > spells.Length)
				return;

			CastSpell(spells[spellIndex]);
		}

		public SpellState AddSpell(Spell spell, int index)
		{
			var newSpell = new SpellState(spell);
			spells[index] = newSpell;
			OnSpellAdded?.Invoke(newSpell, index);
			return newSpell;
		}

		public void RemoveAll(Spell spell)
		{
			var spellStatesToRemove = spells.Where(x => x.Spell == spell);

			foreach (var spellState in spellStatesToRemove)
			{
				var index = Array.IndexOf(spells, spellState);
				spells[index] = default;
				OnSpellRemoved?.Invoke(spellState, index);
			}
		}

		public void RemoveAllSpells()
		{
			var spellsCopy = new List<SpellState>(spells);

			foreach (var spell in spellsCopy)
			{
				RemoveAll(spell.Spell);
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