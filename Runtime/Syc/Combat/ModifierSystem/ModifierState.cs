﻿using System;
using System.Collections;
using Syc.Combat.ModifierSystem.ScriptableObjects;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Combat.ModifierSystem
{
	[Serializable]
	public class ModifierState
	{
		public event Action<int> OnStackAdded;
		public bool HasExpired => ElapsedTime > ModifierType.Duration;
		public Modifier ModifierType { get; }
		public int Stacks { get; set; }
		public float ElapsedTime { get; set; }
		public object ReferenceObject { get; }
		public ICaster Source { get; }
		public ICombatSystem Target { get; }
		
		public bool CoroutineShouldStop { get; set; }

		public ModifierState(ICaster source, ICombatSystem target, Modifier modifier, object referenceObject)
		{
			ModifierType = modifier;
			Source = source;
			ReferenceObject = referenceObject;
			Target = target;
		}

		public void StartCoroutine(IEnumerator coroutine)
		{
			Target.ExecuteCoroutine(coroutine);
		}
		
		public void AddStack()
		{
			Stacks += 1;
			ElapsedTime = 0;
			ModifierType.AppliedStack(this);
			OnStackAdded?.Invoke(Stacks);
		}

		public void ResetDuration()
		{
			ElapsedTime = 0;
		}

		public void Tick(float deltaTime)
		{
			ElapsedTime += deltaTime;
		}
	}
}