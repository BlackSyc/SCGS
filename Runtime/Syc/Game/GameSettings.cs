using System;
using System.Collections.Generic;
using Syc.Core;
using UnityEngine;
using Attribute = Syc.Core.Attributes.Attribute;

namespace Syc.Game
{
	public class GameSettings : MonoBehaviour, ISettings
	{
		public static GameSettings Instance { get; private set; }
		
		[SerializeField] private Attribute mouseSensitivity;

		private Dictionary<string, Attribute> _settings = new Dictionary<string, Attribute>();

		private void Awake()
		{
			if (Instance)
				throw new Exception("There is more than one Settings object in the game!");

			Instance = this;
			
			_settings.Add(nameof(mouseSensitivity), mouseSensitivity);
		}

		public Attribute Get(string setting)
		{
			return _settings[setting];
		}
	}
}