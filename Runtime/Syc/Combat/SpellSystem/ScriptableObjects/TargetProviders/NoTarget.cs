using Syc.Combat.TargetSystem;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	// [CreateAssetMenu(menuName = "SpellState System/TargetType Providers/No target")]
	// Assets have been created.
	public class NoTarget : TargetProvider
	{
		public override Target CreateTarget(ICaster caster) => default;
	}
}