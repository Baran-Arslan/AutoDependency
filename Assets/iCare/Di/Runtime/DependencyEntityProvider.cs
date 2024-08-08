using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Di {
    internal static class DependencyEntityProvider {
        static readonly List<ServiceFactory> _serviceFactoryProviders = new() {
            new PlainServiceFactory(),
            new ScriptableObjectServiceFactory(),
            new MonoBehaviourServiceFactory(),
            new ResourcesServiceFactory()
        };

        [ItemCanBeNull]
        internal static IEnumerable<IDependencyEntity> GetAllDependencies() {
            var allDependencyEntities = 
                Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .OfType<IDependencyEntity>().ToList();

            var allScriptableServices = ScriptableServiceManager.GetAll();
            allDependencyEntities.AddRange(allScriptableServices.OfType<IDependencyEntity>());

            allDependencyEntities.AddRange(CreateAndGetServices().OfType<IDependencyEntity>());
            return allDependencyEntities;
        }

        static IEnumerable<object> CreateAndGetServices() {
            foreach (var (serviceAttr, targetType) in FindTypesWithServiceAttributes()) {
                var factory = LocateServiceFactory(serviceAttr, targetType);
                yield return factory.CreateService(serviceAttr, targetType);
            }
        }

        static IEnumerable<(ServiceAttribute, Type)> FindTypesWithServiceAttributes() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            foreach (var type in assembly.GetTypes()) {
                var serviceAttributes = type.GetCustomAttributes(typeof(ServiceAttribute), true);
                foreach (var attribute in serviceAttributes) yield return ((ServiceAttribute)attribute, type);
            }
        }

        static ServiceFactory LocateServiceFactory(ServiceAttribute attribute, Type serviceType) {
            return _serviceFactoryProviders.FirstOrDefault(factory => factory.CanCreate(attribute, serviceType));
        }
    }
}