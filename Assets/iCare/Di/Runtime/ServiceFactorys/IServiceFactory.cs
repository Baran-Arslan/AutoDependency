using System;

namespace iCare.Di {
    internal abstract class ServiceFactory {
        protected static void Register(object instance, ServiceAttribute attribute) {
            foreach (var registerType in attribute.RegisterTypes) ServiceLocator.Global.Register(instance, registerType);
        }

        public abstract object CreateService(ServiceAttribute attribute, Type serviceType);
        public abstract bool CanCreate(ServiceAttribute attribute, Type serviceType);
    }
}