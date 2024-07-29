using UnityEngine;

namespace iCare.Di {
    internal sealed class MonoContainer : MonoBehaviour {
        public ServiceContainer Value { get; } = new();

        public static implicit operator ServiceContainer(MonoContainer container) {
            return container.Value;
        }
    }
}