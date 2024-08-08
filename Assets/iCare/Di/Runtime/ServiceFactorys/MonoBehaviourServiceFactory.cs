using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Di {
    internal sealed class MonoBehaviourServiceFactory : ServiceFactory {
        public override object CreateService(ServiceAttribute attribute, Type serviceType) {
            object service;

            if (attribute.FindFromScene) {
                service = Object.FindAnyObjectByType(serviceType, FindObjectsInactive.Include);
                if (service == null)
                    Debug.LogError($"Service {serviceType} not found in scene.");
            }
            else {
                service = new GameObject().AddComponent(serviceType);
            }

            Register(service, attribute);
            return service;
        }

        public override bool CanCreate(ServiceAttribute attribute, Type serviceType) {
            return typeof(MonoBehaviour).IsAssignableFrom(serviceType);
        }
    }
}