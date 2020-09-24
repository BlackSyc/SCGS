namespace Syc.Combat.TargetSystem
{
	public interface ITargetManager : ICombatSubSystem
	{
		Target CreateTarget();
	}
}