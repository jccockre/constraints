
using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    public class FrequencyParameter : QuantitativeParameterBase<Frequency>
    {
        public FrequencyParameter(string name, double value, FrequencyUnit unit) : base(name)
        {
            Value = new Frequency(value, unit);
        }

        public FrequencyParameter(string name, double value) : this(name, value, FrequencyUnit.Hertz) { }
        public FrequencyParameter(string name) : this(name, 0) { }
    }
}
