using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace iCare
{
    internal enum RegisterTo
    {
        ToGlobal,
        ToScene,
        ToGameObject
    }

    [Serializable]
    internal sealed class ObjectService
    {
        [SerializeField] Object service;
        [SerializeField] RegisterTo registerTo;
        [SerializeField] MonoContainer targetContainer;
        [SerializeField] string registerID;

        Type RegisterType => service.GetType();

        internal void Register(object client)
        {
            if (service == null)
            {
                Debug.LogError($"Service is null in {client.GetType().Name}", client as Object);
                return;
            }

            if (client is not MonoBehaviour mono)
            {
                if (registerTo != RegisterTo.ToGlobal)
                    Debug.LogWarning(
                        "Only MonoBehaviour clients can register to GameObject or Scene, registering to Global instead.");

                RegisterToGlobal();
                return;
            }


            switch (registerTo)
            {
                case RegisterTo.ToGameObject:
                    RegisterToGameObject(client);
                    break;
                case RegisterTo.ToScene:
                    RegisterToScene(mono);
                    break;
                case RegisterTo.ToGlobal:
                    RegisterToGlobal();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void RegisterToGlobal()
        {
            if (string.IsNullOrWhiteSpace(registerID))
                ServiceLocator.Global.Register(service, RegisterType);
            else
                ServiceLocator.Global.Register(service, RegisterType, registerID);
        }

        void RegisterToGameObject(object client)
        {
            if (targetContainer == null)
            {
                Debug.LogError("Target container is null.", client as Object);
                return;
            }

            if (string.IsNullOrWhiteSpace(registerID))
                ServiceLocator.For(targetContainer).Register(service, RegisterType);
            else
                ServiceLocator.For(targetContainer).Register(service, RegisterType, registerID);
        }

        void RegisterToScene(MonoBehaviour mono)
        {
            if (string.IsNullOrWhiteSpace(registerID))
                ServiceLocator.ForSceneOf(mono).Register(service, RegisterType);
            else
                ServiceLocator.ForSceneOf(mono).Register(service, RegisterType, registerID);
        }
    }
}