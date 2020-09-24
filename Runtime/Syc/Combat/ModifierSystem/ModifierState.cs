using Syc.Combat.ModifierSystem.ScriptableObjects;

namespace Syc.Combat.ModifierSystem
{
	public class ModifierState
	{
		public Modifier Modifier => _modifier;
		
		public bool TimeIsElapsed { get; private set; }

		public float ActiveDuration => _activeDuration;
		
		private Modifier _modifier;

		private float _activeDuration;

		public ModifierState(Modifier modifier)
		{
			_modifier = modifier;
		}

		public void Update(float deltaTime)
		{
			_activeDuration += deltaTime;

			if (_activeDuration >= _modifier.Duration)
				TimeIsElapsed = true;
		}
	}
}