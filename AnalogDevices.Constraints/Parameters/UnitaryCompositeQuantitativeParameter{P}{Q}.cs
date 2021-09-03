
using System;

using EngineeringUnits;
using EngineeringUnits.Units;

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
                dynamic resultUncast = Computation(Alpha.Value);
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
