using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	// [CreateAssetMenu(menuName = "SpellState System/TargetType Providers/Single or default target")]
	// Assets have been created.
	public class SingleOrDefaultTarget : TargetProvider
	{
		[SerializeField] private bool allowFriendlyTargets;
		[SerializeField] private bool allowEnemyTargets;
		
		[SerializeField] private bool allowNonCombatTargets;

		public bool AllowFriendlyTargets => allowFriendlyTargets;
		public bool AllowEnemyTargets => allowEnemyTargets;
		public bool AllowNonCombatTargets => allowNonCombatTargets;
		
		public override Target CreateTarget(ICaster caster)
		{
			if (!caster.System.Has(out ITargetManager targetManager))
				return default;
			
			var currentTarget = targetManager.CreateTarget();

			if (AllowNonCombatTargets && !currentTarget.IsCombatTarget)
			{
				targetManager.LockTarget(currentTarget);
				return currentTarget;
			}

			if (AllowEnemyTargets && currentTarget.IsEnemyTo(caster.System))
			{
				targetManager.LockTarget(currentTarget);
				return currentTarget;
			}

			if (AllowFriendlyTargets && currentTarget.IsFriendlyTo(caster.System))
			{
				targetManager.LockTarget(currentTarget);
				return currentTarget;
			}

			return currentTarget;
		}
	}
}