using UnityEngine;

namespace Syc.Combat
{
	public interface ICombatSystem
	{
		object Allegiance { get; }
		
		ICombatAttributes AttributeSystem { get; }

		Transform Origin { get; }

		T Get<T>();

		bool Has<T>(out T t);

		bool Has<T>();
	}
}