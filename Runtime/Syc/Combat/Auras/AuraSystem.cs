using System;
using System.Collections.Generic;
using System.Linq;
using Syc.Combat.Auras.ScriptableObjects;
using Syc.Combat.SpellSystem;

namespace Syc.Combat.Auras
{
	[Serializable]
	public class AuraSystem : ICombatSubSystem
	{
		public event Action<AuraState> OnAuraAdded;
		public event Action<AuraState> OnAuraRemoved;
		public ICombatSystem System { get; set; }

		public IEnumerable<AuraState> ActiveAuras => _activeAuras;

		private List<AuraState> _activeAuras = new List<AuraState>();

		public AuraState GetAura(Aura aura, object referenceObject)
		{
			return _activeAuras
				.FirstOrDefault(x => x.AuraType == aura && x.ReferenceObject == referenceObject);
		}

		public AuraState AddAura(Aura aura, ICaster source, object referenceObject)
		{
			var activeAura = GetAura(aura, referenceObject);
			
			if (activeAura != null)
			{
				activeAura.AddStack();
				return activeAura;
			}
			
			var newAura = aura.CreateState(source, System, referenceObject);
			_activeAuras.Add(newAura);
			newAura.Apply();
			OnAuraAdded?.Invoke(newAura);
			return newAura;
		}

		public void RemoveAura(Aura aura, object referenceObject)
		{
			var activeAura = _activeAuras
				.FirstOrDefault(x => x.AuraType == aura && x.ReferenceObject == referenceObject);
			
			if (activeAura == null)
				return;
			
			activeAura.Remove();
			_activeAuras.Remove(activeAura);
			OnAuraRemoved?.Invoke(activeAura);
		}
		

		public void Tick(float deltaTime)
		{
			foreach (var activeAura in _activeAuras)
			{
				activeAura.Tick(deltaTime);
			}

			var completeAuras =  _activeAuras
				.Where(x => x.HasExpired)
				.ToList();
			
			completeAuras.AddRange(_activeAuras.Where(x => x.Stacks < 1));

			foreach (var completeAura in completeAuras)
			{
				completeAura.Remove();
				_activeAuras.Remove(completeAura);
				OnAuraRemoved?.Invoke(completeAura);
			}
		}
	}
}