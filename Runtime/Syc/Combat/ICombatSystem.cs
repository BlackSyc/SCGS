using System.Collections;
using Syc.Core.System;
using UnityEngine;

namespace Syc.Combat
{
	public interface ICombatSystem
	{
		object Allegiance { get; }
		
		ICombatAttributes AttributeSystem { get; }

		Transform Origin { get; }
		
		bool CanBeTargeted { get; set; }

		T Get<T>();

		bool Has<T>(out T t);

		bool Has<T>();

		Coroutine ExecuteCoroutine(IEnumerator coroutine);

		void AddSubsystem(ISubSystem<ICombatSystem> subSystem);

		void RemoveSubsystem(ISubSystem<ICombatSystem> subSystem);
	}
}