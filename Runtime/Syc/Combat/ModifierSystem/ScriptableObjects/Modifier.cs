using System.Collections.Generic;
using System.Linq;
using Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Modifiers/Modifier")]
	public class Modifier : ScriptableObject
	{
		public string ModifierName => modifierName;
		public string ModifierDescription => modifierDescription;
		public Sprite Icon => icon;
		public float Duration => duration;

		[SerializeField] protected string modifierName;
		[SerializeField] protected string modifierDescription;
		[SerializeField] protected Sprite icon;
		[SerializeField] protected float duration;
		[SerializeField] protected List<ModifierEffect> modifierEffects;
		
		public void ExecuteAll(ModifierEffectType modifierEffectType, ICaster source, ICombatSystem target, object referenceObject, float elapsedTime)
		{
			foreach (var effect in modifierEffects.Where(x => x.Types.HasFlag(modifierEffectType)))
			{
				effect.Execute(source, target, this, referenceObject, elapsedTime);
			}
		}
	}
}