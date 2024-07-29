using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare.Di {
    internal static class ServiceCreator {
        static readonly List<(object, Type)> _createdServices = new();
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        static void ClearList() {
            _createdServices.Clear();
        }
#endif


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void CreateServices() {
            foreach (var (serviceAttribute, originalType) in GetTargetClasses()) {
                var service = CreateService(serviceAttribute, originalType);
                RegisterService(service, originalType, serviceAttribute.RegisterTypes);
                _createdServices.Add((service, originalType));
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        internal static void InjectCreatedServices() {
            foreach (var (service, originalType) in _createdServices) {
                if (service is MonoBehaviour) continue;
                GlobalInjector.TryInject(service, originalType);
            }
        }

        static object CreateService(ServiceAttribute serviceAttribute, Type serviceType) {
            if (!string.IsNullOrWhiteSpace(serviceAttribute.ResourcesPath)) {
                return CreateResourcesService(serviceAttribute, serviceType);
            }

            if (typeof(MonoBehaviour).IsAssignableFrom(serviceType)) {
                return CreateMonoBehaviourService(serviceAttribute, serviceType);
            }

            if (typeof(ScriptableObject).IsAssignableFrom(serviceType)) {
                return CreateScriptableObjectService(serviceType);
            }

            return CreatePlainService(serviceType);
        }

        static object CreatePlainService(Type serviceType) {
            var serviceInstance = Activator.CreateInstance(serviceType);
            return serviceInstance;
        }

        static object CreateScriptableObjectService(Type serviceType) {
            var service = ScriptableObject.CreateInstance(serviceType);
            return service;
        }

        static object CreateMonoBehaviourService(ServiceAttribute serviceAttribute, Type serviceType) {
            if (serviceAttribute.FindFromScene) {
                var sceneService = Object.FindAnyObjectByType(serviceType, FindObjectsInactive.Include);
                if (sceneService == null) 
                    Debug.LogError($"Service {serviceType} not found in scene.");
                return sceneService;
            }

            var service = new GameObject(serviceType.Name).AddComponent(serviceType);
            return service;
        }

        static object CreateResourcesService(ServiceAttribute serviceAttribute, Type serviceType) {
            var service = Resources.Load(serviceAttribute.ResourcesPath, serviceType);
            if (service == null)
                throw new NullReferenceException(
                    $"Service {serviceType.Highlight()} with path {serviceAttribute.ResourcesPath.Highlight()} not found.");

            if (service is MonoBehaviour monoBehaviour)
                service = Object.Instantiate(monoBehaviour);

            return service;
        }

        static void RegisterService(object service, Type originalType, Type[] registerTypes) {
            foreach (var registerType in registerTypes)
                ServiceLocator.Global.Register(service, registerType);

            ServiceLocator.Global.Register(service, originalType);
        }

        static IEnumerable<(ServiceAttribute, Type)> GetTargetClasses() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            foreach (var type in assembly.GetTypes()) {
                var attributes = type.GetCustomAttributes(typeof(ServiceAttribute), true);
                foreach (var attribute in attributes) yield return ((ServiceAttribute)attribute, type);
            }
        }
    }
}