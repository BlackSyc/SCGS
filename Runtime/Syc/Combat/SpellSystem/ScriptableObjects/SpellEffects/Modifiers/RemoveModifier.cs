using Syc.Combat.ModifierSystem.ScriptableObjects;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Modifiers
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Remove Modifier")]
	public class RemoveModifier : SpellEffect
	{
		[SerializeField] private Modifier modifier;
		
		[SerializeField] private bool useSpellAsReferenceObject;
		public override void Execute(ICaster source, Target target, Spell spell, SpellCast spellCast = default, SpellObject spellObject = default)
		{
			if (!target.IsCombatTarget)
				return;

			if (target.CombatSystem.Has(out ModifierSystem.ModifierSystem modifierSystem))
			{
				modifierSystem.RemoveModifier(modifier, useSpellAsReferenceObject 
					? spell 
					: (object)source);
			}
		}
	}
}