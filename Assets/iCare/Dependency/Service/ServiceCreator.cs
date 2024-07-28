using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare
{
    internal static class ServiceCreator
    {
        static readonly List<(IConstruct, Type)> _injectionTargets = new();
        static LoopProvider _loopProvider;

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        static void ClearList()
        {
            _injectionTargets.Clear();
        }
#endif


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void CreateServices()
        {
            _loopProvider = new GameObject(nameof(LoopProvider)).AddComponent<LoopProvider>();

            foreach (var (serviceAttribute, originalType) in GetTargetClasses())
            {
                var service = CreateService(serviceAttribute, originalType);
                RegisterService(service, originalType, serviceAttribute.RegisterTypes);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        internal static void InjectServices()
        {
            foreach (var (service, serviceType) in _injectionTargets)
                Injector.GlobalConstruct(service, serviceType);
        }

        static object CreateService(ServiceAttribute serviceAttribute, Type serviceType)
        {
            if (!string.IsNullOrWhiteSpace(serviceAttribute.ResourcesPath))
            {
                var service = Resources.Load(serviceAttribute.ResourcesPath, serviceType);
                if (service == null)
                    throw new NullReferenceException(
                        $"Service {serviceType.Highlight()} with path {serviceAttribute.ResourcesPath.Highlight()} not found.");

                if (service is MonoBehaviour monoBehaviour) service = Object.Instantiate(monoBehaviour);

                return service;
            }

            if (typeof(MonoBehaviour).IsAssignableFrom(serviceType))
            {
                if (serviceAttribute.FindFromScene)
                {
                    var sceneService = Object.FindAnyObjectByType(serviceType, FindObjectsInactive.Include);
                    if (sceneService == null) Debug.LogError($"Service {serviceType} not found in scene.");

                    return sceneService;
                }

                var service = new GameObject(serviceType.Name).AddComponent(serviceType);
                Object.DontDestroyOnLoad(service);

                return service;
            }

            if (typeof(ScriptableObject).IsAssignableFrom(serviceType))
            {
                var service = ScriptableObject.CreateInstance(serviceType);
                return service;
            }

            var serviceInstance = Activator.CreateInstance(serviceType);
            return serviceInstance;
        }

        static void RegisterService(object service, Type originalType, Type[] registerTypes)
        {
            foreach (var registerType in registerTypes) ServiceLocator.Global.Register(service, registerType);

            ServiceLocator.Global.Register(service, originalType);
            if (service is IConstruct construct and not MonoBehaviour) _injectionTargets.Add((construct, originalType));

            if (service is IInstaller installer) installer.Install();

            _loopProvider.TryRegister(service);
        }

        static IEnumerable<(ServiceAttribute, Type)> GetTargetClasses()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            foreach (var type in assembly.GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(ServiceAttribute), true);
                foreach (var attribute in attributes) yield return ((ServiceAttribute)attribute, type);
            }
        }
    }
}