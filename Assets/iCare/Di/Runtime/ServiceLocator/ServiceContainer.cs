using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEditor;

[assembly: InternalsVisibleTo("iCare.Di.Tests")]

namespace iCare.Di {
    internal static class ServiceKeys {
        static readonly ConcurrentDictionary<string, int> _stringKeyMap = new();
        static readonly ConcurrentDictionary<Type, int> _typeKeyMap = new();

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        static void ClearMaps() {
            _stringKeyMap.Clear();
            _typeKeyMap.Clear();
        }
#endif

        public static int GetServiceKey(this string text) {
            if (string.IsNullOrEmpty(text))
                throw new InvalidOperationException("String key cannot be null or empty!".Highlight());

            if (_stringKeyMap.TryGetValue(text, out var value))
                return value;

            var hash = text.ComputeFNV1aHash();
            _stringKeyMap[text] = hash;
            return hash;
        }

        public static int GetServiceKey([DisallowNull] this Type type) {
            if (_typeKeyMap.TryGetValue(type, out var value))
                return value;

            var hash = type.AssemblyQualifiedName.GetServiceKey();
            _typeKeyMap[type] = hash;
            return hash;
        }

        public static string GetStringVersion(this int key) {
            foreach (var (keyString, keyValue) in _stringKeyMap)
                if (keyValue == key)
                    return keyString;

            throw new InvalidOperationException("Key not found in the map!".Highlight());
        }
    }

    public static class ServiceContainerExtensions {
        public static void Register(this ServiceContainer container, Func<object> service, Type registerType) {
            container.Register(service, registerType.GetServiceKey());
        }

        public static void Register(this ServiceContainer container, Func<object> service, string registerKey) {
            container.Register(service, registerKey.GetServiceKey());
        }

        public static void Register(this ServiceContainer container, object service, Type registerType) {
            container.Register(service, registerType.GetServiceKey());
        }

        public static void Register<T>(this ServiceContainer container, T service, string stringKey) {
            container.Register(service, stringKey.GetServiceKey());
        }

        public static void Register<T>(this ServiceContainer container, T service) {
            container.Register(service, typeof(T).GetServiceKey());
        }

        public static T Resolve<T>(this ServiceContainer container, string stringKey) {
            return container.Resolve<T>(stringKey.GetServiceKey());
        }

        public static T Resolve<T>(this ServiceContainer container) {
            return container.Resolve<T>(typeof(T).GetServiceKey());
        }

        public static object Resolve(this ServiceContainer container, Type serviceType) {
            return container.Resolve(serviceType.GetServiceKey());
        }
    }

    public sealed class ServiceContainer {
        readonly ConcurrentDictionary<int, object> _services = new();

        public void Register([DisallowNull] object service, int key) {
            ValidateKey(key);
            _services[key] = service;
        }

        public object Resolve(int key) {
            ValidateKey(key);
            ValidateKeyExist(key);
            var result = _services[key];

            if (result is Func<object> func)
                return func.Invoke();

            return _services[key];
        }

        public T Resolve<T>(int key) {
            ValidateKey(key);
            ValidateKeyExist(key);
            var result = _services[key];
            if (result is Func<T> func)
                return func.Invoke();

            return (T)result;
        }

        public void RemoveService(object obj) {
            var key = obj.GetType().GetServiceKey();
            if (!_services.TryRemove(key, out _))
                throw new InvalidOperationException("Remove target not found in the container!".Highlight());
        }

        [Conditional("DEBUG")]
        static void ValidateKey(int key) {
            if (key is < 1000 and > -1000)
                throw new InvalidOperationException(
                    "Service key is too small to be valid! Did you convert it to hash correctly?".Highlight());
        }

        [Conditional("DEBUG")]
        void ValidateKeyExist(int key) {
            if (!_services.ContainsKey(key))
                throw new InvalidOperationException(key.GetStringVersion().Highlight() + " not found in the service container!");
        }

        internal void Clear() {
            _services.Clear();
        }
    }
}