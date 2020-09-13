using Syc.Core;
using Syc.Core.Attributes;
using UnityEngine;

namespace Syc.Game
{
	public class GameSettings : MonoBehaviour
	{
		[SerializeField] private Attribute mouseSensitivity;

		private void Awake()
		{
			Settings.MouseSensitivity = mouseSensitivity;
		}
	}
}