using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

[assembly: InternalsVisibleTo("iCare.Editor")]

namespace iCare.Di {
    [Serializable]
    internal sealed class ObjectService {
        [SerializeField] Object value;
        [SerializeField] string serviceType;

        public void Install(ServiceContainer container) {
            container.Register(value, serviceType);
        }
    }
}