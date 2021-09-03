using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using AnalogDevices.Constraints.Parameters;

namespace AnalogDevices.Constraints
{
    internal abstract class EnumConstraint<K> : UnitaryTypedConstraint<EnumParameter<K>, K> where K : struct, IConvertible
    {
        protected EnumConstraint(EnumParameter<K> parameter) : base(parameter, typeof(K)) { }

        protected static string ToString(K val)
        {
            return Regex.Replace(val.ToString(), @"(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }

    internal abstract class EnumIsConstraintBase<K> : EnumConstraint<K> where K : struct, IConvertible
    {
        protected abstract Dictionary<K, string> GoodValues();

        protected EnumIsConstraintBase(EnumParameter<K> parameter) : base(parameter) { }

        public override ValidationResult Validate()
        {
            var goodValues = GoodValues();
            if (goodValues.ContainsKey(Parameter.Value))
            {
                return ValidationResult.OK;
            }
            else
            {
                switch (goodValues.Count)
                {
                    case 0: return new ValidationResult(false, $"No valid values found for {Parameter.Name}", 1);
                    case 1: return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must be {goodValues.Values.ElementAt(0)}", 1);
                    case 2: return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must be {goodValues.Values.ElementAt(0)} or {goodValues.Values.ElementAt(1)}", 1);
                    default: return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must be one of {String.Join(", ", goodValues.Values)}", 1);
                }
            }
        }
    }

    internal sealed class EnumIsConstraintPrimitive<K> : EnumIsConstraintBase<K> where K : struct, IConvertible
    {
        private Dictionary<K, string> goodValues { get; } = new Dictionary<K, string>();
        protected override Dictionary<K, string> GoodValues()
        {
            return goodValues;
        }

        public EnumIsConstraintPrimitive(EnumParameter<K> parameter, IReadOnlyCollection<K> _goodValues) : base(parameter)
        {
            foreach (K val in _goodValues)
            {
                goodValues[val] = ToString(val);
            }
        }

        protected override ConstraintBase negation()
        {
            return new EnumIsNotConstraintPrimitive<K>(Parameter, goodValues.Keys);
        }
    }

    internal sealed class EnumIsConstraintParameter<K> : EnumIsConstraintBase<K> where K : struct, IConvertible
    {
        private IReadOnlyCollection<EnumParameter<K>> goodValues { get; }
        protected override Dictionary<K, string> GoodValues()
        {
            Dictionary<K, string> output = new Dictionary<K, string>();
            goodValues.ToList().ForEach(v =>
            {
                output[v.Value] = $"{v.Name} ({ToString(v.Value)})";
            });
            return output;
        }

        public EnumIsConstraintParameter(EnumParameter<K> parameter, IReadOnlyCollection<EnumParameter<K>> _goodValues) : base(parameter)
        {
            goodValues = _goodValues;
        }

        protected override ConstraintBase negation()
        {
            return new EnumIsNotConstraintParameter<K>(Parameter, goodValues);
        }
    }

    internal abstract class EnumIsNotConstraintBase<K> : EnumConstraint<K> where K : struct, IConvertible
    {
        protected abstract Dictionary<K, string> BadValues();

        protected EnumIsNotConstraintBase(EnumParameter<K> parameter) : base(parameter) { }

        public override ValidationResult Validate()
        {
            var badValues = BadValues();
            if (!badValues.ContainsKey(Parameter.Value))
            {
                return ValidationResult.OK;
            }
            else
            {
                switch (badValues.Count)
                {
                    case 1: return new ValidationResult(false, $"{Parameter.Name} must not be {badValues.Values.ElementAt(0)}", 1);
                    case 2: return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must not be {badValues.Values.ElementAt(0)} or {badValues.Values.ElementAt(1)}", 1);
                    default: return new ValidationResult(false, $"{Parameter.Name} ({Parameter.Value}) must not be any of {String.Join(", ", badValues.Values)}", 1);
                }
            }
        }
    }

    internal sealed class EnumIsNotConstraintPrimitive<K> : EnumIsNotConstraintBase<K> where K : struct, IConvertible
    {
        private Dictionary<K, string> badValues { get; } = new Dictionary<K, string>();
        protected override Dictionary<K, string> BadValues()
        {
            return badValues;
        }

        public EnumIsNotConstraintPrimitive(EnumParameter<K> parameter, IReadOnlyCollection<K> _badValues) : base(parameter)
        {
            foreach (K val in _badValues)
            {
                badValues[val] = ToString(val);
            }
        }

        protected override ConstraintBase negation()
        {
            return new EnumIsConstraintPrimitive<K>(Parameter, badValues.Keys);
        }
    }

    internal sealed class EnumIsNotConstraintParameter<K> : EnumIsNotConstraintBase<K> where K : struct, IConvertible
    {
        private IReadOnlyCollection<EnumParameter<K>> badValues { get; }
        protected override Dictionary<K, string> BadValues()
        {
            Dictionary<K, string> output = new Dictionary<K, string>();
            badValues.ToList().ForEach(v =>
            {
                output[v.Value] = $"{v.Name} ({ToString(v.Value)})";
            });
            return output;
        }

        public EnumIsNotConstraintParameter(EnumParameter<K> parameter, IReadOnlyCollection<EnumParameter<K>> _badValues) : base(parameter)
        {
            badValues = _badValues;
        }

        protected override ConstraintBase negation()
        {
            return new EnumIsConstraintParameter<K>(Parameter, badValues);
        }
    }
}
