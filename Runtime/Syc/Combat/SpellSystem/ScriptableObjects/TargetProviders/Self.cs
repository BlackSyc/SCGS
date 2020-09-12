using Syc.Combat.TargetSystem;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	// [CreateAssetMenu(menuName = "Spell System/Target Providers/Self")]
	// Assets have been created.
	public class Self : TargetProvider
	{
		public override Target GetTarget(ICaster caster) => new Target(caster.System.Origin.gameObject);
	}
}