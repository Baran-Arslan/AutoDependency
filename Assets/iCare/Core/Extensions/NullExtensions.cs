using System.Collections.Generic;
using UnityEngine;

namespace iCare {
    public static class NullExtensions {
        public static bool IsUnityNull(this object obj) {
            return obj == null || (obj is Object unityObject && unityObject == null);
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list) {
            return list == null || list.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> collection) {
            return collection == null || collection.Count == 0;
        }
    }
}