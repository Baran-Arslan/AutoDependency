using System;
using UnityEngine;

namespace iCare.Di {
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

        internal T Resolve(MonoBehaviour mono) {
            if (isManual) return manualValue;

            return from switch {
                DependencyFrom.FromGlobal => ServiceLocator.Global.Resolve<T>(),
                DependencyFrom.FromScene => ServiceLocator.ForSceneOf(mono).Resolve<T>(),
                DependencyFrom.FromGameObject => ServiceLocator.For(mono).Resolve<T>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        internal T Resolve(object obj) {
            return isManual ? manualValue : ServiceLocator.Global.Resolve<T>();
        }
    }
}