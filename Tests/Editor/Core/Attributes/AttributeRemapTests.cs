using NUnit.Framework;
using Syc.Core.Attributes;

namespace Tests.Editor.Core.Attributes
{
	public class AttributeRemapTests
	{

		[Test]
		public void TestCombatRemapCalculation()
		{
			var attribute1 = new Attribute(10, 2, 2);

			Assert.AreEqual(10, attribute1.CurrentValue);
			Assert.AreEqual(200, attribute1.Remap());
			
			var attribute2 = new Attribute(9, 1, 2);
			
			Assert.AreEqual(9, attribute2.CurrentValue);
			Assert.AreEqual(81, attribute2.Remap());
			
			var attribute3 = new Attribute(3, .5f, 3);
			
			Assert.AreEqual(3, attribute3.CurrentValue);
			Assert.AreEqual(13.5f, attribute3.Remap(), 0.0001f);
			
			var attribute4 = new Attribute(4);
			
			Assert.AreEqual(4, attribute4.BaseValue);
			Assert.AreEqual(4, attribute4.CurrentValue);
			Assert.AreEqual(4, attribute4.Remap());
			
			var attribute5 = new Attribute(9, 1, 0.5f);

			Assert.AreEqual(9, attribute5.CurrentValue);
			Assert.AreEqual(3, attribute5.Remap());
		}

		[Test]
		public void TestStaminaScenario()
		{
			var stamina = new Attribute(10, 10);
			
			Assert.AreEqual(100, stamina.Remap());

			stamina.Add(6, out var staminaAdditionHandle);
			
			Assert.AreEqual(160, stamina.Remap());

			stamina.Multiply(1.25f, out var staminaMultiplicationHandle);
			
			Assert.AreEqual(200, stamina.Remap());

			stamina.Add(8, out var staminaAdditionHandle2);
			
			Assert.AreEqual(300, stamina.Remap());
			
			staminaAdditionHandle.DestroyModification();
			
			Assert.AreEqual(225, stamina.Remap());
			
			staminaAdditionHandle2.DestroyModification();
			staminaMultiplicationHandle.DestroyModification();
			
			Assert.AreEqual(100, stamina.Remap());
			Assert.AreEqual(10, stamina.CurrentValue);
		}
		
	}
}