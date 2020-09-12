using System;
using UnityEngine;

namespace Syc.Game.LocalPlayer
{
	[Serializable]
	public class InputSystem : MonoBehaviour
	{
		public event Action<Vector2> OnMove;

		public event Action<Vector2> OnLook;

		public event Action OnJump;

		public event Action<int> OnCastSpell;
		
	}
}