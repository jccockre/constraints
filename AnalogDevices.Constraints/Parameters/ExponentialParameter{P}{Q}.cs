
using EngineeringUnits;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    internal sealed class ExponentialParameter<P, Q> : BinaryCompositeQuantitativeParameter<P, Q, BaseUnit>
        where P : BaseUnit, new()
        where Q : BaseUnit, new()
    {
        public ExponentialParameter(QuantitativeParameterBase<Q> alpha, QuantitativeParameterBase<BaseUnit> beta)
            : base(alpha, beta, (x, y) => Pow(x, (int)y.BaseunitValue), (x, y) => $"{x}^({y})") { }

        private static UnknownUnit Pow(BaseUnit x, int y)
        {
            var output = x / x;

            while (y > 0)
            {
                output = output * x;
                y--;
            }
            while (y < 0)
            {
                output = output / x;
                y++;
            }

            return output;
        }
    }
}
