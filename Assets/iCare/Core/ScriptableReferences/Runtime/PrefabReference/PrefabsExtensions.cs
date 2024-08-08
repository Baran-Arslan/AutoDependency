using UnityEngine;

namespace iCare {
    public static class PrefabsExtensions {
        public static GameObject Get(this Prefabs prefab) {
            return PrefabReferenceManager.GetByName(prefab.ToString()).Value;
        }
    }
}