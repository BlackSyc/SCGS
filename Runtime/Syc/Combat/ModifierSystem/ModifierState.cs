using System;
using System.Collections.Generic;
using System.Linq;
using Syc.Combat.ModifierSystem.ScriptableObjects;
using Syc.Combat.ModifierSystem.ScriptableObjects.ModifierEffects;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem
{
	[Serializable]
	public class ModifierState
	{
		public event Action<int> OnStackAdded;

		public event Action<int> OnStackRemoved;
		
		public Modifier ModifierType { get; }
		public int Stacks => _modifierStack.Count;

		public object ReferenceObject => _referenceObject;

		// A list of the 'current time'(s) that a stack was added.
		private readonly List<float> _modifierStack = new List<float>();

		private readonly object _referenceObject;
		private readonly ICaster _source;
		private readonly ICombatSystem _target;

		public ModifierState(ICaster source, ICombatSystem target, Modifier modifier, object referenceObject)
		{
			ModifierType = modifier;
			_modifierStack.Add(Time.time);
			_source = source;
			_referenceObject = referenceObject;
			_target = target;
		}

		public float TimeRemaining => ModifierType.Duration - (Time.time - _modifierStack.Max());

		public void AddStack()
		{
			var currentTime = Time.time;
			_modifierStack.Add(currentTime);
			ModifierType.ExecuteAll(ModifierEffectType.OnApplyStack, _source, _target, _referenceObject, currentTime);
			OnStackAdded?.Invoke(_modifierStack.Count);
		}

		public void RemoveStack()
		{
			ModifierType.ExecuteAll(ModifierEffectType.OnRemoveStack, _source, _target, _referenceObject, _modifierStack[0]);
			_modifierStack.RemoveAt(0);
			OnStackRemoved?.Invoke(_modifierStack.Count);
		}

		public void Update(float _)
		{
			_modifierStack.RemoveAll(x =>
			{
				if (Time.time - x < ModifierType.Duration)
					return false;
				
				ModifierType.ExecuteAll(ModifierEffectType.OnRemoveStack, _source, _target, _referenceObject, x);
				return true;

			});
			foreach (var modifier in _modifierStack)
			{
				ModifierType.ExecuteAll(ModifierEffectType.OnUpdate, _source, _target, _referenceObject, modifier);
			}
		}

		public void InvokeOnApply()
		{
			ModifierType.ExecuteAll(ModifierEffectType.OnApply, _source, _target, _referenceObject, Time.time);
		}

		public void InvokeOnRemove()
		{
			ModifierType.ExecuteAll(ModifierEffectType.OnRemove, _source, _target, _referenceObject, Time.time);
		}

		public void ResetDurationFirst()
		{
			if (!_modifierStack.Any())
			{
				_modifierStack.Add(Time.time);
			}
			else
			{
				_modifierStack[0] = Time.time;
			}
		}

		public void ResetDurationAll()
		{
			if (!_modifierStack.Any())
				return;

			for (var i = 0; i < _modifierStack.Count; i++)
			{
				_modifierStack[i] = Time.time;
			}
		}
	}
}