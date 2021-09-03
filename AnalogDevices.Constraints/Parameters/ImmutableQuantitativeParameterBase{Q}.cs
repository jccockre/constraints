using System;

using EngineeringUnits;

namespace AnalogDevices.Constraints.Parameters
{
    public abstract class ImmutableQuantitativeParameterBase<Q> : QuantitativeParameterBase<Q> where Q : BaseUnit, new()
    {
        protected ImmutableQuantitativeParameterBase(string name) : this(name, default(Q)) { }
        protected ImmutableQuantitativeParameterBase(string name, Q value) : base(name, value) { }
        public override Q Value 
        { 
            get => base.Value;
            set
            {
                throw new NotSupportedException($"Parameter {Name} is immutable.");
            }
        }
        public override double CurrentUnitValue
        {
            get => base.CurrentUnitValue;
            set 
            { 
                throw new NotSupportedException($"Parameter {Name} is immutable."); 
            }
        }
    }
}
