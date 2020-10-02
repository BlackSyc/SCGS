using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects
{
	public abstract class Modifier : ScriptableObject
	{
		public string ModifierName => modifierName;
		public string ModifierDescription => modifierDescription;
		public Sprite Icon => icon;
		public float Duration => duration;
		public int StackLimit => stackLimit;

		[SerializeField] protected string modifierName;
		[TextArea(3,10)]
		[SerializeField] protected string modifierDescription;
		[SerializeField] protected Sprite icon;
		[SerializeField] protected float duration;
		[SerializeField] protected int stackLimit;

		public virtual ModifierState CreateState(ICaster source, ICombatSystem target,  object referenceObject)
		{
			return new ModifierState(source, target, this, referenceObject);
		}

		public virtual void Applied(ModifierState state){}

		public virtual void AppliedStack(ModifierState state){}

		public virtual void Removed(ModifierState state){}

		public virtual void RemovedStack(ModifierState state){}
	}
}