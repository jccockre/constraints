
using NUnit.Framework;

using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestBinaryNumericalParameters
    {
        public FrequencyParameter Alpha = new FrequencyParameter("Alpha");
        public FrequencyParameter Beta = new FrequencyParameter("Beta");
        public FrequencyParameter Gamma = new FrequencyParameter("Gamma");
        public FrequencyParameter Delta = new FrequencyParameter("Delta");

        [Test]
        public void TestBinaryNumericalParameters01()
        {
            var sum = Alpha.Plus(Beta);
            var difference = Alpha.Minus(Beta);
            ConstraintBase constraint = Gamma.GreaterThanOrEqualTo(difference).And(Gamma.LessThanOrEqualTo(sum));

            Alpha.CurrentUnitValue = 35;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 35;

            ValidationResult result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 35;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 71;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 70;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 34;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 70;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 35;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);
        }

        [Test]
        public void TestBinaryNumericalParameters02()
        {
            var sum = Alpha.Plus(Beta);
            var difference = Alpha.Minus(Beta);
            ConstraintBase constraint = sum.GreaterThanOrEqualTo(difference).And(sum.LessThanOrEqualTo(Gamma));

            Alpha.CurrentUnitValue = 35;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 35;

            ValidationResult result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 35;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 71;

            result = constraint.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 70;
            Beta.CurrentUnitValue = 35;
            Gamma.CurrentUnitValue = 34;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 5;
            Beta.CurrentUnitValue = -5;
            Gamma.CurrentUnitValue = 70;

            result = constraint.Validate();

            Assert.IsFalse(result.Status);
        }

        [Test]
        public void TestBinaryNumericalParameters03()
        {
            var product = Alpha.Times<BaseUnit, Frequency, Frequency>(Beta);
            var quotient = Beta.DividedBy<BaseUnit, Frequency, Frequency>(Gamma);

            ConstraintBase constraint2 = quotient.GreaterThanOrEqualTo(0.25).And(quotient.LessThanOrEqualTo(1));
            ConstraintBase constraint1 = product.GreaterThanOrEqualTo(
                (BaseUnit) (Frequency.From(1, FrequencyUnit.Hertz) * Frequency.From(1, FrequencyUnit.Hertz)))
                .And(product.LessThanOrEqualTo(
                (BaseUnit) (Frequency.From(1024, FrequencyUnit.Hertz) * Frequency.From(1, FrequencyUnit.Hertz))));

            Alpha.CurrentUnitValue = 16;
            Beta.CurrentUnitValue = 16;
            Gamma.CurrentUnitValue = 32;

            ValidationResult result1 = constraint1.Validate();
            ValidationResult result2 = constraint2.Validate();

            Assert.IsTrue(result1.Status);
            Assert.IsTrue(result2.Status);

            Alpha.CurrentUnitValue = 64;
            Beta.CurrentUnitValue = 32;
            Gamma.CurrentUnitValue = 32;

            result1 = constraint1.Validate();
            result2 = constraint2.Validate();

            Assert.IsFalse(result1.Status);
            Assert.IsTrue(result2.Status);

            Alpha.CurrentUnitValue = 8;
            Beta.CurrentUnitValue = 32;
            Gamma.CurrentUnitValue = 4;

            result1 = constraint1.Validate();
            result2 = constraint2.Validate();

            Assert.IsTrue(result1.Status);
            Assert.IsFalse(result2.Status);
        }

        [Test]
        public void TestBinaryNumericalParameters04()
        {
            var product = Alpha.Times<Frequency, Frequency, BaseUnit>(8);
            var quotient = Parameter.Constant<BaseUnit>(2048).DividedBy<Duration, BaseUnit, Frequency>(Alpha);

            ConstraintBase constraint1 = Gamma.LessThanOrEqualTo(product);
            ConstraintBase constraint2 = quotient.GreaterThan<Duration>(8);

            Alpha.CurrentUnitValue = 256;
            Gamma.CurrentUnitValue = 32;

            ValidationResult result1 = constraint1.Validate();
            ValidationResult result2 = constraint2.Validate();

            Assert.IsTrue(result1.Status);
            Assert.IsFalse(result2.Status);

            Alpha.CurrentUnitValue = 64;
            Gamma.CurrentUnitValue = 2048;

            result1 = constraint1.Validate();
            result2 = constraint2.Validate();

            Assert.IsFalse(result1.Status);
            Assert.IsTrue(result2.Status);
        }

        [Test]
        public void TestBinaryNumericalParameters05()
        {
            Alpha.CurrentUnitValue = 2;

            var pow0 = Alpha.ToThePowerOf(0).Value;
            var pow1 = Alpha.ToThePowerOf<Frequency, Frequency>(1).Value;
            var pow2 = Alpha.ToThePowerOf(2).Value;
            var pow3 = Alpha.ToThePowerOf(3).Value;
            var pow4 = Alpha.ToThePowerOf<Duration, Frequency>(-1).Value;
            var pow5 = Alpha.ToThePowerOf(-2).Value;
        }
    }
}