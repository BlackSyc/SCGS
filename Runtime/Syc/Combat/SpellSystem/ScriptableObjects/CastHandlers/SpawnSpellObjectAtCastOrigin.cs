using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.CastHandlers
{
	[CreateAssetMenu(menuName = "Spell System/Cast Handlers/Spawn spell object at cast origin")]
	public class SpawnSpellObjectAtCastOrigin : CastHandler
	{
		[SerializeField] 
		private SpellObject spellObjectPrefab;

		public override void Handle(SpellCast spellCast)
		{
			Instantiate(spellObjectPrefab, spellCast.Caster.CastOrigin).Spell = spellCast.SpellBehaviour;
		}
	}
}