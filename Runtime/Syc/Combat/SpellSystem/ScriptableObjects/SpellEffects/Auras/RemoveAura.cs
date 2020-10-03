using Syc.Combat.Auras;
using Syc.Combat.Auras.ScriptableObjects;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Auras
{
	[CreateAssetMenu(menuName = "Spell System/Spells/Effects/Remove Aura")]
	public class RemoveAura : SpellEffect
	{
		[SerializeField] private Aura aura;
		
		[SerializeField] private bool useSpellAsReferenceObject;
		public override void Execute(ICaster source, Target target, Spell spell, SpellCast spellCast = default, SpellObject spellObject = default)
		{
			if (!target.IsCombatTarget)
				return;

			if (target.CombatSystem.Has(out AuraSystem auraSystem))
			{
				auraSystem.RemoveAura(aura, useSpellAsReferenceObject 
					? spell 
					: (object)source);
			}
		}
	}
}