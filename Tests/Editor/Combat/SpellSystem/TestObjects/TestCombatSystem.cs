using Syc.Combat;
using Syc.Core.System;
using UnityEngine;

namespace Tests.Editor.Combat.SpellSystem.TestObjects
{
	public class TestCombatSystem : CombatSystem
	{
		public override object Allegiance { get; }
		public override ICombatAttributes AttributeSystem { get; }
		public override Transform Origin { get; }

		public TestCombatSystem(object allegiance, ICombatAttributes attributeSystem, Transform origin)
		{
			Allegiance = allegiance;
			AttributeSystem = attributeSystem;
			Origin = origin;
		}

		public void AddSubsystem(ISubSystem<ICombatSystem> subSystem)
		{
			base.AddSubsystem(subSystem);
		}

		public void RemoveSubsystem(ISubSystem<ICombatSystem> subSystem)
		{
			base.RemoveSubsystem(subSystem);
		}
	}
}