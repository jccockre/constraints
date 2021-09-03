
using EngineeringUnits;
using EngineeringUnits.Units;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    public class ScalarParameter : QuantitativeParameterBase<BaseUnit>
    {
        public ScalarParameter(string name, double value) : base(name)
        {
            Value = (BaseUnit) (Frequency.From(value, FrequencyUnit.Hertz) / Frequency.From(1, FrequencyUnit.Hertz));
        }
    }
}
