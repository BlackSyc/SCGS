using System.Collections;
using UnityEngine;

namespace Syc.Combat.ModifierSystem.ScriptableObjects
{
	public class PeriodicDamageModifier : Modifier
	{
		[SerializeField] protected float baseDamage;
		[SerializeField] protected bool allowMitigation;
		[SerializeField] protected bool affectedBySpellPower;
		[SerializeField] protected bool canCrit;
		[SerializeField] protected float critMultiplier;
		[SerializeField] protected float period;
		
		public override void AppliedStack(ModifierState state)
		{
			state.StartCoroutine(DamageCoroutine(state));
		}

		public override void RemovedStack(ModifierState state)
		{
			state.CoroutineShouldStop = true;
		}

		private IEnumerator DamageCoroutine(ModifierState state)
		{
			float lastBlow = 0f;
			
			while (!state.CoroutineShouldStop)
			{
				if (lastBlow < Time.time - period)
				{
					yield return null;
				}
				
				
				
				
			}
		}
	}
}