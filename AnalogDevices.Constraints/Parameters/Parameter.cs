using System;
using EngineeringUnits;

namespace AnalogDevices.Constraints.Parameters
{
    public static class Parameter
    {
        #region --- Constant ---

        public static QuantitativeParameterBase<Q> Constant<Q>(string name, Q value)
            where Q : BaseUnit, new()
        {
            return new ConstantQuantitativeParameter<Q>(name, value);
        }

        public static QuantitativeParameterBase<Q> Constant<Q>(Q value)
            where Q : BaseUnit, new()
        {
            return Constant<Q>(value.ToString(), value);
        }

        public static QuantitativeParameterBase<Q> Constant<Q>(string name, double value)
            where Q : BaseUnit, new()
        {
            return new ConstantQuantitativeParameter<Q>(name, value);
        }

        public static QuantitativeParameterBase<Q> Constant<Q>(double value)
            where Q : BaseUnit, new()
        {
            return new ConstantQuantitativeParameter<Q>(value.ToString(), value);
        }

        public static QuantitativeParameterBase<Q> Constant<Q, E>(double value, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            var result = BaseUnitExtensions.From<Q, E>(value, unit);
            return new ConstantQuantitativeParameter<Q>(result.ToString(), result);
        }

        #endregion

        #region --- Absolute Value ---

        public static QuantitativeParameterBase<Q> AbsoluteValue<Q>(this QuantitativeParameterBase<Q> self)
            where Q : BaseUnit, new()
        {
            return new AbsoluteValueParameter<Q>(self);
        }

        #endregion

        #region --- Addition and Subtraction ---

        public static QuantitativeParameterBase<Q> Plus<Q>(this QuantitativeParameterBase<Q> summand1, QuantitativeParameterBase<Q> summand2) 
            where Q : BaseUnit, new()
        {
            return new SumParameter<Q>(summand1, summand2);
        }

        public static QuantitativeParameterBase<Q> Plus<Q>(this QuantitativeParameterBase<Q> summand1, Q summand2)
            where Q : BaseUnit, new()
        {
            return new SumParameter<Q>(summand1, Constant<Q>(summand2));
        }

        public static QuantitativeParameterBase<Q> Plus<Q>(this QuantitativeParameterBase<Q> summand1, double summand2)
            where Q : BaseUnit, new()
        {
            return new SumParameter<Q>(summand1, Constant<Q>(summand2));
        }

        public static QuantitativeParameterBase<Q> Minus<Q>(this QuantitativeParameterBase<Q> minuend, QuantitativeParameterBase<Q> subtrahend)
            where Q : BaseUnit, new()
        {
            return new DifferenceParameter<Q>(minuend, subtrahend);
        }

        public static QuantitativeParameterBase<Q> Minus<Q>(this QuantitativeParameterBase<Q> minuend, Q subtrahend)
            where Q : BaseUnit, new()
        {
            return new DifferenceParameter<Q>(minuend, Constant<Q>(subtrahend));
        }

        public static QuantitativeParameterBase<Q> Minus<Q>(this QuantitativeParameterBase<Q> minuend, double subtrahend)
            where Q : BaseUnit, new()
        {
            return new DifferenceParameter<Q>(minuend, Constant<Q>(subtrahend));
        }

        #endregion

        #region --- Multiplication ---

        public static QuantitativeParameterBase<P> Times<P, Q, R>(this QuantitativeParameterBase<Q> multiplicand, QuantitativeParameterBase<R> multiplier)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return new ProductParameter<P, Q, R>(multiplicand, multiplier);
        }

        public static QuantitativeParameterBase<BaseUnit> Times<Q, R>(this QuantitativeParameterBase<Q> multiplicand, QuantitativeParameterBase<R> multiplier)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return Times<BaseUnit, Q, R>(multiplicand, multiplier);
        }

        public static QuantitativeParameterBase<P> Times<P, Q, R>(this QuantitativeParameterBase<Q> multiplicand, R multiplier)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return Times<P, Q, R>(multiplicand, Constant<R>(multiplier));
        }

        public static QuantitativeParameterBase<BaseUnit> Times<Q, R>(this QuantitativeParameterBase<Q> multiplicand, R multiplier)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return Times<BaseUnit, Q, R>(multiplicand, multiplier);
        }

        public static QuantitativeParameterBase<P> Times<P, Q, R>(this Q multiplicand, R multiplier)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return Times<P, Q, R>(Constant<Q> (multiplicand), Constant<R>(multiplier));
        }

        public static QuantitativeParameterBase<BaseUnit> Times<Q, R>(this Q multiplicand, R multiplier)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return Times<BaseUnit, Q, R>(multiplicand, multiplier);
        }

        public static QuantitativeParameterBase<P> Times<P, Q, R>(this QuantitativeParameterBase<Q> multiplicand, double multiplier)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return Times<P, R, Q>(Constant<R>(multiplier), multiplicand);
        }

        public static QuantitativeParameterBase<BaseUnit> Times<Q, R>(this QuantitativeParameterBase<Q> multiplicand, double multiplier)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return Times<BaseUnit, R, Q>(Constant<R>(multiplier), multiplicand);
        }

        #endregion

        #region --- Division ---

        public static QuantitativeParameterBase<P> DividedBy<P, Q, R>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<R> divisor)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return new QuotientParameter<P, Q, R>(dividend, divisor);
        }

        public static QuantitativeParameterBase<BaseUnit> DividedBy<Q, R>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<R> divisor)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<BaseUnit, Q, R>(dividend, divisor);
        }

        public static QuantitativeParameterBase<P> DividedBy<P, Q, R>(this QuantitativeParameterBase<Q> dividend, R divisor)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<P, Q, R>(dividend, Constant<R>(divisor));
        }

        public static QuantitativeParameterBase<BaseUnit> DividedBy<Q, R>(this QuantitativeParameterBase<Q> dividend, R divisor)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<BaseUnit, Q, R>(dividend, Constant<R>(divisor));
        }

        public static QuantitativeParameterBase<P> DividedBy<P, Q, R>(this QuantitativeParameterBase<Q> dividend, double divisor)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<P, Q, R>(dividend, Constant<R>(divisor));
        }

        public static QuantitativeParameterBase<BaseUnit> DividedBy<Q, R>(this QuantitativeParameterBase<Q> dividend, double divisor)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<BaseUnit, Q, R>(dividend, Constant<R>(divisor));
        }

        public static QuantitativeParameterBase<P> DividedBy<P, Q, R>(this Q dividend, QuantitativeParameterBase<R> divisor)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<P, Q, R>(Constant<Q>(dividend), divisor);
        }

        public static QuantitativeParameterBase<BaseUnit> DividedBy<Q, R>(this Q dividend, QuantitativeParameterBase<R> divisor)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<BaseUnit, Q, R>(Constant<Q>(dividend), divisor);
        }

        public static QuantitativeParameterBase<P> DividedBy<P, Q, R>(this Q dividend, R divisor)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<P, Q, R>(Constant<Q>(dividend), Constant<R>(divisor));
        }

        public static QuantitativeParameterBase<BaseUnit> DividedBy<Q, R>(this Q dividend, R divisor)
            where Q : BaseUnit, new()
            where R : BaseUnit, new()
        {
            return DividedBy<BaseUnit, Q, R>(Constant<Q>(dividend), Constant<R>(divisor));
        }

        #endregion

        #region --- Exponents and Logarithms ---

        public static QuantitativeParameterBase<P> ToThePowerOf<P, Q>(this QuantitativeParameterBase<Q> baseB, QuantitativeParameterBase<BaseUnit> exponent)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
        {
            return new PowerParameter<P, Q>(baseB, exponent);
        }

        public static QuantitativeParameterBase<BaseUnit> ToThePowerOf<Q>(this QuantitativeParameterBase<Q> baseB, QuantitativeParameterBase<BaseUnit> exponent)
            where Q : BaseUnit, new()
        {
            return new PowerParameter<BaseUnit, Q>(baseB, exponent);
        }

        public static QuantitativeParameterBase<P> ToThePowerOf<P, Q>(this QuantitativeParameterBase<Q> baseB, BaseUnit exponent)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
        {
            return ToThePowerOf<P, Q>(baseB, Constant<BaseUnit>(exponent));
        }

        public static QuantitativeParameterBase<BaseUnit> ToThePowerOf<Q>(this QuantitativeParameterBase<Q> baseB, BaseUnit exponent)
            where Q : BaseUnit, new()
        {
            return ToThePowerOf<BaseUnit, Q>(baseB, Constant<BaseUnit>(exponent));
        }

        public static QuantitativeParameterBase<P> ToThePowerOf<P, Q>(this Q baseB, BaseUnit exponent)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
        {
            return ToThePowerOf<P, Q>(Constant<Q>(baseB), Constant<BaseUnit>(exponent));
        }

        public static QuantitativeParameterBase<BaseUnit> ToThePowerOf<Q>(this Q baseB, BaseUnit exponent)
            where Q : BaseUnit, new()
        {
            return ToThePowerOf<BaseUnit, Q>(Constant<Q>(baseB), Constant<BaseUnit>(exponent));
        }

        public static QuantitativeParameterBase<P> ToThePowerOf<P, Q>(this QuantitativeParameterBase<Q> baseB, double exponent)
            where P : BaseUnit, new()
            where Q : BaseUnit, new()
        {
            return ToThePowerOf<P, Q>(baseB, Constant<BaseUnit>(exponent));
        }

        public static QuantitativeParameterBase<BaseUnit> ToThePowerOf<Q>(this QuantitativeParameterBase<Q> baseB, double exponent)
            where Q : BaseUnit, new()
        {
            return ToThePowerOf<BaseUnit, Q>(baseB, Constant<BaseUnit>(exponent));
        }

        #endregion

        #region --- Less Than ---

        public static ConstraintBase LessThanOrEqualTo<Q>(this QuantitativeParameterBase<Q> dividend, Q closedUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumClosed<Q>(dividend, closedUpperBound);
        }

        public static ConstraintBase LessThanOrEqualTo<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> closedUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumClosed<Q>(dividend, closedUpperBound);
        }

        public static ConstraintBase LessThanOrEqualTo<Q>(this QuantitativeParameterBase<Q> dividend, double closedUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumClosed<Q>(dividend, closedUpperBound);
        }

        public static ConstraintBase LessThanOrEqualTo<Q, E>(this QuantitativeParameterBase<Q> dividend, double closedUpperBound, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return Constraints.MaximumClosed<Q>(dividend, BaseUnitExtensions.From<Q, E>(closedUpperBound, unit));
        }

        public static ConstraintBase LessThan<Q>(this QuantitativeParameterBase<Q> dividend, Q openUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumOpen<Q>(dividend, openUpperBound);
        }

        public static ConstraintBase LessThan<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> openUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumOpen<Q>(dividend, openUpperBound);
        }

        public static ConstraintBase LessThan<Q>(this QuantitativeParameterBase<Q> dividend, double openUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumOpen<Q>(dividend, openUpperBound);
        }

        public static ConstraintBase LessThan<Q, E>(this QuantitativeParameterBase<Q> dividend, double openUpperBound, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return Constraints.MaximumOpen<Q>(dividend, BaseUnitExtensions.From<Q, E>(openUpperBound, unit));
        }

        public static ConstraintBase AtMost<Q>(this QuantitativeParameterBase<Q> dividend, Q closedUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumClosed<Q>(dividend, closedUpperBound);
        }

        public static ConstraintBase AtMost<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> closedUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumClosed<Q>(dividend, closedUpperBound);
        }

        public static ConstraintBase AtMost<Q>(this QuantitativeParameterBase<Q> dividend, double closedUpperBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MaximumClosed<Q>(dividend, closedUpperBound);
        }

        public static ConstraintBase AtMost<Q, E>(this QuantitativeParameterBase<Q> dividend, double closedUpperBound, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return Constraints.MaximumClosed<Q>(dividend, BaseUnitExtensions.From<Q, E>(closedUpperBound, unit));
        }

        #endregion

        #region --- Greater Than ---

        public static ConstraintBase GreaterThanOrEqualTo<Q>(this QuantitativeParameterBase<Q> dividend, Q closedLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumClosed<Q>(dividend, closedLowerBound);
        }

        public static ConstraintBase GreaterThanOrEqualTo<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> closedLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumClosed<Q>(dividend, closedLowerBound);
        }

        public static ConstraintBase GreaterThanOrEqualTo<Q>(this QuantitativeParameterBase<Q> dividend, double closedLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumClosed<Q>(dividend, closedLowerBound);
        }

        public static ConstraintBase GreaterThanOrEqualTo<Q, E>(this QuantitativeParameterBase<Q> dividend, double closedLowerBound, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return Constraints.MinimumClosed<Q>(dividend, BaseUnitExtensions.From<Q, E>(closedLowerBound, unit));
        }

        public static ConstraintBase GreaterThan<Q>(this QuantitativeParameterBase<Q> dividend, Q openLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumOpen<Q>(dividend, openLowerBound);
        }

        public static ConstraintBase GreaterThan<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> openLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumOpen<Q>(dividend, openLowerBound);
        }

        public static ConstraintBase GreaterThan<Q>(this QuantitativeParameterBase<Q> dividend, double openLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumOpen<Q>(dividend, openLowerBound);
        }

        public static ConstraintBase GreaterThan<Q, E>(this QuantitativeParameterBase<Q> dividend, double openLowerBound, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return Constraints.MinimumOpen<Q>(dividend, BaseUnitExtensions.From<Q, E>(openLowerBound, unit));
        }

        public static ConstraintBase AtLeast<Q>(this QuantitativeParameterBase<Q> dividend, Q closedLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumClosed<Q>(dividend, closedLowerBound);
        }

        public static ConstraintBase AtLeast<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> closedLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumClosed<Q>(dividend, closedLowerBound);
        }

        public static ConstraintBase AtLeast<Q>(this QuantitativeParameterBase<Q> dividend, double closedLowerBound)
            where Q : BaseUnit, new()
        {
            return Constraints.MinimumClosed<Q>(dividend, closedLowerBound);
        }

        public static ConstraintBase AtLeast<Q, E>(this QuantitativeParameterBase<Q> dividend, double closedLowerBound, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return Constraints.MinimumClosed<Q>(dividend, BaseUnitExtensions.From<Q, E>(closedLowerBound, unit));
        }

        #endregion

        #region --- Modular Arithmetic ---

        public static ConstraintBase IsMultipleOf<Q>(this QuantitativeParameterBase<Q> dividend, BaseUnit divisor)
            where Q : BaseUnit, new()
        {
            return Constraints.ModuloZero(dividend, divisor);
        }

        public static ConstraintBase IsMultipleOf<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> divisor)
            where Q : BaseUnit, new()
        {
            return Constraints.ModuloZero(dividend, divisor);
        }

        public static ConstraintBase IsMultipleOf<Q>(this QuantitativeParameterBase<Q> dividend, double divisor)
            where Q : BaseUnit, new()
        {
            return Constraints.ModuloZero(dividend, new BaseUnit(divisor, dividend.Value.Unit));
        }

        public static ConstraintBase IsMultipleOf<Q, E>(this QuantitativeParameterBase<Q> dividend, double divisor, E unit)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return Constraints.ModuloZero(dividend, BaseUnitExtensions.From<Q, E>(divisor, unit));
        }

        public static ConstraintBase IsMultipleOf<Q>(this QuantitativeParameterBase<Q> dividend, BaseUnit divisor, BaseUnit tolerance)
            where Q : BaseUnit, new()
        {
            return Constraints.ModuloZero(dividend, Constant<Q>((Q)tolerance), divisor);
        }

        public static ConstraintBase IsMultipleOf<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> divisor, QuantitativeParameterBase<Q> tolerance)
            where Q : BaseUnit, new()
        {
            return Constraints.ModuloZero(dividend, tolerance, divisor);
        }

        public static ConstraintBase IsMultipleOf<Q>(this QuantitativeParameterBase<Q> dividend, QuantitativeParameterBase<Q> divisor, Q tolerance)
            where Q : BaseUnit, new()
        {
            return IsMultipleOf(dividend, divisor, Constant<Q>(tolerance));
        }

        public static ConstraintBase IsMultipleOf<Q>(this QuantitativeParameterBase<Q> dividend, double divisor, double tolerance)
            where Q : BaseUnit, new()
        {
            return IsMultipleOf(dividend, new BaseUnit(divisor, dividend.Value.Unit), new BaseUnit(divisor, dividend.Value.Unit));
        }

        public static ConstraintBase IsMultipleOf<Q, E>(this QuantitativeParameterBase<Q> dividend, double divisor, E unit, double tolerance)
            where Q : BaseUnit, new()
            where E : Enumeration
        {
            return IsMultipleOf(dividend, BaseUnitExtensions.From<Q, E>(divisor, unit), BaseUnitExtensions.From<Q, E>(tolerance, unit));
        }

        #endregion

        #region --- Enumerations ---

        public static ConstraintBase IsOneOf<K>(this EnumParameter<K> parameter, params K[] goodValues) where K : struct, IConvertible
        {
            return Constraints.OneOf(parameter, goodValues);
        }

        public static ConstraintBase IsOneOf<K>(this EnumParameter<K> parameter, params EnumParameter<K>[] goodValues) where K : struct, IConvertible
        {
            return Constraints.OneOf(parameter, goodValues);
        }

        public static ConstraintBase IsNotAnyOf<K>(this EnumParameter<K> parameter, params K[] badValues) where K : struct, IConvertible
        {
            return Constraints.NotAnyOf(parameter, badValues);
        }

        public static ConstraintBase IsNotAnyOf<K>(this EnumParameter<K> parameter, params EnumParameter<K>[] badValues) where K : struct, IConvertible
        {
            return Constraints.NotAnyOf(parameter, badValues);
        }

        #endregion
    }
}
