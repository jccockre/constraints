
using NUnit.Framework;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    [TestFixture]
    public class TestConditionals
    {
        public FrequencyParameter Alpha = new FrequencyParameter("Alpha");
        public FrequencyParameter Beta = new FrequencyParameter("Beta");
        public FrequencyParameter Gamma = new FrequencyParameter("Gamma");
        public FrequencyParameter Delta = new FrequencyParameter("Delta");
        public FrequencyParameter Epsilon = new FrequencyParameter("Epsilon");
        public FrequencyParameter Zeta = new FrequencyParameter("Zeta");

        [Test]
        public void TestConditionals01()
        {
            var aLessThanB = Alpha.LessThan(Beta);
            var bLessThanG = Beta.LessThan(Gamma);

            var conditional = Constraints.If(aLessThanB).Then(bLessThanG);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 3;

            var result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;

            result = conditional.Validate();

            Assert.IsTrue(result.Status);
        }

        [Test]
        public void TestConditionals02()
        {
            var aLessThanB = Alpha.LessThan(Beta);
            var bLessThanG = Beta.LessThan(Gamma);
            var gLessThanD = Gamma.LessThan(Delta);

            var conditional = Constraints.If(aLessThanB).Then(bLessThanG).Else(gLessThanD);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 3;
            Delta.CurrentUnitValue = 2;

            var result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;
            Delta.CurrentUnitValue = 2;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;
            Delta.CurrentUnitValue = 2;

            result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 3;
            Delta.CurrentUnitValue = 2;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);
        }

        [Test]
        public void TestConditionals03()
        {
            var aLessThanB = Alpha.LessThan(Beta);
            var bLessThanG = Beta.LessThan(Gamma);
            var gLessThanD = Gamma.LessThan(Delta);
            var dLessThanE = Delta.LessThan(Epsilon);

            var conditional = Constraints.If(aLessThanB).Then(bLessThanG).ElseIf(gLessThanD).Then(dLessThanE);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 3;

            var result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;
            Delta.CurrentUnitValue = 2;
            Epsilon.CurrentUnitValue = 3;

            result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;
            Delta.CurrentUnitValue = 2;
            Epsilon.CurrentUnitValue = 1;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);
        }

        [Test]
        public void TestConditionals04()
        {
            var aLessThanB = Alpha.LessThan(Beta);
            var bLessThanG = Beta.LessThan(Gamma);
            var gLessThanD = Gamma.LessThan(Delta);
            var dLessThanE = Delta.LessThan(Epsilon);
            var eLessThanZ = Epsilon.LessThan(Zeta);

            var conditional = Constraints.If(aLessThanB).Then(bLessThanG).ElseIf(gLessThanD).Then(dLessThanE).Else(eLessThanZ);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 3;

            var result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 1;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;
            Delta.CurrentUnitValue = 2;
            Epsilon.CurrentUnitValue = 3;

            result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 1;
            Delta.CurrentUnitValue = 2;
            Epsilon.CurrentUnitValue = 1;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 3;
            Delta.CurrentUnitValue = 2;
            Epsilon.CurrentUnitValue = 3;
            Zeta.CurrentUnitValue = 4;

            result = conditional.Validate();

            Assert.IsTrue(result.Status);

            Alpha.CurrentUnitValue = 3;
            Beta.CurrentUnitValue = 2;
            Gamma.CurrentUnitValue = 3;
            Delta.CurrentUnitValue = 2;
            Epsilon.CurrentUnitValue = 3;
            Zeta.CurrentUnitValue = 2;

            result = conditional.Validate();

            Assert.IsFalse(result.Status);
        }
    }
}