using Syc.Core.System;
using UnityEngine;

namespace Syc.Combat
{
	public abstract class CombatSystem : SystemBase<ICombatSystem>, ICombatSystem
	{
		public abstract object Allegiance { get; }
		public abstract ICombatAttributes AttributeSystem { get; }
		public abstract Transform Origin { get; }

		protected override void AddSubsystem(ISubSystem<ICombatSystem> subSystem)
		{
			base.AddSubsystem(subSystem);
			subSystem.System = this;
		}

		protected override void RemoveSubsystem(ISubSystem<ICombatSystem> subSystem)
		{
			base.RemoveSubsystem(subSystem);
			subSystem.System = default;
		}
	}
}