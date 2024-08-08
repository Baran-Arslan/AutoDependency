using UnityEngine;

namespace iCare {
    public abstract class ScriptableSingeleton<T> : ScriptableObject where T : ScriptableObject {
        static T _instance;

        public static T Instance {
            get {
                if (_instance != null) return _instance;
                _instance = Resources.Load<T>(typeof(T).Name);
                if (_instance == null)
                    Debug.LogError(
                        $"Scriptable singeleton with name {typeof(T).Highlight()} not found in Resources folder.");

                return _instance;
            }
        }
    }
}