using System.Collections.Generic;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.Augments
{
	public abstract class Augment : ScriptableObject
	{
		public bool RemoveOnUse => removeOnUse;

		[SerializeField] private bool removeOnUse;
		[SerializeField] private List<Spell> appliesTo;

		public bool AppliesTo(Spell spell)
		{
			return appliesTo.Contains(spell);
		}
	}
}