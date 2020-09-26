using System;

namespace Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects
{
	[Flags]
	public enum ModifierEffectType
	{
		OnApply,
		OnApplyStack,
		OnUpdate,
		OnRemove,
		OnRemoveStack,
		Custom1,
		Custom2,
		Custom3
	}
}