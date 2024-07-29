namespace iCare.Di
{
    public interface IConstruct
    {
    }

    public interface IConstruct<in T> : IConstruct
    {
        void Construct(T value);
    }

    public interface IConstruct<in T1, in T2> : IConstruct
    {
        void Construct(T1 value1, T2 value2);
    }

    public interface IConstruct<in T1, in T2, in T3> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3);
    }

    public interface IConstruct<in T1, in T2, in T3, in T4> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3, T4 value4);
    }

    public interface IConstruct<in T1, in T2, in T3, in T4, in T5> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5);
    }

    public interface IConstruct<in T1, in T2, in T3, in T4, in T5, in T6> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6);
    }

    public interface IConstruct<in T1, in T2, in T3, in T4, in T5, in T6, in T7> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7);
    }

    public interface IConstruct<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8);
    }

    public interface IConstruct<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8,
            T9 value9);
    }

    public interface IConstruct<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10> : IConstruct
    {
        void Construct(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8,
            T9 value9, T10 value10);
    }
}