using System;
using UnityEngine;

namespace iCare.Di {
    internal sealed class PlainServiceFactory : ServiceFactory {
        public override object CreateService(ServiceAttribute attribute, Type serviceType) {
            var service = Activator.CreateInstance(serviceType);
            Register(service, attribute);
            return service;
        }

        public override bool CanCreate(ServiceAttribute attribute, Type serviceType) {
            return !typeof(MonoBehaviour).IsAssignableFrom(serviceType) &&
                   !typeof(ScriptableObject).IsAssignableFrom(serviceType) &&
                   string.IsNullOrWhiteSpace(attribute.ResourcesPath);
        }
    }
}