namespace Syc.Core.System
{
	public interface ISubSystem
	{
		void Tick(float deltaTime);
	}
	
	public interface ISubSystem<T> : ISubSystem
	{
		T System { get; set; }
	}
}