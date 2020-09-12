using Syc.Combat.TargetSystem;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	// [CreateAssetMenu(menuName = "Spell System/Target Providers/No target")]
	// Assets have been created.
	public class NoTarget : TargetProvider
	{
		public override Target GetTarget(ICaster caster) => default;
	}
}