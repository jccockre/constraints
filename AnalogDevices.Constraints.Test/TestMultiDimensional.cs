
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
            var Voltage = new VoltageParameter("Voltage", 2, ElectricPotentialUnit.Millivolt);
            var Current = new CurrentParameter("Current", 3, ElectricCurrentUnit.Milliampere);

            // don't consume more than 1 microwatt of power
            var ConstrainPowerConsumption = Voltage.Times(Current).AtMost(Power.FromMicrowatts(1));
        
            var Result = ConstrainPowerConsumption.Validate();

            // Result.Status is False
            // Result.Warning is "Voltage * Current (6e-06 W) must be less than or equal to 1E-06 W"
            Assert.IsFalse(Result.Status);
        }
    }
}