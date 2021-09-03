
using NUnit.Framework;

using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestAbsoluteValue
    {
        [Test]
        public void TestAbsoluteValue01()
        {
            var param1 = new FrequencyParameter("freq", -32, FrequencyUnit.Megahertz);

            // TODO: Introduce BaseUnitValue as a getter on parameters
            Assert.IsTrue(-32 == param1.CurrentUnitValue);
            Assert.IsTrue(32 == param1.AbsoluteValue().CurrentUnitValue);
        }
    }
}