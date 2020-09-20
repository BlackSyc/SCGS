namespace Syc.Core.System
{
	public interface ISystemBase
	{
		T Get<T>();

		bool Has<T>(out T t);

		bool Has<T>();
	}
}