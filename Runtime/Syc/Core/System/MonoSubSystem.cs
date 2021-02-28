using MLAPI;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Syc.Core.System
{
	public abstract class MonoSubSystem : MonoBehaviour, ISubSystem
	{
		public virtual void Tick(float deltaTime){}
	}

	public abstract class MonoSubSystem<T> : MonoSubSystem, ISubSystem<T>
	{
		public abstract T System { get; set; }
	}

	public abstract class NetworkedMonoSubSystem : NetworkedBehaviour, ISubSystem
	{
		public virtual void Tick(float deltaTime){}
	}

	public abstract class NetworkedMonoSubsystem<T> : NetworkedMonoSubSystem, ISubSystem<T>
	{
		public abstract T System { get; set; }
	}
}