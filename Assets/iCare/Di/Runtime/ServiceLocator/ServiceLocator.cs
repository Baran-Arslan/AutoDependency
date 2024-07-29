using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace iCare.Di {
    public static class ServiceLocator {
        static readonly Dictionary<Scene, ServiceContainer> _sceneContainers = new();
        static ServiceContainer _global;

        public static ServiceContainer Global => _global ??= new ServiceContainer();

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        internal static void ClearAll() {
            _global?.Clear();
            foreach (var container in _sceneContainers.Values)
                container.Clear();
        }
#endif

        internal static ServiceContainer ForSceneOf(MonoBehaviour mono) {
            var scene = mono.gameObject.scene;

            if (_sceneContainers.TryGetValue(scene, out var container))
                return container;

            container = new ServiceContainer();
            _sceneContainers.Add(scene, container);
            return container;
        }

        internal static ServiceContainer For(MonoBehaviour mono) {
            var container = mono.GetComponentInParent<MonoContainer>();
            if (!container.IsUnityNull())
                return container.Value;

            Debug.LogWarning("MonoContainer not found in parent hierarchy. Using scene container instead.".Highlight());
            return ForSceneOf(mono);
        }
    }
}