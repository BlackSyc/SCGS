using System;
using Syc.Combat;
using Attribute = Syc.Core.Attributes.Attribute;

namespace Tests.Editor.Combat.SpellSystem.TestObjects
{
	public class TestAttributesSystem : ICombatAttributes
	{
		public Attribute Stamina { get; }
		public Attribute SpellPower { get; }
		public Attribute Haste { get; }
		public Attribute CriticalStrikeRating { get; }
		public Attribute Armor { get; }

		public TestAttributesSystem(Attribute stamina, Attribute spellPower, Attribute haste, Attribute criticalStrikeRating, Attribute armor)
		{
			Stamina = stamina;
			SpellPower = spellPower;
			Haste = haste;
			CriticalStrikeRating = criticalStrikeRating;
			Armor = armor;
		}

		public TestAttributesSystem()
		{
			Stamina = new Attribute();
			SpellPower = new Attribute();
			Haste = new Attribute();
			CriticalStrikeRating = new Attribute();
			Armor = new Attribute();
		}

		public Attribute Get(string attributeName)
		{
			throw new NotImplementedException();
		}
	}
}