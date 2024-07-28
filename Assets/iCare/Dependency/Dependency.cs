using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("iCare.Core.Editor")]

namespace iCare
{
    internal enum ResolveFrom
    {
        FromGlobal = 0,
        FromScene = 1,
        FromGameObject = 2,
        Manuel = 3
    }

    [Serializable]
    public class Dependency<T>
    {
        [SerializeField] ResolveFrom resolveFrom;
        [SerializeField] T value;
        [SerializeField] string id;

        internal T Resolve(MonoBehaviour mono)
        {
            ValidateResolveFrom(mono);

            if (string.IsNullOrWhiteSpace(id))
                return resolveFrom switch
                {
                    ResolveFrom.FromGameObject => ServiceLocator.For(mono).Resolve<T>(),
                    ResolveFrom.FromScene => ServiceLocator.ForSceneOf(mono).Resolve<T>(),
                    ResolveFrom.FromGlobal => ServiceLocator.Global.Resolve<T>(),
                    ResolveFrom.Manuel => value,
                    _ => throw new NotImplementedException()
                };

            return resolveFrom switch
            {
                ResolveFrom.FromGameObject => ServiceLocator.For(mono).Resolve<T>(id),
                ResolveFrom.FromScene => ServiceLocator.ForSceneOf(mono).Resolve<T>(id),
                ResolveFrom.FromGlobal => ServiceLocator.Global.Resolve<T>(id),
                ResolveFrom.Manuel => value,
                _ => throw new NotImplementedException()
            };
        }

        void ValidateResolveFrom(MonoBehaviour mono)
        {
            if (resolveFrom == ResolveFrom.Manuel && value == null)
                Debug.LogError("Resolving set to manuel but value is null", mono);
        }
    }
}