using Syc.Core.Attributes;

namespace Syc.Core
{
	public class Settings
	{
		public static Attribute MouseSensitivity { get; set; } = new Attribute(baseValue: 1, remapMultiplier: 1, remapExponent: 1, remapAddition: 0);
	}
}