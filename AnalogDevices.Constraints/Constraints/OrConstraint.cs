
namespace AnalogDevices.Constraints
{
    internal sealed class OrConstraint : BinaryCompositeConstraint
    {
        public OrConstraint(ConstraintBase alpha, ConstraintBase beta) : base(alpha, beta) { }

        public override ValidationResult Validate()
        {
            ValidationResult alpha = Alpha.Validate();
            ValidationResult beta = Beta.Validate();

            if (alpha.Status)
            {
                return alpha;
            }

            if (beta.Status)
            {
                return beta;
            }

            return new ValidationResult(false, parenthesize(alpha) + ", or " + parenthesize(beta), depth(alpha, beta));
        }

        protected override ConstraintBase negation()
        {
            return Constraints.And(Alpha.Not(), Beta.Not());
        }
    }
}
