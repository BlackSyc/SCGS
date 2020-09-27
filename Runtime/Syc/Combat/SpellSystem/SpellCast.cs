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
		public Spell Spell { get; }
		public Target Target { get; }
		public ICaster Caster { get; }
		
		public SpellCast(Spell spell, 
			ICaster caster,
			Target target)
		{
			Spell = spell;
			Caster = caster;
			Target = target;
		}

		public void Start()
		{
			if (SpellCastStarted)
				return;

			Spell.StartCast(this);
			SpellCastStarted = true;
			OnSpellCastStarted?.Invoke(this);

			if (Spell.CastTime > 0)
				return;

			Spell.CompleteCast(this);
			SpellCastCompleted = true;
			OnSpellCompleted?.Invoke(this);
		}
 
		public void Update(float deltaTime)
		{
			if (!SpellCastStarted)
				return;

			if (SpellCastCompleted)
				return;
			
			if (SpellCastCancelled)
				return;

			CurrentCastTime += deltaTime * Caster.System.AttributeSystem.Haste.Remap();
			Spell.UpdateCast(this);
			OnSpellCastProgress?.Invoke(this);

			if (CurrentCastTime >= Spell.CastTime)
			{
				Spell.CompleteCast(this);
				SpellCastCompleted = true;
				OnSpellCompleted?.Invoke(this);
			}
		}

		public void Cancel(CancelCastReason reason)
		{
			CancelCastReason = reason;
			Spell.CancelCast(this);
			SpellCastCancelled = true;
			OnSpellCancelled?.Invoke(this);
		}
	}
}