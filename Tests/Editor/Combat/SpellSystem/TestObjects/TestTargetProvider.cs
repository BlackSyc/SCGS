using Syc.Combat.SpellSystem;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using Syc.Combat.TargetSystem;

namespace Tests.Editor.Combat.SpellSystem
{
	public class TestTargetProvider : TargetProvider
	{
		public override Target CreateTarget(ICaster caster)
		{
			throw new System.NotImplementedException();
		}
	}
}