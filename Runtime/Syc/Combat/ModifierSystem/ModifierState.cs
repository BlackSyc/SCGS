using System;
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

		public event Action<int> OnStackRemoved;
		
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
			ElapsedTime = 0;
			
			if (Stacks >= ModifierType.StackLimit)
				return;
			
			Stacks += 1;
			ModifierType.AppliedStack(this);
			OnStackAdded?.Invoke(Stacks);
		}

		public void RemoveStack()
		{
			Stacks -= 1;
			ModifierType.RemovedStack(this);
			OnStackRemoved?.Invoke(Stacks);
		}

		public void Apply()
		{
			AddStack();
			ModifierType.Applied(this);
		}

		public void Remove()
		{
			var stacks = Stacks;
			for (var i = 0; i < stacks; i++)
			{
				RemoveStack();
			}
			
			ModifierType.Removed(this);
		}

		public void Tick(float deltaTime)
		{
			ElapsedTime += deltaTime;
		}
	}
}