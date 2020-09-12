using System;
using Syc.Combat.SpellSystem.ScriptableObjects;
using Syc.Combat.TargetSystem;

namespace Syc.Combat.SpellSystem
{
	public class SpellCast
	{
		public event Action<SpellCast> OnSpellCastStarted;
		public event Action<SpellCast> OnSpellCastProgress;
		public event Action<SpellCast> OnSpellCompleted;
		public event Action<SpellCast> OnSpellCancelled;
		
		public bool SpellCastStarted { get; private set; }
		public bool SpellCastCompleted { get; private set; }
		public bool SpellCastCancelled { get; private set; }
		
		public CancelCastReason? CancelCastReason { get; private set; }
		

		public float CurrentCastTime;
		public SpellBehaviour SpellBehaviour { get; }
		public Target Target { get; }
		public ICaster Caster { get; }
		
		public SpellCast(SpellBehaviour spellBehaviour, 
			ICaster caster,
			Target target)
		{
			SpellBehaviour = spellBehaviour;
			Caster = caster;
			Target = target;
		}

		public void Update(float deltaTime)
		{
			if (SpellCastCancelled)
				return;
			
			if (!SpellCastStarted)
			{
				SpellBehaviour.StartCast(this);
				SpellCastStarted = true;
				OnSpellCastStarted?.Invoke(this);
			}

			CurrentCastTime += deltaTime * Caster.System.AttributeSystem.Haste.CurrentValue;
			SpellBehaviour.UpdateCast(this);
			OnSpellCastProgress?.Invoke(this);

			if (CurrentCastTime >= SpellBehaviour.CastTime)
			{
				SpellBehaviour.CompleteCast(this);
				SpellCastCompleted = true;
				OnSpellCompleted?.Invoke(this);
			}
		}

		public void Cancel(CancelCastReason reason)
		{
			CancelCastReason = reason;
			SpellBehaviour.CancelCast(this);
			SpellCastCancelled = true;
			OnSpellCancelled?.Invoke(this);
		}
	}
}