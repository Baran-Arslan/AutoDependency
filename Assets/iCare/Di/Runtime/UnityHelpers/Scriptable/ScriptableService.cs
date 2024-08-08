using UnityEngine;

namespace iCare.Di {
    public abstract class ScriptableService : ScriptableObject { }

    public abstract class ScriptableService<T> : ScriptableService, IConstruct<T> {
        public abstract void Construct(T value);
    }

    public abstract class ScriptableService<T1, T2> : ScriptableService, IConstruct<T1, T2> {
        public abstract void Construct(T1 value1, T2 value2);
    }

    public abstract class ScriptableService<T1, T2, T3> : ScriptableService, IConstruct<T1, T2, T3> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3);
    }

    public abstract class ScriptableService<T1, T2, T3, T4> : ScriptableService, IConstruct<T1, T2, T3, T4> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3, T4 value4);
    }

    public abstract class ScriptableService<T1, T2, T3, T4, T5> : ScriptableService, IConstruct<T1, T2, T3, T4, T5> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5);
    }

    public abstract class ScriptableService<T1, T2, T3, T4, T5, T6> : ScriptableService, IConstruct<T1, T2, T3, T4, T5, T6> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6);
    }

    public abstract class ScriptableService<T1, T2, T3, T4, T5, T6, T7> : ScriptableService, IConstruct<T1, T2, T3, T4, T5, T6, T7> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7);
    }

    public abstract class
        ScriptableService<T1, T2, T3, T4, T5, T6, T7, T8> : ScriptableService, IConstruct<T1, T2, T3, T4, T5, T6, T7, T8> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8);
    }

    public abstract class ScriptableService<T1, T2, T3, T4, T5, T6, T7, T8, T9> : ScriptableService,
        IConstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9);
    }

    public abstract class ScriptableService<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : ScriptableService,
        IConstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> {
        public abstract void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9,
            T10 value10);
    }
}