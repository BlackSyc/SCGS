using System.Collections.Generic;
using Syc.Combat.SpellSystem;
using Syc.Combat.SpellSystem.ScriptableObjects.Augments;
using UnityEngine;

namespace Syc.Combat.Auras.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Auras/Augment Aura")]
	public class AugmentAura : Aura
	{
		[Header("Augment Aura")]
		[Space]
		[SerializeField] protected List<Augment> augments;
		
		public override void AppliedStack(AuraState state)
		{
			if (!state.Target.Has(out ICaster caster))
				return;

			foreach (var augment in augments)
			{
				caster.AddAugment(this, augment);
			}
		}

		public override void Removed(AuraState state)
		{
			if (!state.Target.Has(out ICaster caster))
				return;

			caster.RemoveAugmentsFor(this);
		}
	}
}