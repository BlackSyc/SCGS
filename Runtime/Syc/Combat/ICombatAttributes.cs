
using Syc.Core.Attributes;

namespace Syc.Combat
{
	public interface ICombatAttributes : IAttributes
	{
		Attribute Stamina { get; }

		Attribute SpellPower { get; }

		Attribute Haste { get; }

		Attribute CriticalStrikeRating { get; }

		Attribute Armor { get; }
	}
}