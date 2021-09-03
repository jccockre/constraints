
using System;

namespace AnalogDevices.Constraints
{
    internal sealed class CustomUnitaryConstraint<P> : ConstraintBase
    {
        public ParameterBase<P> Parameter { get; }
        public Func<ParameterBase<P>, ValidationResult> Validator { get; }
        public CustomUnitaryConstraint(ParameterBase<P> parameter, Func<ParameterBase<P>, ValidationResult> validator) 
        {
            Parameter = parameter;
            Validator = validator;
            Parameter.AddSubscriber(this);
        }

        public override ValidationResult Validate()
        {
            return Validator(Parameter);
        }
    }
}
