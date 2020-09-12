using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.CastHandlers
{
	[CreateAssetMenu(menuName = "Spell System/Cast Handlers/Spawn spell object at caster origin")]
	public class SpawnSpellObjectAtCasterOrigin : CastHandler
	{
		[SerializeField] 
		private SpellObject spellObjectPrefab;
		
		public override void Handle(SpellCast spellCast)
		{
			Instantiate(spellObjectPrefab, spellCast.Caster.System.Origin).Spell = spellCast.SpellBehaviour;
		}
	}
}