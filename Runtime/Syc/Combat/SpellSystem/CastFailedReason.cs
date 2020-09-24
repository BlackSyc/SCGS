namespace Syc.Combat.SpellSystem
{
	public enum CastFailedReason
	{
		InvalidTarget,
		OnCoolDown,
		OnGlobalCoolDown,
		AlreadyCasting,
		SpellNotFound,
		Custom
	}
}