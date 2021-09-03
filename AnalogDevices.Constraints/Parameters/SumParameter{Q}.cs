
using EngineeringUnits;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    internal sealed class SumParameter<Q> : BinaryCompositeQuantitativeParameter<Q, Q, Q>
        where Q : BaseUnit, new()
    {
        public SumParameter(QuantitativeParameterBase<Q> alpha, QuantitativeParameterBase<Q> beta)
            : base(alpha, beta, (x, y) => x + y, (x, y) => $"{x} + {y}") { }
    }
}
