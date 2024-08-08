using System;
using System.Collections.Generic;

namespace iCare {
    public interface IDynamicVariable<T> {
        T Value { get; set; }
        T PreviousValue { get; }
        event Action<T> OnValueChanged;
    }

    public class DynamicVariable<T> : IDynamicVariable<T> {
        T _value;

        public DynamicVariable(T initialValue) {
            _value = initialValue;
        }

        public DynamicVariable() { }

        public T PreviousValue { get; private set; }
        public event Action<T> OnValueChanged;

        public T Value {
            get => _value;
            set {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;
                PreviousValue = _value;
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public static implicit operator T(DynamicVariable<T> variable) {
            return variable.Value;
        }
    }
}