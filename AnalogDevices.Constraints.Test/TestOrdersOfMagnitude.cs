
using NUnit.Framework;

using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestOrdersOfMagnitude
    {
        public FrequencyParameter Alpha = new FrequencyParameter("Alpha", 6, FrequencyUnit.Megahertz);
        public FrequencyParameter Beta = new FrequencyParameter("Beta", -0.1, FrequencyUnit.Hertz);
        public FrequencyParameter Gamma = new FrequencyParameter("Gamma", 3000, FrequencyUnit.Kilohertz);

        [Test]
        public void TestOrdersOfMagnitude01()
        {
            var constraint1 = Alpha.AtLeast(3).And(Alpha.AtMost(10));
            var constraint2 = Alpha.AtLeast(Frequency.From(3e6, FrequencyUnit.Hertz)).And(Alpha.AtMost(Frequency.From(10e6, FrequencyUnit.Hertz)));
            var constraint3 = Alpha.AtLeast(Beta).And(Alpha.AtMost(Gamma));

            var result1 = constraint1.Validate();
            var result2 = constraint2.Validate();
            var result3 = constraint3.Validate();

            Assert.IsTrue(result1.Status);
            Assert.IsTrue(result2.Status);
            Assert.IsFalse(result3.Status);
        }

        [Test]
        public void TestOrdersOfMagnitude02()
        {
            var product1 = Alpha.Times<BaseUnit, Frequency, Frequency>(Beta);
            var quotient1 = Alpha.DividedBy<BaseUnit, Frequency, Frequency>(Gamma);

            Assert.AreEqual(-6e5, product1.Value.BaseunitValue);
            Assert.AreEqual(2, quotient1.Value.BaseunitValue);
        }
    }
}