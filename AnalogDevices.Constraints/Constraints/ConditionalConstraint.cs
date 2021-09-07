

namespace AnalogDevices.Constraints
{
    public sealed class ConditionalConstraint : ConstraintBase
    {
        public ConstraintBase Antecedent { get; }
        public ConstraintBase Consequent { get; }
        public ConstraintBase Alternate { get; private set; }
        public ConditionalConstraint(ConstraintBase antecedent, ConstraintBase consequent) : base()
        {
            Antecedent = antecedent;
            Consequent = consequent;

            Antecedent.AddSubscriber(this);
            Consequent.AddSubscriber(this);
        }

        public void Destroy()
        {
            Antecedent.RemoveSubscriber(this);
            Consequent.RemoveSubscriber(this);
            Alternate?.RemoveSubscriber(this);
        }

        public void SetAlternate(ConstraintBase alternate)
        {
            if (null != Alternate)
            {
                throw new System.InvalidOperationException("Forbidden to assign Alternate on Conditional Constraint that already has one.");
            }

            Alternate = alternate;
            Alternate.AddSubscriber(this);
        }

        public override ValidationResult Validate()
        {
            var antecedentResult = Antecedent.Validate();
            if (antecedentResult.Status)
            {
                var consequentResult = Consequent.Validate();
                if (consequentResult.Status)
                {
                    return ValidationResult.OK;
                }
                else
                {
                    return new ValidationResult(false, $"While {Antecedent}, {consequentResult.Warning}.", consequentResult.Depth);
                }
            }
            else if (null == Alternate)
            {
                return ValidationResult.OK;
            }
            else
            {
                var alternateResult = Alternate.Validate();
                if (alternateResult.Status)
                {
                    return ValidationResult.OK;
                }
                else
                {
                    return new ValidationResult(false, $"While '{Antecedent}' is false, {alternateResult.Warning}.", alternateResult.Depth);
                }
            }
        }
    }
}
