
using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    public class TimeParameter : QuantitativeParameterBase<Duration>
    {
        public TimeParameter(string name, double value, DurationUnit unit) : base(name)
        {
            Value = new Duration(value, unit);
        }

        public TimeParameter(string name, double value) : this(name, value, DurationUnit.Second) { }
        public TimeParameter(string name) : this(name, 0) { }
    }
}
