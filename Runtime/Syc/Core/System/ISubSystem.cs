namespace Syc.Core.System
{
	public interface ISubSystem
	{
		void Update(float deltaTime);
	}
	
	public interface ISubSystem<T>
	{
		T System { get; set; }
		void Update(float deltaTime);
	}


}