namespace Syc.Combat.TargetSystem
{
	public interface ITargetManager : ICombatSubSystem
	{
		Target GetCurrentTarget();
		void LockTarget(Target target);
		
	}
}