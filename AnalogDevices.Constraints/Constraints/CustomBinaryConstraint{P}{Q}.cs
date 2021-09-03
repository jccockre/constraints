
using System;

namespace AnalogDevices.Constraints
{
    internal sealed class CustomBinaryConstraint<P, Q> : ConstraintBase
    {
        public ParameterBase<P> Alpha { get; }
        public ParameterBase<Q> Beta { get; }
        public Func<ParameterBase<P>, ParameterBase<Q>, ValidationResult> Validator { get; }
        public CustomBinaryConstraint(ParameterBase<P> alpha, ParameterBase<Q> beta, Func<ParameterBase<P>, ParameterBase<Q>, ValidationResult> validator) 
        {
            Alpha = alpha;
            Beta = beta;
            Validator = validator;
            Alpha.AddSubscriber(this);
            Beta.AddSubscriber(this);
        }

        public override ValidationResult Validate()
        {
            return Validator(Alpha, Beta);
        }
    }
}
