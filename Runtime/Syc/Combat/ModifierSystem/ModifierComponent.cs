using System;

namespace Syc.Combat.ModifierSystem
{
	[Serializable]
	public class ModifierComponent : ICombatSubSystem
	{
		public ICombatSystem System { get; set; }

		public void Update(float deltaTime)
		{
			throw new NotImplementedException();
		}
	}
}