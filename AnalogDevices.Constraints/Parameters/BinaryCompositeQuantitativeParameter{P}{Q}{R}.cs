
using System;

using EngineeringUnits;
using EngineeringUnits.Units;

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
                dynamic resultUncast = Computation(Alpha.Value, Beta.Value);
                P newP = new P();
                switch (newP)
                {
                    // TODO: Replace this with IntelligentCast when available - or maybe ditch it entirely
                    case Power p: resultUncast = (Power) resultUncast; break;
                    case ElectricCurrent p: resultUncast = (ElectricCurrent) resultUncast; break;
                    case ElectricPotential p: resultUncast = (ElectricPotential) resultUncast; break;
                    case Frequency p: resultUncast = Frequency.From((double)((BaseUnit)resultUncast).BaseunitValue, FrequencyUnit.Hertz); break;
                    case Duration p: resultUncast = (Duration) resultUncast; break;
                    default: resultUncast = (P) resultUncast; break;
                }
                return resultUncast as P;
            }
            set { } 
        }
    }
}
