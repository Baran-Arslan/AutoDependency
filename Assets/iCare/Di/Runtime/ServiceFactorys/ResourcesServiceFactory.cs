using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Di {
    internal sealed class ResourcesServiceFactory : ServiceFactory {
        public override object CreateService(ServiceAttribute attribute, Type serviceType) {
            var service = Resources.Load(attribute.ResourcesPath, serviceType);
            if (service == null)
                throw new NullReferenceException($"Service {serviceType} with path {attribute.ResourcesPath} not found.");

            if (service is MonoBehaviour monoService)
                service = Object.Instantiate(monoService);

            Register(service, attribute);
            return service;
        }

        public override bool CanCreate(ServiceAttribute attribute, Type serviceType) {
            return !string.IsNullOrWhiteSpace(attribute.ResourcesPath);
        }
    }
}