using UnityEngine;

namespace Syc.Combat.TargetSystem
{
	public class Target
	{
		public bool IsCombatTarget => GameObject.GetComponent<ICombatSystem>() != null;
		
		public GameObject GameObject { get; }

		public ICombatSystem CombatSystem => GameObject.GetComponent<ICombatSystem>();

		public Target(GameObject gameObject)
		{
			GameObject = gameObject;
		}
		
		public bool IsFriendlyTo(ICombatSystem combatSystem)
		{
			if (!IsCombatTarget)
				return false;
			
			return combatSystem.Allegiance == GameObject.GetComponent<ICombatSystem>().Allegiance;
		}
		
		public bool IsEnemyTo(ICombatSystem combatSystem) => !IsFriendlyTo(combatSystem);
	}
}