using UnityEngine;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("iCare.Editor")]

namespace iCare {
    [System.Serializable]
    internal sealed class Service {
        [SerializeField] Object value;
        [SerializeField] string serviceType;
        
        
    }
}