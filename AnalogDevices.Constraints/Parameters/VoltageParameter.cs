
using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    public class VoltageParameter : QuantitativeParameterBase<ElectricPotential>
    {
        public VoltageParameter(string name, double value, ElectricPotentialUnit unit) : base(name)
        {
            Value = new ElectricPotential(value, unit);
        }

        public VoltageParameter(string name, double value) : this(name, value, ElectricPotentialUnit.Volt) { }
        public VoltageParameter(string name) : this(name, 0) { }
    }
}
