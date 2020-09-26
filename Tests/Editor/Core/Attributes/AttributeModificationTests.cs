using NUnit.Framework;
using Syc.Core.Attributes;

namespace Tests.Editor.Core.Attributes
{
    public class AttributeModificationTests
    {
        // private const int DefaultValue = 10;
        // private readonly Attribute _stamina = new Attribute(DefaultValue);
        // private readonly Attribute _criticalStrikeRating = new Attribute(DefaultValue);
        // private readonly Attribute _haste = new Attribute(DefaultValue);
        // private readonly Attribute _armor = new Attribute(DefaultValue);
        // private readonly Attribute _spellPower = new Attribute(DefaultValue);
        //
        // // A Test behaves as an ordinary method
        // [Test]
        // public void TestAttributeAddition()
        // {
        //     Assert.AreEqual(DefaultValue, _stamina.BaseValue);
        //     Assert.AreEqual(DefaultValue, _stamina.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.BaseValue);
        //     
        //     _stamina.Add(DefaultValue, out var staminaAdditionHandle);
        //
        //     Assert.AreEqual(DefaultValue, _stamina.BaseValue);
        //     Assert.AreEqual(DefaultValue + DefaultValue, _stamina.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.BaseValue);
        //     
        //     staminaAdditionHandle.DestroyModification();
        //     
        //     Assert.AreEqual(DefaultValue, _stamina.BaseValue);
        //     Assert.AreEqual(DefaultValue, _stamina.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.BaseValue);
        // }
        //
        // [Test]
        // public void TestAttributeMultiplication()
        // {
        //     Assert.AreEqual(DefaultValue, _spellPower.BaseValue);
        //     Assert.AreEqual(DefaultValue, _spellPower.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _haste.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _haste.BaseValue);
        //     
        //     _spellPower.Multiply(DefaultValue, out var spellPowerMultiplicationHandle);
        //
        //     Assert.AreEqual(DefaultValue, _spellPower.BaseValue);
        //     Assert.AreEqual(DefaultValue * DefaultValue, _spellPower.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _haste.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _haste.BaseValue);
        //     
        //     spellPowerMultiplicationHandle.DestroyModification();
        //     
        //     Assert.AreEqual(DefaultValue, _spellPower.BaseValue);
        //     Assert.AreEqual(DefaultValue, _spellPower.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _haste.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _haste.BaseValue);
        // }
        //
        // [Test]
        // public void TestAttributeAdditionAndThenMultiplication()
        // {
        //     Assert.AreEqual(DefaultValue, _spellPower.BaseValue);
        //     Assert.AreEqual(DefaultValue, _spellPower.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.BaseValue);
        //     
        //     _spellPower.Add(DefaultValue, out var spellPowerAdditionHandle);
        //     _spellPower.Multiply(DefaultValue, out var spellPowerMultiplicationHandle);
        //
        //     Assert.AreEqual(DefaultValue, _spellPower.BaseValue);
        //     Assert.AreEqual((DefaultValue + DefaultValue) * DefaultValue, _spellPower.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.BaseValue);
        //     
        //     spellPowerAdditionHandle.DestroyModification();
        //     
        //     Assert.AreEqual(DefaultValue, _spellPower.BaseValue);
        //     Assert.AreEqual(DefaultValue * DefaultValue, _spellPower.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.BaseValue);
        //     
        //     spellPowerMultiplicationHandle.DestroyModification();
        //     
        //     Assert.AreEqual(DefaultValue, _spellPower.BaseValue);
        //     Assert.AreEqual(DefaultValue, _spellPower.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _armor.BaseValue);
        // }
        //
        // [Test]
        // public void TestAttributeMultiplicationAndThenAddition()
        // {
        //     Assert.AreEqual(DefaultValue, _stamina.BaseValue);
        //     Assert.AreEqual(DefaultValue, _stamina.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.BaseValue);
        //     
        //     _stamina.Multiply(DefaultValue, out var staminaMultiplicationHandle);
        //     _stamina.Add(DefaultValue, out var staminaAdditionHandle);
        //
        //     Assert.AreEqual(DefaultValue, _stamina.BaseValue);
        //     Assert.AreEqual((DefaultValue + DefaultValue) * DefaultValue, _stamina.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.BaseValue);
        //     
        //     staminaMultiplicationHandle.DestroyModification();
        //
        //     Assert.AreEqual(DefaultValue, _stamina.BaseValue);
        //     Assert.AreEqual(DefaultValue + DefaultValue, _stamina.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.BaseValue);
        //     
        //     staminaAdditionHandle.DestroyModification();
        //     
        //     Assert.AreEqual(DefaultValue, _stamina.BaseValue);
        //     Assert.AreEqual(DefaultValue, _stamina.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.CurrentValue);
        //     Assert.AreEqual(DefaultValue, _criticalStrikeRating.BaseValue);
        // }
    }
}
