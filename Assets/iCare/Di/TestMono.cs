using UnityEngine;

namespace iCare {
    [System.Serializable]
    internal sealed class TestServiceeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee {
        public float gameSpeed;
    }

    
    internal sealed class TestMono : MonoBehaviour {
        [SerializeField] Dependency<Rigidbody> rb;
        [SerializeField] Dependency<Transform> tr;
        [SerializeField] Dependency<Camera> cam;
        [SerializeField] Dependency<TestServiceeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee> service;


        [SerializeField] Service serviceField;

    }
}