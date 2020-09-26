using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects.Attributes
{
	[CreateAssetMenu(menuName = "Spell System/Modifiers/Effects/Remove Attribute Multiplication")]

	public class AttributeRemoveMultiplication : ModifierEffect
	{
		[SerializeField] private string attributeName;
		[SerializeField] private bool useModifierAsSource = true;
		
		public override void Execute(ICaster source, ICombatSystem target, Modifier modifier, object referenceObject, float elapsedTime)
		{
			referenceObject = useModifierAsSource 
				? modifier 
				: referenceObject;
			
			target.AttributeSystem.Get(attributeName)?.RemoveMultiplier(referenceObject);
		}
	}
}