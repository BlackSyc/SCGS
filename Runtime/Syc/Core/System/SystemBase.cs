using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Syc.Core.System
{
	public class SystemBase : ISystemBase, ISubSystem
	{
		private readonly List<ISubSystem> _subsystems = new List<ISubSystem>();

		public T Get<T>()
		{
			return _subsystems.OfType<T>().SingleOrDefault();
		}

		public bool Has<T>(out T t)
		{
			t = Get<T>();
			return t != null;
		}

		public bool Has<T>()
		{
			return _subsystems.OfType<T>().Any();
		}

		public void Update()
		{
			if (_subsystems == default || !_subsystems.Any())
				return;

			var deltaTime = Time.deltaTime;
			foreach (var subSystem in _subsystems)
			{
				subSystem.Tick(deltaTime);
			}
		}

		protected void AddSubsystem(ISubSystem subSystem)
		{
			if (subSystem == null)
				return;

			_subsystems.Add(subSystem);
		}
		
		protected void RemoveSubsystem(ISubSystem subSystem)
		{
			_subsystems.Remove(subSystem);
		}

		public virtual void Tick(float deltaTime) { }
	}

	public class SystemBase<T> : SystemBase, ISubSystem<T>
	{
		public T System { get; set; }
	}
}