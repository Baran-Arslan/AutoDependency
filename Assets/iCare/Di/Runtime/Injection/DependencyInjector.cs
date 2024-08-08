using System.Collections.Generic;

namespace iCare.Di {
    internal static class DependencyInjector {
        internal static void Inject(IEnumerable<object> objectsList) {
            foreach (var service in objectsList) Inject(service);
        }

        internal static void Inject(object service) {
            Inject(service, ServiceLocator.Global);
        }

        internal static void Inject(object service, ServiceContainer container) {
            var serviceType = service.GetType();

            var constructMethod = ConstructMethodCache.GetConstructMethod(serviceType);
            var constructDelegate = DelegateFactory.CreateDelegate(constructMethod);

            ConstructMethodInvoker.InvokeMethod(constructDelegate, service, container);
        }
    }
}