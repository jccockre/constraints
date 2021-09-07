
using System;

using EngineeringUnits;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    public static class Constraints
    {
        public static ConstraintBase And(this ConstraintBase self, ConstraintBase other)
        {
            return new AndConstraint(self, other);
        }

        public static ConstraintBase Or(this ConstraintBase self, ConstraintBase other)
        {
            return new OrConstraint(self, other);
        }

        public static ConstraintBase Not(this ConstraintBase self)
        {
            return new NotConstraint(self);
        }

        public static Antecedent If(ConstraintBase condition)
        {
            return new Antecedent(condition);
        }

        public static ConditionalConstraint Then(this Antecedent self, ConstraintBase consequent)
        {
            var output = new ConditionalConstraint(self.Condition, consequent);
            ConditionalConstraint current = self.Parent;
            if (null == current)
            {
                return output;
            }
            else
            {
                while (null != current.Alternate)
                {
                    current = current.Alternate as ConditionalConstraint;
                }
                current.SetAlternate(output);
                return self.Parent;
            }
        }

        public static ConditionalConstraint Else(this ConditionalConstraint self, ConstraintBase alternate)
        {
            ConditionalConstraint current = self;
            while (null != current.Alternate)
            {
                current = current.Alternate as ConditionalConstraint;
            }
            current.SetAlternate(alternate);
            return self;
        }

        public static Antecedent ElseIf(this ConditionalConstraint self, ConstraintBase condition)
        {
            return new Antecedent(condition, self);
        }

        internal static ConstraintBase MinimumClosed<Q>(QuantitativeParameterBase<Q> parameter, Q minimum)
            where Q : BaseUnit, new()
        {
            return new MinConstraintPrimitiveClosed<Q>(parameter, minimum);
        }

        internal static ConstraintBase MinimumClosed<Q>(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> minimum)
            where Q : BaseUnit, new()
        {
            return new MinConstraintParameterClosed<Q>(parameter, minimum);
        }

        internal static ConstraintBase MinimumClosed<Q>(QuantitativeParameterBase<Q> parameter, double minimum)
            where Q : BaseUnit, new()
        {
            return MinimumClosed(parameter, parameter.Value.Create<Q>(minimum));
        }

        internal static ConstraintBase MinimumOpen<Q>(QuantitativeParameterBase<Q> parameter, Q minimum)
            where Q : BaseUnit, new()
        {
            return new MinConstraintPrimitiveOpen<Q>(parameter, minimum);
        }

        internal static ConstraintBase MinimumOpen<Q>(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> minimum)
            where Q : BaseUnit, new()
        {
            return new MinConstraintParameterOpen<Q>(parameter, minimum);
        }

        internal static ConstraintBase MinimumOpen<Q>(QuantitativeParameterBase<Q> parameter, double minimum)
            where Q : BaseUnit, new()
        {
            return MinimumOpen(parameter, parameter.Value.Create<Q>(minimum));
        }

        internal static ConstraintBase MaximumClosed<Q>(QuantitativeParameterBase<Q> parameter, Q maximum)
            where Q : BaseUnit, new()
        {
            return new MaxConstraintPrimitiveClosed<Q>(parameter, maximum);
        }

        internal static ConstraintBase MaximumClosed<Q>(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> maximum)
            where Q : BaseUnit, new()
        {
            return new MaxConstraintParameterClosed<Q>(parameter, maximum);
        }

        internal static ConstraintBase MaximumClosed<Q>(QuantitativeParameterBase<Q> parameter, double maximum)
            where Q : BaseUnit, new()
        {
            return MaximumClosed(parameter, parameter.Value.Create<Q>(maximum));
        }

        internal static ConstraintBase MaximumOpen<Q>(QuantitativeParameterBase<Q> parameter, Q maximum)
            where Q : BaseUnit, new()
        {
            return new MaxConstraintPrimitiveOpen<Q>(parameter, maximum);
        }

        internal static ConstraintBase MaximumOpen<Q>(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> maximum)
            where Q : BaseUnit, new()
        {
            return new MaxConstraintParameterOpen<Q>(parameter, maximum);
        }

        internal static ConstraintBase MaximumOpen<Q>(QuantitativeParameterBase<Q> parameter, double maximum)
            where Q : BaseUnit, new()
        {
            return MaximumOpen(parameter, parameter.Value.Create<Q>(maximum));
        }

        public static ConstraintBase ModuloZero<Q>(QuantitativeParameterBase<Q> parameter, BaseUnit modulus)
            where Q : BaseUnit, new()
        {
            return new ModuloZeroConstraintPrimitive<Q>(parameter, Parameter.Constant<Q>(parameter.Value.Create<Q>(0)), modulus);
        }

        public static ConstraintBase ModuloZero<Q>(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> tolerance, BaseUnit modulus)
            where Q : BaseUnit, new()
        {
            return new ModuloZeroConstraintPrimitive<Q>(parameter, tolerance, modulus);
        }

        public static ConstraintBase ModuloZero<Q>(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> modulus)
            where Q : BaseUnit, new()
        {
            return new ModuloZeroConstraintParameter<Q>(parameter, Parameter.Constant<Q>(parameter.Value.Create<Q>(0)), modulus);
        }

        public static ConstraintBase ModuloZero<Q>(QuantitativeParameterBase<Q> parameter, QuantitativeParameterBase<Q> tolerance, QuantitativeParameterBase<Q> modulus)
            where Q : BaseUnit, new()
        {
            return new ModuloZeroConstraintParameter<Q>(parameter, tolerance, modulus);
        }

        public static ConstraintBase IsOneOf<K>(EnumParameter<K> parameter, params K[] goodValues) where K : struct, IConvertible
        {
            return new EnumIsConstraintPrimitive<K>(parameter, goodValues);
        }

        public static ConstraintBase IsOneOf<K>(EnumParameter<K> parameter, params EnumParameter<K>[] goodValues) where K : struct, IConvertible
        {
            return new EnumIsConstraintParameter<K>(parameter, goodValues);
        }

        public static ConstraintBase Is<K>(EnumParameter<K> parameter, K goodValue) where K : struct, IConvertible
        {
            return IsOneOf<K>(parameter, new K[] { goodValue });
        }

        public static ConstraintBase Is<K>(EnumParameter<K> parameter, EnumParameter<K> goodValue) where K : struct, IConvertible
        {
            return IsOneOf<K>(parameter, new EnumParameter<K>[] { goodValue });
        }

        public static ConstraintBase IsNotAnyOf<K>(EnumParameter<K> parameter, params K[] badValues) where K : struct, IConvertible
        {
            return new EnumIsNotConstraintPrimitive<K>(parameter, badValues);
        }

        public static ConstraintBase IsNotAnyOf<K>(EnumParameter<K> parameter, params EnumParameter<K>[] badValues) where K : struct, IConvertible
        {
            return new EnumIsNotConstraintParameter<K>(parameter, badValues);
        }

        public static ConstraintBase IsNot<K>(EnumParameter<K> parameter, K badValue) where K : struct, IConvertible
        {
            return IsNotAnyOf<K>(parameter, new K[] { badValue });
        }

        public static ConstraintBase IsNot<K>(EnumParameter<K> parameter, EnumParameter<K> badValue) where K : struct, IConvertible
        {
            return IsNotAnyOf<K>(parameter, new EnumParameter<K>[] { badValue });
        }
    }
}
