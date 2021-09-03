
using System;

namespace AnalogDevices.Constraints
{
    public abstract class ParameterBase<T> : Broadcaster
    {
        public string Name { get; }
        public Type DataType { get; }
        public abstract T Value { get; set; }

        protected ParameterBase(string name)
        {
            Name = name;
            DataType = typeof(T);
        }
    }
}
