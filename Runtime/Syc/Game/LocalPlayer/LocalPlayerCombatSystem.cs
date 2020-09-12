using Syc.Combat;
using Syc.Combat.HealthSystem;
using Syc.Combat.ModifierSystem;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Game.LocalPlayer
{
	public class LocalPlayerCombatSystem : CombatSystem
	{
		[SerializeField] private InputSystem inputSystem;
		public override object Allegiance => gameObject.layer;
		public override ICombatAttributes AttributeSystem => defaultAttributesSystem;
		public override Transform Origin => transform;

		[SerializeField] private LocalPlayerTargetSystem localPlayerTargetSystem;
		
		[SerializeField] private SpellRack spellComponent;

		[SerializeField] private HealthComponent healthComponent;

		[SerializeField] private ModifierComponent modifierComponent;

		[SerializeField] private DefaultAttributesSystem defaultAttributesSystem;

		private void Awake()
		{
			AddSubsystem(localPlayerTargetSystem);
			AddSubsystem(spellComponent);
			AddSubsystem(healthComponent);
			AddSubsystem(modifierComponent);

			inputSystem.OnCastSpell += spellComponent.CastSpell;
			inputSystem.OnMove += _ => spellComponent.MovementIntterupt();
		}

		private void OnDestroy()
		{
			inputSystem.OnCastSpell -= spellComponent.CastSpell;
		}
	}
}