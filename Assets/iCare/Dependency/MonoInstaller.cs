using UnityEngine;

namespace iCare
{
    [DefaultExecutionOrder(-1000)]
    public class MonoInstaller : MonoBehaviour
    {
        [SerializeField] ObjectService[] services;

        void Awake()
        {
            Install();
            foreach (var service in services) service.Register(this);
        }

        protected virtual void Install()
        {
        }
    }
}