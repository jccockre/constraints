
using NUnit.Framework;

using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestBinaryConstraintsParameter
    {
        public FrequencyParameter Alpha = new FrequencyParameter("Alpha");
        public FrequencyParameter Beta = new FrequencyParameter("Beta");
        public FrequencyParameter Gamma = new FrequencyParameter("Gamma");
        public FrequencyParameter Delta = new FrequencyParameter("Delta");

        [Test]
        public void TestBinaryConstraintsParameter01()
        {
            ConstraintBase constraint = Alpha.AtLeast(Beta).And(Alpha.AtMost(Frequency.From(100, FrequencyUnit.Hertz)))
                .And(Gamma.AtLeast(Frequency.From(35, FrequencyUnit.Hertz)).And(Gamma.AtMost(Delta)));

            Alpha.Value = Frequency.From(35, FrequencyUnit.Hertz);
            Beta.Value = Frequency.From(35, FrequencyUnit.Hertz);
            Gamma.Value = Frequency.From(35, FrequencyUnit.Hertz);
            Delta.Value = Frequency.From(35, FrequencyUnit.Hertz);

            ValidationResult result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 35;
            Beta.CurrentUnitValue = 47;
            Gamma.CurrentUnitValue = 35;
            Delta.CurrentUnitValue = 35;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 22;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 35;
            Delta.CurrentUnitValue = 35;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 35;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 35;
            Delta.CurrentUnitValue = 135;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 35;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 135;
            Delta.CurrentUnitValue = 65;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 135;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 135;
            Delta.CurrentUnitValue = 65;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);
        }
    }
}