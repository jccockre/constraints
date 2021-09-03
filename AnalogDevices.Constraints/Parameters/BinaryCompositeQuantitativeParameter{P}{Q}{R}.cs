
using System;

using EngineeringUnits;

namespace AnalogDevices.Constraints.Parameters
{
    internal abstract class BinaryCompositeQuantitativeParameter<P, Q, R> : QuantitativeParameterBase<P> 
        where P : BaseUnit, new()
        where Q : BaseUnit, new()
        where R : BaseUnit, new()
    {
        public QuantitativeParameterBase<Q> Alpha { get; }
        public QuantitativeParameterBase<R> Beta { get; }
        public Func<BaseUnit, BaseUnit, UnknownUnit> Computation { get; }
        public BinaryCompositeQuantitativeParameter(QuantitativeParameterBase<Q> alpha, QuantitativeParameterBase<R> beta, Func<BaseUnit, BaseUnit, UnknownUnit> computation, Func<string, string, string> writtenExpression)
            : base(writtenExpression(alpha.Name, beta.Name))
        {
            Alpha = alpha;
            Beta = beta;
            Computation = computation;

            Alpha.AddSubscriber(this);
            Beta.AddSubscriber(this);
        }

        public override P Value 
        {
            get
            {
                UnknownUnit resultUncast = Computation(Alpha.Value, Beta.Value);
                BaseUnit cast = resultUncast.IntelligentCast();
                return (P) cast;
            }
            set { } 
        }
    }
}
