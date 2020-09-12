namespace Syc.Combat.SpellSystem.ScriptableObjects.CastHandlers
{
	public class DirectSpellHit : CastHandler
	{
		public override void Handle(SpellCast spellCast)
		{
			spellCast.SpellBehaviour.SpellHit(spellCast.Caster, spellCast.Target);
		}
	}
}