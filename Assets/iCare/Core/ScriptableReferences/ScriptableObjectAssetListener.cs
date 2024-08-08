#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace iCare {
    public sealed class ScriptableObjectAssetListener : AssetModificationProcessor {
        public static event Action<ScriptableObject> OnScriptableObjectCreatedEvent;
        public static event Action<ScriptableObject> OnScriptableObjectDeletedEvent;

        static void OnWillCreateAsset(string assetName) {
            if (assetName.Contains(".meta")) return;
            if (Path.GetExtension(assetName) != ".asset") return;
            EditorApplication.delayCall += () => {
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetName);
                if (asset != null)
                    OnScriptableObjectCreated(asset);
            };
        }


        static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options) {
            if (Path.GetExtension(assetPath) != ".asset")
                return AssetDeleteResult.DidNotDelete;
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (asset != null)
                OnScriptableObjectDeleted(asset);

            return AssetDeleteResult.DidNotDelete;
        }

        static void OnScriptableObjectDeleted(ScriptableObject asset) {
            OnScriptableObjectDeletedEvent?.Invoke(asset);
        }


        static void OnScriptableObjectCreated(ScriptableObject asset) {
            OnScriptableObjectCreatedEvent?.Invoke(asset);
        }
    }
}
#endif