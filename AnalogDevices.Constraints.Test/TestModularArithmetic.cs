
using NUnit.Framework;

using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestModularArithmetic
    {
        [Test]
        public void TestModularArithmetic01()
        {
            FrequencyParameter DataRate = new FrequencyParameter("Data Rate", 5, FrequencyUnit.Megahertz);
            ScalarParameter NumSamples = new ScalarParameter("Number of Samples", 1024);

            FrequencyParameter ClockRate = new FrequencyParameter("Clock Rate", 10, FrequencyUnit.Gigahertz);

            var constraint1 = ClockRate.IsMultipleOf(DataRate);
            var constraint2 = NumSamples.IsMultipleOf(64);

            var result1 = constraint1.Validate();
            var result2 = constraint2.Validate();

            Assert.IsTrue(result1.Status);
            Assert.IsTrue(result2.Status);

            DataRate.CurrentUnitValue = 77;
            NumSamples.CurrentUnitValue = 985;

            result1 = constraint1.Validate();
            result2 = constraint2.Validate();

            Assert.IsFalse(result1.Status);
            Assert.IsFalse(result2.Status);
        }

        [Test]
        public void TestModularArithmetic02()
        {
            FrequencyParameter DataRate = new FrequencyParameter("Data Rate", 5, FrequencyUnit.Megahertz);

            FrequencyParameter ClockRate = new FrequencyParameter("Clock Rate", 10, FrequencyUnit.Gigahertz);

            var constraint1 = ClockRate.IsMultipleOf(DataRate, Frequency.FromKilohertz(2));

            ClockRate.CurrentUnitValue = 10.005001; // OK to within a tolerance of 2 kilohertz

            var result1 = constraint1.Validate();

            Assert.IsTrue(result1.Status);

            ClockRate.CurrentUnitValue = 10.005003; // out-of-bounds

            result1 = constraint1.Validate();

            Assert.IsFalse(result1.Status);
        }
    }
}