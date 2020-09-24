using Syc.Combat.TargetSystem;

namespace Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders
{
	// [CreateAssetMenu(menuName = "SpellState System/TargetType Providers/SelfCastOrigin")]
	// Assets have been created.
	public class Self : TargetProvider
	{
		public override bool HasValidTarget(ICaster caster, out Target target)
		{
			target = new Target(caster.System.Origin.gameObject, caster.System.Origin.position);
			return true;
		}
	}
}