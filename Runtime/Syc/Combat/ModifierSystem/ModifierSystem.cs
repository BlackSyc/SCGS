using System;
using System.Collections.Generic;
using System.Linq;
using Syc.Combat.ModifierSystem.ScriptableObjects;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem
{
	[Serializable]
	public class ModifierSystem : ICombatSubSystem
	{
		public event Action<ModifierState> OnModifierAdded;
		public event Action<ModifierState> OnModifierRemoved;
		public ICombatSystem System { get; set; }

		private List<ModifierState> _activeModifiers = new List<ModifierState>();

		public ModifierState GetModifier(Modifier modifier, object referenceObject)
		{
			return _activeModifiers
				.FirstOrDefault(x => x.ModifierType == modifier && x.ReferenceObject == referenceObject);
		}

		public ModifierState AddModifier(Modifier modifier, ICaster source, object referenceObject)
		{
			var activeModifier = GetModifier(modifier, referenceObject);
			
			if (activeModifier != null)
			{
				if (!modifier.CanStack)
					return activeModifier;
				
				activeModifier.AddStack();
				return activeModifier;
			}
			var newModifier = new ModifierState(source, System, modifier, referenceObject);
			_activeModifiers.Add(newModifier);
			OnModifierAdded?.Invoke(newModifier);
			newModifier.InvokeOnApply();
			return newModifier;
		}

		public void RemoveModifier(Modifier modifier, object referenceObject)
		{
			var activeModifier = _activeModifiers
				.FirstOrDefault(x => x.ModifierType == modifier && x.ReferenceObject == referenceObject);
			
			if (activeModifier == null)
				return;
			
			activeModifier.RemoveStack();
			
			if (activeModifier.Stacks >= 1)
				return;

			_activeModifiers.Remove(activeModifier);
			OnModifierRemoved?.Invoke(activeModifier);
		}
		

		public void Tick(float deltaTime)
		{
			foreach (var activeModifier in _activeModifiers)
			{
				activeModifier.Update(deltaTime);
			}

			var completeModifiers =  _activeModifiers
				.Where(x => x.Stacks < 1)
				.ToList();

			foreach (var completeModifier in completeModifiers)
			{
				_activeModifiers.Remove(completeModifier);
				OnModifierRemoved?.Invoke(completeModifier);
			}
		}
	}
}