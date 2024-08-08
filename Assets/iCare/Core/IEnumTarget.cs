using System;

namespace iCare {
    public interface IEnumTarget { }

    public interface IEnumTarget<T> : IEnumTarget where T : Enum { }
}