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

		public override bool HasValidTarget(ICaster caster, out Target target)
		{
			target = default;
			
			if (!caster.System.Has(out ITargetManager targetManager))
				return false;
			
			target = targetManager.CreateTarget();
			
			if (AllowNonCombatTargets && !target.IsCombatTarget)
				return true;

			if (AllowEnemyTargets && target.IsEnemyTo(caster.System))
				return true;

			return AllowFriendlyTargets && target.IsFriendlyTo(caster.System);
		}
	}
}