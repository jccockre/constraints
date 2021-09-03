
using EngineeringUnits;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    internal abstract class ModuloConstraint<Q> : UnitaryTypedConstraint<QuantitativeParameterBase<Q>, Q> where Q : BaseUnit, new()
    {
        protected abstract BaseUnit GetModulus();
        protected abstract BaseUnit GetRemainder();
        protected abstract string GetReason();
        public QuantitativeParameterBase<Q> Tolerance { get; }

        protected ModuloConstraint(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> tolerance) : base(parameter, typeof(BaseUnit))
        {
            Tolerance = tolerance;
        }

        public override ValidationResult Validate()
        {
            decimal val = Parameter.Value.BaseunitValue;
            decimal modulus = GetModulus().BaseunitValue;
            decimal remainder = GetRemainder().BaseunitValue;
            decimal tolerance = Tolerance.Value.Abs().baseUnit.BaseunitValue;

            decimal result = val % modulus;
            if (result < remainder - tolerance || result > remainder + tolerance)
            {
                return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}), must {GetReason()}.", 1);
            }

            return ValidationResult.OK;
        }
    }

    internal abstract class ModuloZeroConstraint<Q> : ModuloConstraint<Q> where Q : BaseUnit, new()
    {
        public ModuloZeroConstraint(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> tolerance) : base(parameter, tolerance) { }
        protected override BaseUnit GetRemainder()
        {
            return new BaseUnit(0, Parameter.Value.Unit);
        }
    }

    internal sealed class ModuloZeroConstraintPrimitive<Q> : ModuloZeroConstraint<Q> where Q : BaseUnit, new()
    {
        public BaseUnit Modulus { get; }
        public ModuloZeroConstraintPrimitive(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> tolerance, BaseUnit modulus) 
            : base(parameter, tolerance)
        {
            Modulus = modulus;
        }
        protected override BaseUnit GetModulus()
        {
            return Modulus;
        }
        protected override string GetReason()
        {
            return $"must be an integer multiple of {Modulus}";
        }

        protected override ConstraintBase negation()
        {
            throw new System.NotImplementedException();
        }
    }

    internal sealed class ModuloZeroConstraintParameter<Q> : ModuloZeroConstraint<Q> where Q : BaseUnit, new()
    {
        public QuantitativeParameterBase<Q> Modulus { get; }
        public ModuloZeroConstraintParameter(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> tolerance, QuantitativeParameterBase<Q> modulus) 
            : base(parameter, tolerance)
        {
            Modulus = modulus;
        }
        protected override BaseUnit GetModulus()
        {
            return Modulus.Value;
        }
        protected override string GetReason()
        {
            return $"must be an integer multiple of {Modulus.Name} ({Modulus.Value})";
        }

        protected override ConstraintBase negation()
        {
            throw new System.NotImplementedException();
        }
    }
}
