using Syc.Combat;
using Syc.Combat.HealthSystem;
using Syc.Combat.ModifierSystem;
using Syc.Combat.SpellSystem;
using UnityEngine;

namespace Syc.Game.LocalPlayer
{
	public class LocalPlayerCombatMonoSystem : CombatMonoSystem
	{
		[SerializeField] private PlayerInput playerInput;
		public override object Allegiance => gameObject.layer;
		public override ICombatAttributes AttributeSystem => defaultAttributesSystem;
		public override Transform Origin => transform;

		[SerializeField] private LocalPlayerTargetSystem localPlayerTargetSystem;
		
		[SerializeField] private SpellRack spellComponent;

		[SerializeField] private HealthSystem healthSystem;

		[SerializeField] private ModifierSystem modifierSystem;

		[SerializeField] private DefaultAttributesSystem defaultAttributesSystem;

		private void Awake()
		{
			AddSubsystem(localPlayerTargetSystem);
			AddSubsystem(spellComponent);
			AddSubsystem(healthSystem);
			AddSubsystem(modifierSystem);

			playerInput.OnCastSpell += spellComponent.CastSpell;
			playerInput.OnMove += _ => spellComponent.MovementIntterupt();
		}

		private void OnDestroy()
		{
			playerInput.OnCastSpell -= spellComponent.CastSpell;
		}
	}
}