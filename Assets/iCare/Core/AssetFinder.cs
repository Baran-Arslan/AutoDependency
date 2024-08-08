using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace iCare {
    public static class AssetFinder {
        public static IEnumerable<ScriptableObject> FindAllScriptableObjectsOfType(Type type) {
            var assets = AssetDatabase.FindAssets($"t:{type.Name}");

            return assets.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>);
        }

        public static IEnumerable<T> FindAllScriptableObjectsOfType<T>() where T : ScriptableObject {
            var assets = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            return assets.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<T>);
        }
    }
}