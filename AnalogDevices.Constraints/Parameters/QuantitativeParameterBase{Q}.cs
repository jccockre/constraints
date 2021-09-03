

using EngineeringUnits;

namespace AnalogDevices.Constraints.Parameters
{
    public abstract class QuantitativeParameterBase<Q> : MutableTypedParameter<Q> where Q : BaseUnit, new()
    {
        protected QuantitativeParameterBase(string name): this(name, default(Q)) { }
        protected QuantitativeParameterBase(string name, Q value) : base(name, value) { }

        public virtual double CurrentUnitValue
        {
            get
            {
                return Value.As(Value.Unit);
            }
            set
            {
                if (value != CurrentUnitValue)
                {
                    Value.Transform(new UnknownUnit(value, Value.Unit));
                    NotifyChanged();
                }
            }
        }
    }
}
