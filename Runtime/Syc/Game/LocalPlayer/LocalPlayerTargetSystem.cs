using System;
using Syc.Combat;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Game.LocalPlayer
{
	[Serializable]
	public class LocalPlayerTargetSystem : ITargetManager
	{

		[SerializeField] 
		private Transform playerCamera;

		[SerializeField] private float maxRange;

		public Target GetCurrentTarget()
		{
			if (Physics.Raycast(playerCamera.position, playerCamera.forward, out var hit, maxRange))
			{
				return new Target(hit.transform.gameObject);
			}
			
			return new TemporaryTarget(playerCamera.position + maxRange * playerCamera.forward);
		}

		public void LockTarget(Target target)
		{
			throw new NotImplementedException();
		}

		public ICombatSystem System { get; set; }
		public void Update(float deltaTime) { }
	}
}