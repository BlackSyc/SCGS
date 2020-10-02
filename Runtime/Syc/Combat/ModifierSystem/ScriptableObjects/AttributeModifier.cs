using System;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Modifiers/Attribute Modifier")]
	public class AttributeModifier : Modifier
	{
		[Header("Attribute Modifier")]
		[Space]
		[SerializeField] protected string attributeName;
		[SerializeField] protected float value;
		[SerializeField] protected AttributeModifierType type;
		[SerializeField] protected bool useModifierAsReference;

		public override void AppliedStack(ModifierState state)
		{
			if (state.Stacks > 1 && !CanStack)
				return;
			
			var attribute = state.Target.AttributeSystem.Get(attributeName);

			switch (type)
			{
				case AttributeModifierType.Add:
					attribute.Add(value, useModifierAsReference ? this : (object) state.Source);
					break;
				case AttributeModifierType.Multiply:
					attribute.Multiply(value, useModifierAsReference ? this : (object) state.Source);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override void RemovedStack(ModifierState state)
		{
			if (state.Stacks > 0 && !CanStack)
				return;
			
			var attribute = state.Target.AttributeSystem.Get(attributeName);

			switch (type)
			{
				case AttributeModifierType.Add:
					attribute.RemoveAddition(useModifierAsReference ? this : (object) state.Source);
					break;
				case AttributeModifierType.Multiply:
					attribute.RemoveMultiplier(useModifierAsReference ? this : (object) state.Source);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public enum AttributeModifierType
	{
		Add,
		Multiply
	}
}