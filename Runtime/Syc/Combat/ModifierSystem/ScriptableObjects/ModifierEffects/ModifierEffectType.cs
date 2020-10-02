﻿using System;

namespace Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects
{
	[Flags]
	public enum ModifierEffectType
	{
		OnApply = 1,
		OnApplyStack = 2,
		OnUpdate = 4,
		OnRemove = 8,
		OnRemoveStack = 16,
		Custom1 = 32,
		Custom2 = 64,
		Custom3 = 128
	}
}