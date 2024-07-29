namespace iCare {
    public static class NullExtensions {
        public static bool IsUnityNull(this object obj) {
            return obj == null || obj is UnityEngine.Object unityObject && unityObject == null;
        }
    }
}