
namespace AnalogDevices.Constraints
{
    internal abstract class NegatableConstraintBase : ConstraintBase
    {
        private ConstraintBase negationConstraint;

        public ConstraintBase Negation()
        {
            if (null == negationConstraint)
            {
                negationConstraint = negation();
            }

            return negationConstraint;
        }

        protected abstract ConstraintBase negation();
    }
}
