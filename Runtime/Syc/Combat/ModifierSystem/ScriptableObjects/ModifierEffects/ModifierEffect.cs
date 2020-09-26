using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects
{
	public abstract class ModifierEffect : ScriptableObject
	{
		public ModifierEffectType Types => types;
		
		[SerializeField]
		protected ModifierEffectType types;

		public abstract void Execute(ICaster source, ICombatSystem target, Modifier modifier, object referenceObject, float elapsedTime);
	}
}