using System;

namespace AnalogDevices.Constraints.Parameters
{
    public class EnumParameter<K> : MutableTypedParameter<K> where K : struct, IConvertible
    {
        public EnumParameter(string name) : base(name) { }
        public EnumParameter(string name, K value) : base(name, value) { }
    }
}
