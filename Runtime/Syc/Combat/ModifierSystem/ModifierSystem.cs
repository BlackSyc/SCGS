using System;
using System.Collections.Generic;
using System.Linq;
using Syc.Combat.ModifierSystem.ScriptableObjects;

namespace Syc.Combat.ModifierSystem
{
	[Serializable]
	public class ModifierSystem : ICombatSubSystem
	{
		public event Action<Modifier> OnModifierAdded;
		public event Action<Modifier> OnModifierRemoved;
		public ICombatSystem System { get; set; }

		private List<Modifier> _activeModifiers = new List<Modifier>();

		public Modifier AddModifier(ModifierBehaviour modifierBehaviour)
		{
			var newModifier = new Modifier(modifierBehaviour);
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