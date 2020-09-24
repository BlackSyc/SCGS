using Syc.Combat.TargetSystem;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	// [CreateAssetMenu(menuName = "SpellState System/TargetType Providers/SelfCastOrigin")]
	// Assets have been created.
	public class Self : TargetProvider
	{
		public override Target CreateTarget(ICaster caster) => new Target(caster.System.Origin.gameObject);
	}
}