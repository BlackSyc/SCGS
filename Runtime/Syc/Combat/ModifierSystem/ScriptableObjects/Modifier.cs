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
		public bool CanStack => canStack;

		[SerializeField] protected string modifierName;
		[TextArea(3,10)]
		[SerializeField] protected string modifierDescription;
		[SerializeField] protected Sprite icon;
		[SerializeField] protected float duration;
		[SerializeField] protected bool canStack;

		public virtual ModifierState CreateState(ICaster source, ICombatSystem target,  object referenceObject)
		{
			return new ModifierState(source, target, this, referenceObject);
		}

		public abstract void Applied(ModifierState state);

		public abstract void Removed(ModifierState state);
	}
}