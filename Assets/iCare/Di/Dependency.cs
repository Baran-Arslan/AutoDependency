using System;
using UnityEngine;

namespace iCare {
    internal enum DependencyFrom {
        FromGlobal,
        FromScene,
        FromGameObject
    }

    [Serializable]
    internal sealed class Dependency<T> {
        [SerializeField] bool isManual;
        [SerializeField] DependencyFrom from;
        [SerializeField] T manualValue;
    }
}