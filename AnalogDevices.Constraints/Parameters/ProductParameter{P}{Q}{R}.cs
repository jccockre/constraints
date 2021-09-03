
using EngineeringUnits;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    internal sealed class ProductParameter<P, Q, R> : BinaryCompositeQuantitativeParameter<P, Q, R>
        where P : BaseUnit, new()
        where Q : BaseUnit, new()
        where R : BaseUnit, new()
    {
        public ProductParameter(QuantitativeParameterBase<Q> alpha, QuantitativeParameterBase<R> beta)
            : base(alpha, beta, (x, y) => x * y, (x, y) => $"{x} * {y}") { }
    }
}
