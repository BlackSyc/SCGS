namespace Syc.Combat.TargetSystem
{
	public interface ITargetManager : ICombatSubSystem
	{
		Target CreateTarget();
		void LockTarget(Target target);
		
	}
}