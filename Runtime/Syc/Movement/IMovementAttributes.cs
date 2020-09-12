using Syc.Core.Attributes;

namespace Syc.Movement
{
	public interface IMovementAttributes
	{
		Attribute MovementSpeed { get; }
		
		Attribute RotationSpeed { get; }
		
		Attribute JumpPower { get; }
	}
}
