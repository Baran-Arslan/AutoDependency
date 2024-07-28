using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace iCare
{
    public static class ServiceLocator
    {
        static readonly Dictionary<Scene, ServiceContainer> _sceneContainers = new();
        static ServiceContainer _global;

        public static ServiceContainer Global => _global ??= new ServiceContainer();

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        static void ClearAll()
        {
            _global?.Clear();
            foreach (var container in _sceneContainers.Values)
                container.Clear();
        }
#endif

        public static ServiceContainer ForSceneOf(MonoBehaviour mono)
        {
            var scene = mono.gameObject.scene;

            if (_sceneContainers.TryGetValue(scene, out var container))
                return container;

            container = new ServiceContainer();
            _sceneContainers.Add(scene, container);
            return container;
        }

        public static ServiceContainer For(MonoBehaviour mono)
        {
            var container = mono.GetComponentInParent<MonoContainer>();
            return !container.IsUnityNull() ? container.Value : ForSceneOf(mono);
        }
    }
}