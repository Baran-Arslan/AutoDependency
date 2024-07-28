using NUnit.Framework;

namespace iCare.Tests
{
    [TestFixture]
    public class DynamicVariableTests
    {
        [Test]
        public void ValueChanged_ShouldTriggerOnValueChangedEvent()
        {
            var dynamicVariable = new DynamicVariable<int>(0);
            var wasEventTriggered = false;
            dynamicVariable.OnValueChanged += newValue => wasEventTriggered = true;

            dynamicVariable.Value = 1;

            Assert.IsTrue(wasEventTriggered);
        }

        [Test]
        public void PreviousValue_ShouldBeUpdatedCorrectly()
        {
            var dynamicVariable = new DynamicVariable<int>(0)
            {
                Value = 1
            };

            Assert.AreEqual(0, dynamicVariable.PreviousValue);
            Assert.AreEqual(1, dynamicVariable.Value);
        }

        [Test]
        public void ImplicitConversion_ShouldReturnCurrentValue()
        {
            var dynamicVariable = new DynamicVariable<int>(5);
            int value = dynamicVariable;

            Assert.AreEqual(5, value);
        }
    }
}