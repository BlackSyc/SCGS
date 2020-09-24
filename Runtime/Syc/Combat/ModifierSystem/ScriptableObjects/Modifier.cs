using UnityEngine;
using UnityEngine.UI;

namespace Syc.Combat.ModifierSystem.ScriptableObjects
{
	[CreateAssetMenu(menuName = "SpellState System/")]
	public class Modifier : ScriptableObject
	{
		public string ModifierName => modifierName;
		public string ModifierDescription => modifierDescription;
		public Image Icon => icon;
		public float Duration => duration;

		[SerializeField] protected string modifierName;

		[SerializeField] protected string modifierDescription;
		
		[SerializeField] protected Image icon;

		[SerializeField] protected float duration;
	}
}