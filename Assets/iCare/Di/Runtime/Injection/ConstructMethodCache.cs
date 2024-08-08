using System;
using System.Collections.Generic;
using System.Reflection;

namespace iCare.Di {
    internal static class ConstructMethodCache {
        const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        static readonly Dictionary<Type, MethodInfo> _cache = new();


        internal static MethodInfo GetConstructMethod(Type type) {
            if (_cache.TryGetValue(type, out var methodInfo)) return methodInfo;

            methodInfo = type.GetMethod("Construct", FLAGS);

            if (methodInfo == null)
                throw new InvalidOperationException($"No suitable construct method found for service type {type}");

            _cache[type] = methodInfo;

            return methodInfo;
        }
    }
}