using UnityEngine;

namespace Syc.Core
{
	public static class GameObjectExtensions
	{
		public static bool HasComponent<T>(this GameObject gameObject, out T component)
		{
			component = gameObject.GetComponent<T>();
			return component != null;
		}
	}
}