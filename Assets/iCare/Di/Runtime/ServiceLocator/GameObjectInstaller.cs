using UnityEngine;

namespace iCare.Di {
    [RequireComponent(typeof(MonoContainer))]
    public class GameObjectInstaller : MonoInstaller {
        protected override ContainerFrom UseContainerFrom => ContainerFrom.Parent;
    }
}