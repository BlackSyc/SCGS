using System;
using System.Collections.Generic;
using System.Linq;
using Syc.Combat.ModifierSystem.ScriptableObjects;

namespace Syc.Combat.ModifierSystem
{
	[Serializable]
	public class ModifierSystem : ICombatSubSystem
	{
		public event Action<ModifierState> OnModifierAdded;
		public event Action<ModifierState> OnModifierRemoved;
		public ICombatSystem System { get; set; }

		private List<ModifierState> _activeModifiers = new List<ModifierState>();

		public ModifierState AddModifier(Modifier modifier)
		{
			var newModifier = new ModifierState(modifier);
			_activeModifiers.Add(newModifier);
			OnModifierAdded?.Invoke(newModifier);
			return newModifier;
		}

		public void Tick(float deltaTime)
		{
			foreach (var activeModifier in _activeModifiers)
			{
				activeModifier.Update(deltaTime);
			}

			var completeModifiers = _activeModifiers.Where(x => x.TimeIsElapsed);

			foreach (var completeModifier in completeModifiers)
			{
				_activeModifiers.Remove(completeModifier);
				OnModifierRemoved?.Invoke(completeModifier);
			}
		}
	}
}