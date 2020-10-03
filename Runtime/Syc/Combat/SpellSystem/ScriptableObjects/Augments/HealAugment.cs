using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.Augments
{
	[CreateAssetMenu(menuName = "Spell System/Augments/Heal Augment")]

	public class HealAugment : Augment
	{
		public float MultiplyBaseHeal => multiplyBaseHeal;

		public float AddCritChance => addCritChance;

		[SerializeField] private float multiplyBaseHeal = 1;
		
		[SerializeField] private float addCritChance;

	}
}