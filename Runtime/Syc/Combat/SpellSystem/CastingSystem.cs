using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	public class CastingSystem : ICaster
	{
		public event Action<SpellCast> OnNewSpellCast;
		public event Action<Spell, CastFailedReason> OnCastFailed;

		public float GlobalCoolDownUntil { get; set; }
		
		public ICombatSystem System { get; set; }

		public Transform CastOrigin => castOrigin;
		
		public SpellCast CurrentSpellCast { get; private set; }

		[SerializeField]
		private Transform castOrigin;

		public void CastSpell(SpellState spellState)
		{
			if (spellState == default)
			{
				OnCastFailed?.Invoke(null, CastFailedReason.SpellNotFound);
				return;
			}

			var result = spellState.TryCreateSpellCast(out var newSpellCast,this);

			if (result.Success)
			{
				SetCurrentSpellCast(newSpellCast);
				OnNewSpellCast?.Invoke(CurrentSpellCast);
			}
			else
			{
				OnCastFailed?.Invoke(result.SpellState.Spell, result.Reason.GetValueOrDefault());
			}	
		}

		public void MovementIntterupt()
		{
			CurrentSpellCast?.Cancel(CancelCastReason.Movement);
		}
		
		public void Tick(float deltaTime)
		{
			CurrentSpellCast?.Update(deltaTime);
		}

		private void SetCurrentSpellCast(SpellCast newSpellCast)
		{
			CurrentSpellCast?.Cancel(CancelCastReason.CastReplaced);

			newSpellCast.OnSpellCompleted += RemoveCurrentSpellCast;
			newSpellCast.OnSpellCancelled += RemoveCurrentSpellCast;

			CurrentSpellCast = newSpellCast;
		}

		private void RemoveCurrentSpellCast(SpellCast currentSpellCast)
		{
			if(CurrentSpellCast == currentSpellCast)
				CurrentSpellCast = default;
		}
	}
}