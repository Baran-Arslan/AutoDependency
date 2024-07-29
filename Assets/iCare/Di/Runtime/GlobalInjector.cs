using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using iCare.Di.LoopSystem;

namespace iCare.Di {
    public static class GlobalInjector {
        const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static void TryInject([DisallowNull] object service, Type serviceType = null) {
            serviceType ??= service.GetType();
            
            LoopProvider.Instance.TryRegister(service);
            if (service is IInstaller installer) 
                installer.Install();
            

            var constructMethod = serviceType.GetMethod("Construct", FLAGS);
            if (constructMethod == null) return;
            

            var methodParams = constructMethod!.GetParameters();
            var paramValues = new object[methodParams.Length];
            for (var i = 0; i < methodParams.Length; i++) {
                var paramType = methodParams[i].ParameterType;
                paramValues[i] = ServiceLocator.Global.Resolve(paramType);
            }

            constructMethod.Invoke(service, paramValues);
        }

        [Conditional("DEBUG")]
        static void ValidateConstructMethod(Type serviceType, MethodInfo constructMethod) {
            if (constructMethod == null)
                throw new InvalidOperationException($"Construct method not found in {serviceType.Highlight()}.");
        }
    }
}