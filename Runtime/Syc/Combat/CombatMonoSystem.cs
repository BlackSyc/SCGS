using System.Collections;
using Syc.Core.System;
using UnityEngine;

namespace Syc.Combat
{
	public abstract class CombatMonoSystem : MonoSystemBase, ICombatSystem
	{
		public abstract object Allegiance { get; }
		public abstract ICombatAttributes AttributeSystem { get; }
		public abstract Transform Origin { get; }
		public abstract bool CanBeTargeted { get; set; }
		public Coroutine ExecuteCoroutine(IEnumerator coroutine)
		{
			return StartCoroutine(coroutine);
		}

		public void AddSubsystem(ISubSystem<ICombatSystem> subSystem)
		{
			base.AddSubsystem(subSystem);
			subSystem.System = this;
		}

		public void RemoveSubsystem(ISubSystem<ICombatSystem> subSystem)
		{
			base.RemoveSubsystem(subSystem);
			subSystem.System = default;
		}
	}
}