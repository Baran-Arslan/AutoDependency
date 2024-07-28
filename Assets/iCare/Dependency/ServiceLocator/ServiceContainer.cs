using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("iCare.Core.Tests")]

namespace iCare
{
    public sealed class ServiceContainer
    {
        readonly Dictionary<Type, object> _services = new();
        readonly Dictionary<string, Dictionary<Type, object>> _servicesByName = new();

        public T Resolve<T>()
        {
            return (T)ResolveInternal(typeof(T), null);
        }

        public T Resolve<T>(string id)
        {
            return (T)ResolveInternal(typeof(T), id);
        }

        public object Resolve(Type serviceType)
        {
            return ResolveInternal(serviceType, null);
        }

        object ResolveInternal(Type serviceType, string id)
        {
            object service;
            if (id == null)
            {
                if (!_services.TryGetValue(serviceType, out service))
                    throw new InvalidOperationException(
                        $"Service of type {serviceType.Highlight()} is not registered.");
            }
            else
            {
                if (!_servicesByName.TryGetValue(id, out var services) ||
                    !services.TryGetValue(serviceType, out service))
                    throw new InvalidOperationException(
                        $"Service of type {serviceType.Highlight()} with ID '{id.Highlight()}' is not registered.");
            }

            if (service == null)
                throw new InvalidOperationException($"Service of type {serviceType.Highlight()} is not registered.");

            return service is Func<object> func ? func() : service;
        }

        public void Register<T>(T service)
        {
            RegisterInternal(service, typeof(T), null);
        }

        public void Register(object service, Type serviceType)
        {
            RegisterInternal(service, serviceType, null);
        }

        public void Register<T>(T service, string id)
        {
            RegisterInternal(service, typeof(T), id);
        }

        public void Register(object service, Type serviceType, string id)
        {
            RegisterInternal(service, serviceType, id);
        }

        public void RegisterFactory<T>(Func<T> service)
        {
            RegisterInternal(service, typeof(T), null);
        }

        public void RegisterFactory(Func<object> service, Type serviceType)
        {
            RegisterInternal(service, serviceType, null);
        }

        void RegisterInternal(object service, Type serviceType, string id)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

            if (id == null)
            {
                if (!_services.TryAdd(serviceType, service))
                    throw new InvalidOperationException(
                        $"Service of type {serviceType.Highlight()} is already registered.");
            }
            else
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentException("Register ID cannot be null or empty.", nameof(id));

                if (!_servicesByName.TryGetValue(id, out var services))
                {
                    services = new Dictionary<Type, object>();
                    _servicesByName[id] = services;
                }

                if (!services.TryAdd(serviceType, service))
                    throw new InvalidOperationException(
                        $"Service of type {serviceType.Highlight()} with ID '{id.Highlight()}' is already registered.");
            }
        }

        public void Clear()
        {
            _services.Clear();
            _servicesByName.Clear();
        }
    }
}