using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Syc.Core.System
{
	public class SystemBase<TSystem> : MonoBehaviour
	{
		private readonly List<ISubSystem<TSystem>> _subsystems = new List<ISubSystem<TSystem>>();

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

		private void Update()
		{
			if (_subsystems == default || !_subsystems.Any())
				return;

			var deltaTime = Time.deltaTime;
			foreach (var subSystem in _subsystems)
			{
				subSystem.Update(deltaTime);
			}
		}

		protected virtual void AddSubsystem(ISubSystem<TSystem> subSystem)
		{
			if (subSystem == null)
				return;

			_subsystems.Add(subSystem);
		}
		
		protected virtual void RemoveSubsystem(ISubSystem<TSystem> subSystem)
		{
			_subsystems.Remove(subSystem);
		}
	}
}