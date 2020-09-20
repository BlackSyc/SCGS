using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects
{
	public class ModifierBehaviour : ScriptableObject
	{
		public float Duration => duration;
		
		[SerializeField]
		protected float duration;

	}
}