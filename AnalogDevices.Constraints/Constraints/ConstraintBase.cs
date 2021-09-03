
namespace AnalogDevices.Constraints
{
    public abstract class ConstraintBase : Broadcaster
    {
        public abstract ValidationResult Validate();
    }
}
