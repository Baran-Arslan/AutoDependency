using iCare;
using UnityEngine;


public interface ITestInterface {
    
}


internal sealed class TestClass : MonoBehaviour, ITestInterface {
    [SerializeField] private Service[] values;

    void Awake() {
        foreach (var value in values) {
            Debug.Log(value.ServiceType);
        }
    }
}