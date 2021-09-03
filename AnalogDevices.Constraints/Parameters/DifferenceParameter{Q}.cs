
using EngineeringUnits;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    internal sealed class DifferenceParameter<Q> : BinaryCompositeQuantitativeParameter<Q, Q, Q>
        where Q : BaseUnit, new()
    {
        public DifferenceParameter(QuantitativeParameterBase<Q> alpha, QuantitativeParameterBase<Q> beta)
            : base(alpha, beta, (x, y) => x - y, (x, y) => $"{x} - {y}") { }
    }
}
