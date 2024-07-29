using UnityEngine;

namespace iCare {
    [CreateAssetMenu]
    public sealed class TestScriptableObj : ScriptableObject {
        [SerializeField] Dependency<Rigidbody> rb;
        [SerializeField] Dependency<Transform> tr;
        [SerializeField] Dependency<Camera> cam;
    }
}