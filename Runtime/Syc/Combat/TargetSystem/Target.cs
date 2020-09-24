using UnityEngine;

namespace Syc.Combat.TargetSystem
{
	public class Target
	{
		public bool IsCombatTarget => TargetObject != null && TargetObject.GetComponent<ICombatSystem>() != null;
		
		public GameObject TargetObject { get; }

		public ICombatSystem CombatSystem => TargetObject.GetComponent<ICombatSystem>();

		public Vector3 Position => TargetObject == default
			? _originalRelativePosition
			: TargetObject.transform.position + _originalRelativePosition;

		private Vector3 _originalRelativePosition;

		public Target(GameObject targetObject, Vector3 exactWorldPosition)
		{
			TargetObject = targetObject;
			_originalRelativePosition = exactWorldPosition - targetObject.transform.position;
		}
		
		public bool IsFriendlyTo(ICombatSystem combatSystem)
		{
			if (!IsCombatTarget)
				return false;
			
			return combatSystem.Allegiance == TargetObject.GetComponent<ICombatSystem>().Allegiance;
		}
		
		public bool IsEnemyTo(ICombatSystem combatSystem) => !IsFriendlyTo(combatSystem);
	}
}