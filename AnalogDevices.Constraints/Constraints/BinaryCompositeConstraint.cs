
using System;

namespace AnalogDevices.Constraints
{
    internal abstract class BinaryCompositeConstraint : NegatableConstraintBase
    {
        public ConstraintBase Alpha { get; }
        public ConstraintBase Beta { get; }

        protected BinaryCompositeConstraint(ConstraintBase alpha, ConstraintBase beta)
        {
            Alpha = alpha;
            Beta = beta;

            Alpha.AddSubscriber(this);
            Beta.AddSubscriber(this);
        }

        protected string parenthesize(ValidationResult result)
        {
            if (1 < result.Depth)
            {
                return "(" + result.Warning + ")";
            }
            else
            {
                return result.Warning;
            }
        }

        protected int depth(ValidationResult alpha, ValidationResult beta)
        {
            return Math.Max(alpha.Depth, beta.Depth) + 1;
        }
    }
}
