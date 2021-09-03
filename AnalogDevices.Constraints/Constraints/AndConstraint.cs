
namespace AnalogDevices.Constraints
{
    internal sealed class AndConstraint : BinaryCompositeConstraint
    {
        public AndConstraint(ConstraintBase alpha, ConstraintBase beta) : base(alpha, beta) { }

        public override ValidationResult Validate()
        {
            ValidationResult alpha = Alpha.Validate();
            ValidationResult beta = Beta.Validate();

            if (alpha.Status)
            {
                return beta;
            }

            if (beta.Status)
            {
                return alpha;
            }

            return new ValidationResult(false, parenthesize(alpha) + ", and " + parenthesize(beta), depth(alpha, beta));
        }

        protected override ConstraintBase negation()
        {
            return Constraints.Or(Alpha.Not(), Beta.Not());
        }
    }
}
