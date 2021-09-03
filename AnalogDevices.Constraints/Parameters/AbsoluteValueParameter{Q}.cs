
using EngineeringUnits;

namespace AnalogDevices.Constraints.Parameters
{
    internal sealed class AbsoluteValueParameter<Q> : UnitaryCompositeQuantitativeParameter<Q, Q>
        where Q : BaseUnit, new()
    {
        public AbsoluteValueParameter(QuantitativeParameterBase<Q> parameter)
            : base(parameter, (x) =>
            {
                // TODO: Get rid of all of this logic when EngineeringUnits Absolute Value function is fixed
                if (x.BaseunitValue >= 0)
                {
                    return x;
                }
                else
                {
                    // TODO: Figure how to create constants with base unit value, not SI-prefixed value
                    var foo = ((decimal) x.Value) / x.BaseunitValue;
                    return x.Times(x.Create<Q>((double) (-1 * foo))).Value;
                }
            }, (x) => $"Abs({x})") { }
    }
}
