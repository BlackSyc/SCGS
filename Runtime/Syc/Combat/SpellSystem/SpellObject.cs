using Syc.Combat.SpellSystem.ScriptableObjects;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	public abstract class SpellObject : MonoBehaviour
	{
		public virtual SpellBehaviour Spell { get; set; }
	}
}