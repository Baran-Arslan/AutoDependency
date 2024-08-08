using System;
using UnityEngine;

namespace iCare.Di {
    [DefaultExecutionOrder(-1000)]
    public abstract class MonoInstaller : MonoBehaviour, IInstaller {
        [SerializeField] ObjectService[] servicesToInstall;

        bool _isInstalled;

        protected abstract ContainerFrom UseContainerFrom { get; }

        void Awake() {
            Install();
        }

        public void Install() {
            if (_isInstalled) return;

            switch (UseContainerFrom) {
                case ContainerFrom.Global:
                    var container = ServiceLocator.Global;
                    TriggerInstall(container);
                    break;
                case ContainerFrom.Parent:
                    container = ServiceLocator.For(this);
                    TriggerInstall(container);
                    break;
                case ContainerFrom.Scene:
                    container = ServiceLocator.ForSceneOf(this);
                    TriggerInstall(container);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _isInstalled = true;
            return;

            void TriggerInstall(ServiceContainer container) {
                for (var i = 0; i < servicesToInstall.Length; i++) servicesToInstall[i].Install(container);
                Install(container);
                servicesToInstall = null;
            }
        }

        protected virtual void Install(ServiceContainer container) { }

        protected enum ContainerFrom {
            Global,
            Parent,
            Scene
        }
    }
}