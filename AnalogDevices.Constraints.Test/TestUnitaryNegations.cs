
using NUnit.Framework;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestUnitaryNegations
    {
        public FrequencyParameter Alpha = new FrequencyParameter("Alpha");
        public FrequencyParameter Beta = new FrequencyParameter("Beta");
        public FrequencyParameter Gamma = new FrequencyParameter("Gamma");

        [Test]
        public void TestUnitaryNegations01()
        {
            ConstraintBase constraint = Alpha.AtLeast(0).And(Alpha.AtMost(100)).Not();

            Alpha.CurrentUnitValue = 0;

            ValidationResult result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 100;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = -1;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 101;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);
        }

        [Test]
        public void TestUnitaryNegations02()
        {
            ConstraintBase constraint = Alpha.AtLeast(0).And(Alpha.AtMost(100)).Not().Not();

            Alpha.CurrentUnitValue = 0;

            ValidationResult result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 100;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = -1;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 101;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);
        }

        [Test]
        public void TestUnitaryNegations03()
        {
            ConstraintBase constraint = Alpha.AtLeast(Beta).And(Alpha.AtMost(Gamma)).Not().Not().Not();

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = 0;
            Gamma.CurrentUnitValue = 0;

            ValidationResult result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = -5;
            Gamma.CurrentUnitValue = 0;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 5;
            Beta.CurrentUnitValue = 0;
            Gamma.CurrentUnitValue = 5;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = -5;
            Beta.CurrentUnitValue = 0;
            Gamma.CurrentUnitValue = 5;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 15;
            Beta.CurrentUnitValue = 0;
            Gamma.CurrentUnitValue = 5;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);
        }
    }
}