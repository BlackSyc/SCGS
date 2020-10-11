using Syc.Combat.SpellSystem.ScriptableObjects;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem
{
	public class SpellObject : MonoBehaviour
	{
		public ICaster Source { get; set; }
		public Target Target { get; set; }
		public SpellCast SpellCast { get; set; }
		public Spell Spell { get; set; }

		public virtual void Initialize()
		{
			
		}
	}
}