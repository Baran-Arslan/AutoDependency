using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace iCare {
    public static class TypeExtensions {
        public static bool InheritsOrImplements(this Type type, Type targetType) {
            if (type == targetType || (type.IsClass && type.IsSubclassOf(targetType)))
                return true;

            return targetType.IsInterface && type.GetInterfaces().Contains(targetType);
        }
        
        public static IEnumerable<Type> FindAllTypesOf<T>() {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.InheritsOrImplements(typeof(T)));
        }

        public static bool TryResolveGenericType(this Type type, out Type resolvedType) {
            if (type.IsGenericType) {
                resolvedType = type.GetGenericArguments()[0];
                return true;
            }

            resolvedType = null;
            return false;
        }

        public static IEnumerable<Type> GetTypesUntilBase([DisallowNull] this Type type, [DisallowNull] Type baseType,
            bool includeInterfaces = true) {
#if DEBUG
            if (type.IsInterface)
                throw new ArgumentException("Type cannot be an interface.", nameof(type));

            if (!baseType.IsAssignableFrom(type))
                throw new ArgumentException("Base type must be a base class of the given type.", nameof(baseType));
#endif

            var currentType = type;
            while (currentType != baseType) {
                if (includeInterfaces)
                    foreach (var @interface in currentType!.GetInterfaces())
                        yield return @interface;

                yield return currentType;

                currentType = currentType!.BaseType;
            }
        }
    }
}