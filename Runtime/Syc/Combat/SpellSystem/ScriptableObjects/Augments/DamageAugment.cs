using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.Augments
{
	[CreateAssetMenu(menuName = "Spell System/Augments/Damage Augment")]
	public class DamageAugment : Augment
	{
		public float MultiplyBaseDamage => multiplyBaseDamage;

		public float AddCritChance => addCritChance;

		[SerializeField] private float multiplyBaseDamage = 1;
		
		[SerializeField] private float addCritChance;

	}
}