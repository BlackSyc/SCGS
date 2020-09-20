using System;
using Syc.Core;
using UnityEngine;
using Attribute = Syc.Core.Attributes.Attribute;

namespace Syc.Game
{
	public class GameSettings : MonoBehaviour, ISettings
	{
		public static GameSettings Instance { get; private set; }
		
		[SerializeField] private Attribute mouseSensitivity;

		private void Awake()
		{
			if (Instance)
				throw new Exception("There is more than one Settings object in the game!");

			Instance = this;
		}

		public Attribute MouseSensitivity => mouseSensitivity;
	}
}