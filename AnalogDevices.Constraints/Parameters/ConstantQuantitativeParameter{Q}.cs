
using System;

using EngineeringUnits;
using EngineeringUnits.Units;

namespace AnalogDevices.Constraints.Parameters
{
    internal sealed class ConstantQuantitativeParameter<Q> : ImmutableQuantitativeParameterBase<Q> where Q : BaseUnit, new()
    {
        public ConstantQuantitativeParameter(string name, Q value) : base(name, value) { }
        public ConstantQuantitativeParameter(string name, double scalarValue)
            : base(name, (Q)(Frequency.From(scalarValue, FrequencyUnit.Hertz) / Frequency.From(1, FrequencyUnit.Hertz))) 
        { 
            if (!typeof(Q).Equals(typeof(BaseUnit)))
            {
                throw new NotSupportedException($"Attempted to create a constant scalar with illegal units {typeof(Q).Name}.");
            }
        }
    }
}
