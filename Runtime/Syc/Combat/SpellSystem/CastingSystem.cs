using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
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

		public Transform CastOrigin => castOrigin;
		
		public SpellCast CurrentSpellCast { get; private set; }

		public bool GlobalCooldownIsActive => _globalCooldownRemaining > 0;

		public float GlobalCooldown => globalCooldown * System.AttributeSystem.Haste.Remap();

		private float _globalCooldownRemaining;

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