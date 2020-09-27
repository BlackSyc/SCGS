using Syc.Core;
using UnityEngine;

namespace Syc.Combat.TargetSystem
{
	public class Target
	{
		public bool IsCombatTarget => CombatSystem != null;
		
		public GameObject TargetObject { get; }

		public ICombatSystem CombatSystem => TargetObject != null 
		                                     && TargetObject.HasComponent(out ICombatSystem combatSystem)
		                                     && combatSystem.CanBeTargeted 
			? combatSystem
			: default;

		public Vector3 Position => TargetObject == default
			? _originalRelativePosition
			: TargetObject.transform.position + _originalRelativePosition;

		private readonly Vector3 _originalRelativePosition;

		public Target(GameObject targetObject, Vector3 exactWorldPosition)
		{
			TargetObject = targetObject;

			if (TargetObject != default)
				_originalRelativePosition = exactWorldPosition - targetObject.transform.position;
			else
				_originalRelativePosition = exactWorldPosition;
		}
		
		public bool IsFriendlyTo(ICombatSystem combatSystem)
		{
			if (!IsCombatTarget)
				return false;
			
			return combatSystem.Allegiance == CombatSystem.Allegiance;
		}
		
		public bool IsEnemyTo(ICombatSystem combatSystem)
		{ 
			if (!IsCombatTarget)
				return false;
			
			return combatSystem.Allegiance != CombatSystem.Allegiance;
		} 
	}
}