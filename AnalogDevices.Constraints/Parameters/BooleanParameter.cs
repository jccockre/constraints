
namespace AnalogDevices.Constraints.Parameters
{
    public class BooleanParameter : MutableTypedParameter<bool>
    {
        protected BooleanParameter(string name) : base(name) { }
        protected BooleanParameter(string name, bool value) : base(name, value) { }
    }
}
