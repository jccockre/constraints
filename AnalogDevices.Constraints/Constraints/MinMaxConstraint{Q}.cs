
using EngineeringUnits;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    internal abstract class NumericalBoundConstraintBase<Q> : UnitaryTypedConstraint<QuantitativeParameterBase<Q>, Q>
        where Q : BaseUnit, new()
    {
        protected abstract Q GetBound();
        protected abstract string GetReason();

        protected NumericalBoundConstraintBase(QuantitativeParameterBase<Q> parameter) : base(parameter, typeof(BaseUnit)) { }

        protected string ToParametersUnit(Q other)
        {
            return $"{other.As(Parameter.Value.Unit)} {Parameter.Value.Unit.Symbol ?? Parameter.Value.Unit.ToString()}";
        }
    }

    internal abstract class MinConstraintClosed<Q> : NumericalBoundConstraintBase<Q>
        where Q : BaseUnit, new()
    {
        protected MinConstraintClosed(QuantitativeParameterBase<Q> parameter) : base(parameter) { }

        public override ValidationResult Validate()
        {
            if (Parameter.Value < GetBound())
            {
                return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must be greater than or equal to {GetReason()}", 1);
            }

            return ValidationResult.OK;
        }

        public override string ToString()
        {
            return $"{Parameter} >= {GetReason()}";
        }
    }

    internal abstract class MinConstraintOpen<Q> : NumericalBoundConstraintBase<Q>
        where Q : BaseUnit, new()
    {
        protected MinConstraintOpen(QuantitativeParameterBase<Q> parameter) : base(parameter) { }

        public override ValidationResult Validate()
        {
            if (Parameter.Value <= GetBound())
            {
                return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must be greater than {GetReason()}", 1);
            }

            return ValidationResult.OK;
        }

        public override string ToString()
        {
            return $"{Parameter} > {GetReason()}";
        }
    }

    internal abstract class MaxConstraintClosed<Q> : NumericalBoundConstraintBase<Q>
        where Q : BaseUnit, new()
    {
        protected MaxConstraintClosed(QuantitativeParameterBase<Q> parameter) : base(parameter) { }

        public override ValidationResult Validate()
        {
            if (Parameter.Value > GetBound())
            {
                return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must be less than or equal to {GetReason()}", 1);
            }

            return ValidationResult.OK;
        }

        public override string ToString()
        {
            return $"{Parameter} <= {GetReason()}";
        }
    }

    internal abstract class MaxConstraintOpen<Q> : NumericalBoundConstraintBase<Q>
        where Q : BaseUnit, new()
    {
        protected MaxConstraintOpen(QuantitativeParameterBase<Q> parameter) : base(parameter) { }

        public override ValidationResult Validate()
        {
            if (Parameter.Value >= GetBound())
            {
                return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must be less than {GetReason()}", 1);
            }

            return ValidationResult.OK;
        }

        public override string ToString()
        {
            return $"{Parameter} < {GetReason()}";
        }
    }

    internal sealed class MinConstraintPrimitiveOpen<Q> : MinConstraintOpen<Q>
        where Q : BaseUnit, new()
    {
        private Q Minimum { get; }
        public MinConstraintPrimitiveOpen(QuantitativeParameterBase<Q> parameter, Q minimum) : base(parameter)
        {
            Minimum = minimum;
        }

        protected override Q GetBound()
        {
            return Minimum;
        }

        protected override string GetReason()
        {
            return ToParametersUnit(Minimum);
        }

        protected override ConstraintBase negation()
        {
            return new MaxConstraintPrimitiveClosed<Q>(Parameter, Minimum);
        }
    }

    internal sealed class MaxConstraintPrimitiveOpen<Q> : MaxConstraintOpen<Q>
        where Q : BaseUnit, new()
    {
        private Q Maximum { get; }
        public MaxConstraintPrimitiveOpen(QuantitativeParameterBase<Q> parameter, Q maximum) : base(parameter)
        {
            Maximum = maximum;
        }

        protected override Q GetBound()
        {
            return Maximum;
        }

        protected override string GetReason()
        {
            return ToParametersUnit(Maximum);
        }

        protected override ConstraintBase negation()
        {
            return new MinConstraintPrimitiveClosed<Q>(Parameter, Maximum);
        }
    }

    internal sealed class MinConstraintParameterOpen<Q> : MinConstraintOpen<Q>
        where Q : BaseUnit, new()
    {
        private QuantitativeParameterBase<Q> Minimum { get; }
        public MinConstraintParameterOpen(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> minimum) : base(parameter)
        {
            Minimum = minimum;
        }

        protected override Q GetBound()
        {
            return Minimum.Value;
        }

        protected override string GetReason()
        {
            return $"{Minimum.Name} ({ToParametersUnit(Minimum.Value)})";
        }

        protected override ConstraintBase negation()
        {
            return new MaxConstraintParameterClosed<Q>(Parameter, Minimum);
        }
    }

    internal sealed class MaxConstraintParameterOpen<Q> : MaxConstraintOpen<Q>
        where Q : BaseUnit, new()
    {
        private QuantitativeParameterBase<Q> Maximum { get; }
        public MaxConstraintParameterOpen(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> maximum) : base(parameter)
        {
            Maximum = maximum;
        }

        protected override Q GetBound()
        {
            return Maximum.Value;
        }

        protected override string GetReason()
        {
            return $"{Maximum.Name} ({ToParametersUnit(Maximum.Value)})";
        }

        protected override ConstraintBase negation()
        {
            return new MinConstraintParameterClosed<Q>(Parameter, Maximum);
        }
    }

    internal sealed class MinConstraintPrimitiveClosed<Q> : MinConstraintClosed<Q>
        where Q : BaseUnit, new()
    {
        private Q Minimum { get; }
        public MinConstraintPrimitiveClosed(QuantitativeParameterBase<Q> parameter, Q minimum) : base(parameter)
        {
            Minimum = minimum;
        }

        protected override Q GetBound()
        {
            return Minimum;
        }

        protected override string GetReason()
        {
            return ToParametersUnit(Minimum);
        }

        protected override ConstraintBase negation()
        {
            return new MaxConstraintPrimitiveOpen<Q>(Parameter, Minimum);
        }
    }

    internal sealed class MaxConstraintPrimitiveClosed<Q> : MaxConstraintClosed<Q>
        where Q : BaseUnit, new()
    {
        private Q Maximum { get; }
        public MaxConstraintPrimitiveClosed(QuantitativeParameterBase<Q> parameter, Q maximum) : base(parameter)
        {
            Maximum = maximum;
        }

        protected override Q GetBound()
        {
            return Maximum;
        }

        protected override string GetReason()
        {
            return ToParametersUnit(Maximum);
        }

        protected override ConstraintBase negation()
        {
            return new MinConstraintPrimitiveOpen<Q>(Parameter, Maximum);
        }
    }

    internal sealed class MinConstraintParameterClosed<Q> : MinConstraintClosed<Q>
        where Q : BaseUnit, new()
    {
        private QuantitativeParameterBase<Q> Minimum { get; }
        public MinConstraintParameterClosed(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> minimum) : base(parameter)
        {
            Minimum = minimum;
        }

        protected override Q GetBound()
        {
            return Minimum.Value;
        }

        protected override string GetReason()
        {
            return $"{Minimum.Name} ({ToParametersUnit(Minimum.Value)})";
        }

        protected override ConstraintBase negation()
        {
            return new MaxConstraintParameterOpen<Q>(Parameter, Minimum);
        }
    }

    internal sealed class MaxConstraintParameterClosed<Q> : MaxConstraintClosed<Q>
        where Q : BaseUnit, new()
    {
        private QuantitativeParameterBase<Q> Maximum { get; }
        public MaxConstraintParameterClosed(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> maximum) : base(parameter)
        {
            Maximum = maximum;
        }

        protected override Q GetBound()
        {
            return Maximum.Value;
        }

        protected override string GetReason()
        {
            return $"{Maximum.Name} ({ToParametersUnit(Maximum.Value)})";
        }

        protected override ConstraintBase negation()
        {
            return new MinConstraintParameterOpen<Q>(Parameter, Maximum);
        }
    }
}
