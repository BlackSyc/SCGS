using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Syc.Combat.SpellSystem.ScriptableObjects;
using Syc.Combat.SpellSystem.ScriptableObjects.Augments;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	public interface ICaster : ICombatSubSystem
	{
		event Action<SpellCast> OnNewSpellCast;
		event Action<Spell, CastFailedReason> OnCastFailed;
		
		IEnumerable<Augment> Augments { get; }
		
		Transform CastOrigin { get; }

		SpellCast CurrentSpellCast { get; }

		bool GlobalCooldownIsActive { get; }

		void AddAugment(object referenceObject, Augment augment);

		void RemoveAugmentsFor(object referenceObject, Augment augment = default);

		void RemoveAllAugments(IEnumerable<Augment> augments);
	}
}