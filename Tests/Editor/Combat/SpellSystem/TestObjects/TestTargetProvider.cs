using Syc.Combat.SpellSystem;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using Syc.Combat.TargetSystem;

namespace Tests.Editor.Combat.SpellSystem.TestObjects
{
	public class TestTargetProvider : TargetProvider
	{
		public override bool HasValidTarget(ICaster caster, out Target target)
		{
			throw new System.NotImplementedException();
		}
	}
}