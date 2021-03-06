﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Syc.Core.System
{
	public class MonoSystemBase : MonoSubSystem, ISystemBase
	{
		private List<ISubSystem> _subsystems = new List<ISubSystem>();

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
	}

	public class MonoSystemBase<T> : MonoSystemBase, ISubSystem<T>
	{
		public T System { get; set; }
	}

	public class NetworkedMonoSystemBase : NetworkedMonoSubSystem, ISystemBase
	{
		private List<ISubSystem> _subsystems = new List<ISubSystem>();

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
	}
	
	public class NetworkedMonoSystemBase<T> : NetworkedMonoSystemBase, ISubSystem<T>
	{
		public T System { get; set; }
	}
}