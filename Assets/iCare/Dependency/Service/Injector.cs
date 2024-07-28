using System;
using System.Diagnostics;
using System.Reflection;

namespace iCare
{
    public static class Injector
    {
        public static void GlobalConstruct(object service, Type serviceType = null)
        {
            serviceType ??= service.GetType();


            const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var constructMethod = serviceType.GetMethod("Construct", FLAGS);

            ValidateConstructMethod(serviceType, constructMethod);

            var methodParams = constructMethod!.GetParameters();
            var paramValues = new object[methodParams.Length];
            for (var i = 0; i < methodParams.Length; i++)
            {
                var paramType = methodParams[i].ParameterType;
                paramValues[i] = ServiceLocator.Global.Resolve(paramType);
            }

            constructMethod.Invoke(service, paramValues);
        }

        [Conditional("DEBUG")]
        static void ValidateConstructMethod(Type serviceType, MethodInfo constructMethod)
        {
            if (constructMethod == null)
                throw new InvalidOperationException($"Construct method not found in {serviceType.Highlight()}.");
        }
    }
}