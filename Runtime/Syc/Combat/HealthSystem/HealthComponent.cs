﻿using System;
using UnityEngine;

namespace Syc.Combat.HealthSystem
{
	[Serializable]
	public class HealthComponent : ICombatSubSystem
	{
		public event Action<DamageRequest> OnDied;
		public event Action<DamageRequest> OnDamageReceived;
		public event Action<HealRequest> OnHealingReceived;
		
		public ICombatSystem System { get; set; }

		public float MaxHealth => System.AttributeSystem?.Stamina.CurrentValue ?? 9999f;
		public float CurrentHealth => currentHealth;
		public bool IsDead => CurrentHealth == 0f;
		
		[SerializeField] 
		private float currentHealth;

		public float Damage(DamageRequest damageRequest)
		{
			if (IsDead)
				return 0;

			var damageAmount = damageRequest.CalculateAmount(System.AttributeSystem);
			
			currentHealth -= damageAmount;

			if (currentHealth > 0)
			{
				damageRequest.AmountDealt = damageAmount;
				OnDamageReceived?.Invoke(damageRequest);
				return damageAmount;
			}

			var actualDamageDealt = damageAmount + currentHealth;
			currentHealth = 0;
			damageRequest.AmountDealt = actualDamageDealt;
			OnDamageReceived?.Invoke(damageRequest);
			OnDied?.Invoke(damageRequest);
			return actualDamageDealt;
		}
		
		public float Heal(HealRequest healRequest)
		{
			if (IsDead)
				return 0;

			var healAmount = healRequest.CalculateAmount(System.AttributeSystem);
			currentHealth += healAmount;

			if (currentHealth <= MaxHealth)
			{
				healRequest.AmountDealt = healAmount;
				OnHealingReceived?.Invoke(healRequest);
				return healAmount;
			}

			var actualHealAmount = healAmount - (currentHealth - MaxHealth);
			currentHealth = MaxHealth;
			healRequest.AmountDealt = actualHealAmount;
			OnHealingReceived?.Invoke(healRequest);
			return actualHealAmount;
		}
		
		public void Update(float deltaTime) { }
	}
}