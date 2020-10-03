using System;
using System.Collections;
using Syc.Combat.Auras.ScriptableObjects;
using Syc.Combat.SpellSystem;

namespace Syc.Combat.Auras
{
	[Serializable]
	public class AuraState
	{
		public event Action<int> OnStackAdded;

		public event Action<int> OnStackRemoved;
		
		public bool HasExpired => ElapsedTime > AuraType.Duration;
		public Aura AuraType { get; }
		public int Stacks { get; set; }
		public float ElapsedTime { get; set; }
		public object ReferenceObject { get; }
		public ICaster Source { get; }
		public ICombatSystem Target { get; }
		
		public bool CoroutineShouldStop { get; set; }

		public AuraState(ICaster source, ICombatSystem target, Aura aura, object referenceObject)
		{
			AuraType = aura;
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
			
			if (Stacks >= AuraType.StackLimit)
				return;
			
			Stacks += 1;
			AuraType.AppliedStack(this);
			OnStackAdded?.Invoke(Stacks);
		}

		public void RemoveStack()
		{
			Stacks -= 1;
			AuraType.RemovedStack(this);
			OnStackRemoved?.Invoke(Stacks);
		}

		public void Apply()
		{
			AddStack();
			AuraType.Applied(this);
		}

		public void Remove()
		{
			var stacks = Stacks;
			for (var i = 0; i < stacks; i++)
			{
				RemoveStack();
			}
			
			AuraType.Removed(this);
		}

		public void Tick(float deltaTime)
		{
			ElapsedTime += deltaTime;
		}
	}
}