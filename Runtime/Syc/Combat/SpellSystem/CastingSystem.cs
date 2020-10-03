using System;
using System.Collections.Generic;
using System.Linq;
using Syc.Combat.SpellSystem.ScriptableObjects;
using Syc.Combat.SpellSystem.ScriptableObjects.Augments;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	[Serializable]
	public class CastingSystem : ICaster
	{
		public event Action<SpellCast> OnNewSpellCast;
		public event Action<Spell, CastFailedReason> OnCastFailed;
		public event Action<float> OnGlobalCooldownStarted;
		public event Action OnGlobalCooldownCancelled;
		
		public ICombatSystem System { get; set; }

		public IEnumerable<Augment> Augments => _augments.Values.SelectMany(x => x);
		
		public Transform CastOrigin => castOrigin;
		
		public SpellCast CurrentSpellCast { get; private set; }

		public bool GlobalCooldownIsActive => _globalCooldownRemaining > 0;

		public float GlobalCooldown => globalCooldown * System.AttributeSystem.Haste.Remap();

		private float _globalCooldownRemaining;

		private Dictionary<object, List<Augment>> _augments = new Dictionary<object, List<Augment>>();

		[SerializeField] private Transform castOrigin;

		[SerializeField] private float globalCooldown = 1f;

		public void CastSpell(SpellState spellState)
		{
			if (spellState == default)
			{
				OnCastFailed?.Invoke(null, CastFailedReason.SpellNotFound);
				return;
			}
			
			if (CurrentSpellCast != null)
			{
				if (CurrentSpellCast.Spell == spellState.Spell)
				{
					OnCastFailed?.Invoke(spellState.Spell, CastFailedReason.AlreadyCasting);
					return;
				}
				CurrentSpellCast.Cancel(CancelCastReason.CastReplaced);
			}

			var result = spellState.TryCreateSpellCast(out var newSpellCast,this);

			if (result.Success)
			{
				SetCurrentSpellCast(newSpellCast);
				OnNewSpellCast?.Invoke(CurrentSpellCast);
				CurrentSpellCast.Start();
				if (result.SpellState.Spell.OnGlobalCooldown)
				{
					_globalCooldownRemaining = GlobalCooldown;
					OnGlobalCooldownStarted?.Invoke(_globalCooldownRemaining);
				}
			}
			else
			{
				OnCastFailed?.Invoke(result.SpellState.Spell, result.Reason.GetValueOrDefault());
			}	
		}

		public void MovementIntterupt()
		{
			if (CurrentSpellCast?.Spell == null)
				return;

			if (CurrentSpellCast.Spell.CastWhileMoving || CurrentSpellCast.Spell.CastTime == 0)
				return;
			
			CurrentSpellCast?.Cancel(CancelCastReason.Movement);
		}
		
		public void Tick(float deltaTime)
		{
			_globalCooldownRemaining -= deltaTime;
			CurrentSpellCast?.Update(deltaTime);
		}

		public void AddAugment(object referenceObject, Augment augment)
		{
			if(referenceObject == default)
				throw new ArgumentNullException($"{nameof(referenceObject)} can not be null.");
			
			if (_augments.TryGetValue(referenceObject, out var existingAugmentList))
			{
				existingAugmentList.Add(augment);
			}
			else
			{
				_augments.Add(referenceObject, new List<Augment>{ augment });
			}
		}

		public void RemoveAugmentsFor(object referenceObject, Augment augment = default)
		{
			if(referenceObject == default)
				throw new ArgumentNullException($"{nameof(referenceObject)} can not be null.");
			
			if (!_augments.TryGetValue(referenceObject, out var existingAugmentList))
				return;

			if (augment != default)
			{
				existingAugmentList.RemoveAll(x => x == augment);
				return;
			}

			_augments.Remove(referenceObject);
		}

		public void RemoveAllAugments(IEnumerable<Augment> augments)
		{
			foreach (var augment in augments)
			{
				var valuesContainingAugment = _augments.Values.Where(x => x.Contains(augment));
				foreach (var augmentList in valuesContainingAugment)
				{
					augmentList.RemoveAll(x => x == augment);
				}
			}

			var emptyAugmentEntries = _augments
				.Where(x => x.Value.Count < 1)
				.Select(x => x.Key)
				.ToList();

			foreach (var emptyAugmentEntry in emptyAugmentEntries)
			{
				_augments.Remove(emptyAugmentEntry);
			}	
		}

		private void SetCurrentSpellCast(SpellCast newSpellCast)
		{
			CurrentSpellCast?.Cancel(CancelCastReason.CastReplaced);

			newSpellCast.OnSpellCompleted += RemoveCurrentSpellCast;
			newSpellCast.OnSpellCancelled += ResetGlobalCooldown;
			newSpellCast.OnSpellCancelled += RemoveCurrentSpellCast;

			CurrentSpellCast = newSpellCast;
		}

		private void ResetGlobalCooldown(SpellCast spellCast)
		{
			if (!spellCast.Spell.OnGlobalCooldown || !GlobalCooldownIsActive)
				return;

			_globalCooldownRemaining = 0;
			OnGlobalCooldownCancelled?.Invoke();
		}

		private void RemoveCurrentSpellCast(SpellCast currentSpellCast)
		{
			if(CurrentSpellCast == currentSpellCast)
				CurrentSpellCast = default;
		}
	}
}