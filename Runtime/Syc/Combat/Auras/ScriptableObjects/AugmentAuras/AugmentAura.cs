using System.Collections.Generic;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.Auras.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Auras/Augment Auras/Augment Aura")]
	public class AugmentAura : Aura
	{
		[Header("Augment Aura")] 
		[Space] 
		[SerializeField] private List<Spell> appliesTo;

		public bool AppliesTo(Spell spell) => appliesTo.Contains(spell);
	}
	
	public enum OnUse
	{
		RemoveStack,
		Remove,
		DoNothing
	}
}