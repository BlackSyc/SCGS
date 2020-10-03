using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.Auras.ScriptableObjects
{
	public abstract class Aura : ScriptableObject
	{
		public string Name => name;
		public string Description => description;
		public Sprite Icon => icon;
		public float Duration => duration;
		public int StackLimit => stackLimit;

		[SerializeField] protected string name;
		[TextArea(3,10)]
		[SerializeField] protected string description;
		[SerializeField] protected Sprite icon;
		[SerializeField] protected float duration;
		[SerializeField] protected int stackLimit;

		public virtual AuraState CreateState(ICaster source, ICombatSystem target,  object referenceObject)
		{
			return new AuraState(source, target, this, referenceObject);
		}

		public virtual void Applied(AuraState state){}

		public virtual void AppliedStack(AuraState state){}

		public virtual void Removed(AuraState state){}

		public virtual void RemovedStack(AuraState state){}
	}
}