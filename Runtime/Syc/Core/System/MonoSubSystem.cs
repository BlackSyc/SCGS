using UnityEngine;

namespace Syc.Core.System
{
	public abstract class MonoSubSystem : MonoBehaviour, ISubSystem
	{
		public abstract void Tick(float deltaTime);
	}

	public abstract class MonoSubSystem<T> : MonoSubSystem, ISubSystem<T>
	{
		public abstract T System { get; set; }
	}
}