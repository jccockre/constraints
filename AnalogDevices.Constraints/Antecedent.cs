
namespace AnalogDevices.Constraints
{
    public sealed class Antecedent
    {
        public ConstraintBase Condition { get; }
        public ConditionalConstraint Parent { get; }
        public Antecedent(ConstraintBase condition)
        {
            Condition = condition;
        }
        public Antecedent(ConstraintBase condition, ConditionalConstraint parent)
            : this(condition)
        {
            Parent = parent;
        }
    }
}
