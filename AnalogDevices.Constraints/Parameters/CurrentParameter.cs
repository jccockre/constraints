
using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    public class CurrentParameter : QuantitativeParameterBase<ElectricCurrent>
    {
        public CurrentParameter(string name, double value, ElectricCurrentUnit unit) : base(name)
        {
            Value = new ElectricCurrent(value, unit);
        }

        public CurrentParameter(string name, double value) : this(name, value, ElectricCurrentUnit.Ampere) { }
        public CurrentParameter(string name) : this(name, 0) { }
    }
}
