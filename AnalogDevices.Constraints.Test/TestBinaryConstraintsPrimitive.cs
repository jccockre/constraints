
using NUnit.Framework;

using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestBinaryConstraintsPrimitive
    {
        public FrequencyParameter Alpha = new FrequencyParameter("Alpha");
        public FrequencyParameter Beta = new FrequencyParameter("Beta");
        public FrequencyParameter Gamma = new FrequencyParameter("Gamma");
        public FrequencyParameter Delta = new FrequencyParameter("Delta");

        [Test]
        public void TestBinaryConstraintsPrimitive01()
        {
            ConstraintBase constraint = Alpha.AtLeast(0).And(Alpha.AtMost(100)).And(Beta.AtLeast(35).And(Beta.AtMost(75)));

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = 35;

            ValidationResult result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 100;
            Beta.CurrentUnitValue = 75;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 101;
            Beta.CurrentUnitValue = 75;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 100;
            Beta.CurrentUnitValue = 76;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = -1;
            Beta.CurrentUnitValue = 35;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = 34;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = -1;
            Beta.CurrentUnitValue = 34;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 101;
            Beta.CurrentUnitValue = 76;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);
        }

        [Test]
        public void TestBinaryConstraintsPrimitive02()
        {
            ConstraintBase constraint = Alpha.AtLeast(0).And(Alpha.AtMost(100)).Or(Beta.AtLeast(35).And(Beta.AtMost(75)));

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = 35;

            ValidationResult result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 100;
            Beta.CurrentUnitValue = 75;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 101;
            Beta.CurrentUnitValue = 75;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 100;
            Beta.CurrentUnitValue = 76;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = -1;
            Beta.CurrentUnitValue = 35;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = 34;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = -1;
            Beta.CurrentUnitValue = 34;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 101;
            Beta.CurrentUnitValue = 76;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);
        }

        [Test]
        public void TestBinaryConstraintsPrimitive03()
        {
            ConstraintBase constraint1 = Alpha.AtLeast(0).And(Alpha.AtMost(100)).Or(Beta.AtLeast(35).And(Beta.AtMost(75)));
            ConstraintBase constraint2 = Gamma.AtLeast(0).And(Gamma.AtMost(100)).Or(Delta.AtLeast(35).And(Delta.AtMost(75)));
            ConstraintBase constraint = constraint1.And(constraint2);

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 0;
            Delta.CurrentUnitValue = 35;

            ValidationResult result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = -1;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 0;
            Delta.CurrentUnitValue = 76;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = -1;
            Beta.CurrentUnitValue = 34;
            Gamma.CurrentUnitValue = 0;
            Delta.CurrentUnitValue = 35;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 0;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = -1;
            Delta.CurrentUnitValue = 34;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 101;
            Beta.CurrentUnitValue = 34;
            Gamma.CurrentUnitValue = -1;
            Delta.CurrentUnitValue = 76;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);
        }
    }
}