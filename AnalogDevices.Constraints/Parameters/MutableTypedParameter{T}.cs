
namespace AnalogDevices.Constraints.Parameters
{
    public abstract class MutableTypedParameter<T> : ParameterBase<T>
    {
        protected MutableTypedParameter(string name, T value) : base(name)
        {
            _value = value;
        }

        protected MutableTypedParameter(string name) : this(name, default(T)) { }

        public override T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (null == _value || !_value.Equals(value))
                {
                    _value = value;
                    NotifyChanged();
                }
            }
        }
        private T _value;
    }
}