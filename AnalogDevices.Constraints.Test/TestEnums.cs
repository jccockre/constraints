
using NUnit.Framework;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints.Test
{
    public enum Animals { Cat, Dog, Pig, Cow, Fox, Hen, Rat, Fly, Gryffin, Centaur, Charizard, ManBearPig }
    [TestFixture]
    public class TestEnums
    {
        public EnumParameter<Animals> Pet = new EnumParameter<Animals>("Pet");
        public EnumParameter<Animals> Nemesis = new EnumParameter<Animals>("Nemesis");
        public EnumParameter<Animals> Sidekick = new EnumParameter<Animals>("Sidekick");

        [Test]
        public void TestEnums01()
        {
            var constraint1 = Pet.IsOneOf(Animals.Cat, Animals.Dog, Animals.Rat);

            Pet.Value = Animals.Dog;

            var result1 = constraint1.Validate();

            Assert.IsTrue(result1.Status);

            Pet.Value = Animals.Fox;

            result1 = constraint1.Validate();

            Assert.IsFalse(result1.Status);

            var constraint2 = constraint1.Not();

            var result2 = constraint2.Validate();

            Assert.IsTrue(result2.Status);

            Pet.Value = Animals.Cat;

            result2 = constraint2.Validate();

            Assert.IsFalse(result2.Status);
        }

        [Test]
        public void TestEnums02()
        {
            var constraint1 = Pet.IsOneOf(Animals.Cat, Animals.Dog, Animals.Rat).And(Pet.IsNotAnyOf(Animals.Cat, Animals.Rat));

            Pet.Value = Animals.Dog;

            var result1 = constraint1.Validate();

            Assert.IsTrue(result1.Status);

            Pet.Value = Animals.Fox;

            result1 = constraint1.Validate();

            Assert.IsFalse(result1.Status);

            Pet.Value = Animals.Cat;

            result1 = constraint1.Validate();

            Assert.IsFalse(result1.Status);
        }

        [Test]
        public void TestEnums03()
        {
            var constraint1 = Sidekick.IsOneOf(Animals.Gryffin, Animals.Centaur, Animals.Charizard).And(Sidekick.IsNotAnyOf(Nemesis));

            Sidekick.Value = Animals.Centaur;
            Nemesis.Value = Animals.Centaur;

            var result1 = constraint1.Validate();

            Assert.IsFalse(result1.Status);

            Nemesis.Value = Animals.ManBearPig;

            result1 = constraint1.Validate();

            Assert.IsTrue(result1.Status);

            Sidekick.Value = Animals.ManBearPig;

            result1 = constraint1.Validate();

            Assert.IsFalse(result1.Status);
        }
    }
}
