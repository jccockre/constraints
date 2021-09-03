using System;

namespace AnalogDevices.Constraints
{
    internal abstract class UnitaryTypedConstraint<Q, T> : NegatableConstraintBase where Q : ParameterBase<T>
    {
        protected Q Parameter { get; }
        protected UnitaryTypedConstraint(Q parameter, Type requiredDataType)
        {
            if (requiredDataType.IsAssignableFrom(parameter.DataType))
            {
                Parameter = parameter;
                Parameter.AddSubscriber(this);
            }
            else
            {
                throw new ArgumentException($"Attempted to bind parameter with data type of {parameter.DataType.Name} to constraint requiring {requiredDataType.Name}.");
            }
        }
    }
}
