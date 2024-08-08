using System;
using System.Linq;

namespace iCare.Di {
    internal static class ConstructMethodInvoker {
        internal static void InvokeMethod(Delegate methodDelegate, object service, ServiceContainer container) {
            var methodParams = methodDelegate.Method.GetParameters();
            var paramValues = new object[methodParams.Length];
            for (var i = 0; i < methodParams.Length; i++) {
                var paramType = methodParams[i].ParameterType;
                paramValues[i] = container.Resolve(paramType);
            }

            methodDelegate.DynamicInvoke(new[] { service }.Concat(paramValues).ToArray());
        }
    }
}