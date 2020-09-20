using Moq;
using NUnit.Framework;
using Syc.Combat;
using Syc.Combat.SpellSystem;
using Syc.Combat.SpellSystem.ScriptableObjects.TargetProviders;
using UnityEngine;

namespace Tests.Editor.Combat.SpellSystem
{
	public class CastingSystemTests
	{
		[Test]
		public void TestCastingSystemCast()
		{
			// Arrange
			var mockCombatSystem = new Mock<ICombatSystem>();
			var castingSystem = new CastingSystem { System = mockCombatSystem.Object };

			mockCombatSystem.Setup(x => x.Origin).Returns(new GameObject().transform);
			mockCombatSystem.Setup(x => x.Get<ICaster>()).Returns(castingSystem);
			
			var testSpell = ScriptableObject.CreateInstance<TestSpellBehaviour>();
			testSpell.TargetProvider = ScriptableObject.CreateInstance<Self>();

			SpellCast spellCast = null;
			castingSystem.OnNewSpellCast += x => spellCast = x;
			
			// Act
			castingSystem.CastSpell(new Spell(testSpell));
			
			// Assert
			mockCombatSystem.Verify(x => x.Origin, Times.Exactly(1 ));
			Assert.IsNotNull(spellCast);
			Assert.AreEqual(spellCast.SpellBehaviour, testSpell);
		}
	}
}