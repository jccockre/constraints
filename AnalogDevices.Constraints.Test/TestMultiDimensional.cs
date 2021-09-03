
using NUnit.Framework;

using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestMultiDimensional
    {
        public FrequencyParameter DataRate = new FrequencyParameter("Data Rate", 5, FrequencyUnit.Megahertz);
        public TimeParameter FrameDuration = new TimeParameter("Frame Duration", 20, DurationUnit.Millisecond);
        public ScalarParameter NumSamples = new ScalarParameter("Number of Samples", 1024);

        public VoltageParameter Voltage = new VoltageParameter("Voltage", 2, ElectricPotentialUnit.Millivolt);
        public CurrentParameter Current = new CurrentParameter("Current", 3, ElectricCurrentUnit.Milliampere);

        [Test]
        public void TestMultiDimensional01()
        {
            var dataRateTimesFrameDuration = DataRate.Times(FrameDuration);
            var constraint1 = NumSamples.GreaterThanOrEqualTo(dataRateTimesFrameDuration);

            var result1 = constraint1.Validate();

            Assert.IsFalse(result1.Status);
        }

        [Test]
        public void TestMultiDimensional02()
        {
            var power = Voltage.Times<Power, ElectricPotential, ElectricCurrent>(Current);
            var constraint1 = power.LessThanOrEqualTo(10, PowerUnit.Microwatt);
            var constraint2 = power.LessThanOrEqualTo(1, PowerUnit.Microwatt);

            var result1 = constraint1.Validate();
            var result2 = constraint2.Validate();

            Assert.IsTrue(result1.Status);
            Assert.IsFalse(result2.Status);
        }
    }
}