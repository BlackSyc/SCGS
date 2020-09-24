using System;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects
{
	[Flags]
	public enum SpellEffectType
	{
		OnStartCast = 1, 
		OnUpdateCast = 2, 
		OnCompleteCast = 4, 
		OnCancelCast = 8, 
		OnImpact = 16, 
		Custom1 = 32, 
		Custom2 = 64, 
		Custom3 = 128
	}
}