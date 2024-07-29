using UnityEngine;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("iCare.Editor")]

namespace iCare.Di {
    [System.Serializable]
    internal sealed class ObjectService {
        [SerializeField] Object value;
        [SerializeField] string serviceType;

        public void Install(ServiceContainer container) {
            container.Register(value, serviceType);
        }
    }
}