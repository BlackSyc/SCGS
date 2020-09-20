using Syc.Combat.ModifierSystem.ScriptableObjects;

namespace Syc.Combat.ModifierSystem
{
	public class Modifier
	{
		public ModifierBehaviour ModifierBehaviour => _modifierBehaviour;
		
		public bool TimeIsElapsed { get; private set; }
		
		private ModifierBehaviour _modifierBehaviour;

		private float _activeDuration = 0;

		public Modifier(ModifierBehaviour modifierBehaviour)
		{
			_modifierBehaviour = modifierBehaviour;
		}

		public void Update(float deltaTime)
		{
			_activeDuration += deltaTime;

			if (_activeDuration >= _modifierBehaviour.Duration)
				TimeIsElapsed = true;
		}
	}
}