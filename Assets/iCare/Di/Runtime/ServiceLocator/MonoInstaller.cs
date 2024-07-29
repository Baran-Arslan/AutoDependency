using System;
using UnityEngine;

namespace iCare.Di {
    [DefaultExecutionOrder(-1000)]
    public abstract class MonoInstaller : MonoBehaviour {
        [SerializeField] ObjectService[] servicesToInstall;

        protected enum ContainerFrom {
            Global,
            Parent,
            Scene
        }

        protected abstract ContainerFrom UseContainerFrom { get; }
        void Awake() {
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


            return;

            void TriggerInstall(ServiceContainer container) {
                Array.ForEach(servicesToInstall, service => service.Install(container));
                Install(container);
            }
        }

        protected virtual void Install(ServiceContainer container) {}
    }
}