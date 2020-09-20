namespace Syc.Core.System
{
	public interface ISubSystem
	{
		void Update(float deltaTime);
	}
	
	public interface ISubSystem<T> : ISubSystem
	{
		T System { get; set; }
	}
}