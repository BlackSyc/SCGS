using System;
using UnityEngine;

namespace Syc.Combat.Auras.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Spell System/Auras/Attribute Aura")]
	public class AttributeAura : Aura
	{
		[Header("Attribute Aura")]
		[Space]
		[SerializeField] protected string attributeName;
		[SerializeField] protected float value;
		[SerializeField] protected AttributeAuraType type;
		[SerializeField] protected bool useAuraAsReference;

		public override void AppliedStack(AuraState state)
		{
			var attribute = state.Target.AttributeSystem.Get(attributeName);

			switch (type)
			{
				case AttributeAuraType.Add:
					attribute.Add(value, useAuraAsReference ? this : (object) state.Source);
					break;
				case AttributeAuraType.Multiply:
					attribute.Multiply(value, useAuraAsReference ? this : (object) state.Source);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override void RemovedStack(AuraState state)
		{
			var attribute = state.Target.AttributeSystem.Get(attributeName);

			switch (type)
			{
				case AttributeAuraType.Add:
					attribute.RemoveAddition(useAuraAsReference ? this : (object) state.Source);
					break;
				case AttributeAuraType.Multiply:
					attribute.RemoveMultiplier(useAuraAsReference ? this : (object) state.Source);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public enum AttributeAuraType
	{
		Add,
		Multiply
	}
}