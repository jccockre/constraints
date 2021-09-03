
using System;

using EngineeringUnits;

namespace AnalogDevices.Constraints.Parameters
{
    internal abstract class UnitaryCompositeQuantitativeParameter<P, Q> : QuantitativeParameterBase<P> 
        where P : BaseUnit, new()
        where Q : BaseUnit, new()
    {
        public QuantitativeParameterBase<Q> Alpha { get; }
        public Func<Q, UnknownUnit> Computation { get; }
        public UnitaryCompositeQuantitativeParameter(QuantitativeParameterBase<Q> alpha, Func<Q, UnknownUnit> computation, Func<string, string> writtenExpression)
            : base(writtenExpression(alpha.Name))
        {
            Alpha = alpha;
            Computation = computation;

            Alpha.AddSubscriber(this);
        }

        public override P Value 
        {
            get
            {
                UnknownUnit resultUncast = Computation(Alpha.Value);
                BaseUnit cast = resultUncast.IntelligentCast();
                return (P)cast;
            }
            set { } 
        }
    }
}
