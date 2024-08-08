using System;
using UnityEngine;

namespace iCare.Di {
    internal sealed class ScriptableObjectServiceFactory : ServiceFactory {
        public override object CreateService(ServiceAttribute attribute, Type serviceType) {
            var service = ScriptableObject.CreateInstance(serviceType);
            Register(service, attribute);
            return service;
        }

        public override bool CanCreate(ServiceAttribute attribute, Type serviceType) {
            return typeof(ScriptableObject).IsAssignableFrom(serviceType);
        }
    }
}