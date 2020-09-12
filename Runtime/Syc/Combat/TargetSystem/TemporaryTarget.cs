using UnityEngine;

namespace Syc.Combat.TargetSystem
{
	public class TemporaryTarget : Target
	{
		public TemporaryTarget(Vector3 position) 
			: base(Object.Instantiate(new GameObject(), position, Quaternion.identity)){ }
		
		~TemporaryTarget()
		{
			Object.Destroy(GameObject);
		}
	}
}