using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace iCare {
    public static class TypeExtensions {
        public static IEnumerable<Type> GetTypesUntilBase([DisallowNull] this Type type, [DisallowNull] Type baseType,
            bool includeInterfaces = true) {
#if DEBUG
            if (baseType.IsInterface) {
                throw new ArgumentException("Base type cannot be an interface.", nameof(baseType));
            }

            if (!baseType.IsAssignableFrom(type)) {
                throw new ArgumentException("Base type must be a base class of the given type.", nameof(baseType));
            }

            if (type.IsInterface) {
                throw new ArgumentException("Type cannot be an interface.", nameof(type));
            }
#endif

            var currentType = type;
            while (currentType != baseType) {
                if (includeInterfaces) {
                    foreach (var @interface in currentType!.GetInterfaces()) {
                        yield return @interface;
                    }
                }

                yield return currentType;

                currentType = currentType!.BaseType;
            }
        }
    }
}