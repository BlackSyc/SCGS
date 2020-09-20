using System;
using System.Linq;
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
			var hits = Physics.RaycastAll(playerCamera.position, playerCamera.forward, maxRange);
			if (!hits.Any())
			{
				return new TemporaryTarget(playerCamera.position + maxRange * playerCamera.forward);
			}

			var hit = hits.First(x =>
				x.transform.GetComponent<ICombatSystem>() == default ||
				x.transform.GetComponent<ICombatSystem>().CanBeTargeted);
			return new Target(hit.transform.gameObject);

		}

		public void LockTarget(Target target)
		{
			throw new NotImplementedException();
		}

		public ICombatSystem System { get; set; }
		public void Tick(float deltaTime) { }
	}
}