using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Syc.Core.System
{
	public class MonoSystemBase : MonoSubSystem, ISystemBase
	{
		[SerializeField]
		private List<ISubSystem> subsystems = new List<ISubSystem>();

		public T Get<T>()
		{
			return subsystems.OfType<T>().SingleOrDefault();
		}

		public bool Has<T>(out T t)
		{
			t = Get<T>();
			return t != null;
		}

		public bool Has<T>()
		{
			return subsystems.OfType<T>().Any();
		}

		private void Update()
		{
			if (subsystems == default || !subsystems.Any())
				return;

			var deltaTime = Time.deltaTime;
			foreach (var subSystem in subsystems)
			{
				subSystem.Tick(deltaTime);
			}
		}

		protected void AddSubsystem(ISubSystem subSystem)
		{
			if (subSystem == null)
				return;

			subsystems.Add(subSystem);
		}
		
		protected void RemoveSubsystem(ISubSystem subSystem)
		{
			subsystems.Remove(subSystem);
		}

		public override void Tick(float deltaTime) { }
	}

	public class MonoSystemBase<T> : MonoSystemBase, ISubSystem<T>
	{
		public T System { get; set; }
	}
}