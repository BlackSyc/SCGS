using Syc.Combat.TargetSystem;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	// [CreateAssetMenu(menuName = "SpellState System/TargetType Providers/No target")]
	// Assets have been created.
	public class NoTarget : TargetProvider
	{
		public override bool HasValidTarget(ICaster caster, out Target target)
		{
			target = default;
			return true;
		}
	}
}