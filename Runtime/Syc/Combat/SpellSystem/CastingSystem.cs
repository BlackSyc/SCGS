using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	public class CastingSystem : ICaster
	{
		public event Action<SpellCast> OnNewSpellCast;
		public event Action<SpellBehaviour, CastFailedReason> OnCastFailed;

		public Transform CastOrigin => castOrigin;
		
		public SpellCast CurrentSpellCast { get; private set; }

		[SerializeField]
		private Transform castOrigin;

		public void CastSpell(Spell spell)
		{
			if (spell == default)
			{
				OnCastFailed?.Invoke(null, CastFailedReason.SpellNotFound);
				return;
			}

			var result = spell.TryGetSpellCast(out var newSpellCast,this);

			if (result.Success)
			{
				SetCurrentSpellCast(newSpellCast);
				OnNewSpellCast?.Invoke(CurrentSpellCast);
			}
			else
			{
				OnCastFailed?.Invoke(result.Spell, result.Reason.GetValueOrDefault());
			}	
		}

		public void MovementIntterupt()
		{
			CurrentSpellCast?.Cancel(CancelCastReason.Movement);
		}

		public ICombatSystem System { get; set; }

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