using Syc.Core.Attributes;

namespace Syc.Core
{
	public interface ISettings
	{
		Attribute Get(string setting);
	}
}