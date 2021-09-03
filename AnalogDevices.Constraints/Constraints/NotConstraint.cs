
using System;

namespace AnalogDevices.Constraints
{
    internal sealed class NotConstraint : NegatableConstraintBase
    {
        private ConstraintBase inner { get; }
        public NotConstraint(ConstraintBase toNegate)
        {
            if (toNegate is NegatableConstraintBase negatableConstraint)
            {
                inner = negatableConstraint.Negation();
                inner.AddSubscriber(this);
            }
            else
            {
                throw new ArgumentException($"Attempted to negate non-negatable constraint of type {toNegate.GetType().Name}.");
            }
        }

        public override ValidationResult Validate()
        {
            return inner.Validate();
        }

        protected override ConstraintBase negation()
        {
            if (inner is NegatableConstraintBase negatableConstraint)
            {
                return negatableConstraint.Negation();
            }
            else
            {
                throw new ArgumentException($"Attempted to negate non-negatable constraint of type {inner.GetType().Name}.");
            }
        }
    }
}
