
using System.Reflection;

using EngineeringUnits;
using EngineeringUnits.Units;

namespace AnalogDevices.Constraints
{
    public static class BaseUnitExtensions
    {
        public static Q Create<Q>(this Q baseUnit, double value)
            where Q : BaseUnit, new()
        {
            var temp = new BaseUnit(value, baseUnit.Unit);
            Q newQ = new Q();
            if (typeof(BaseUnit).Equals(newQ.GetType()))
            {
                newQ = (Q) (Frequency.From(value, FrequencyUnit.Hertz) / Frequency.From(1, FrequencyUnit.Hertz));
            }
            newQ.Transform(temp);
            return newQ;
        }

        public static Q From<Q, E>(double val, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            MethodInfo from = typeof(Q).GetMethod("From");
            return (Q) from.Invoke(null, new object[] { val, unit });
        }
    }
}
