using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace iCare {
    public abstract class ScriptableReference<TValue, TEnum> : ScriptableObject, IEnumTarget<TEnum> where TEnum : Enum {
        [SerializeField] TValue value;
        public TValue Value => value;
    }

    public abstract class ReferenceManager<TScriptable, TManager> : ScriptableSingeleton<TManager>
        where TScriptable : ScriptableObject where TManager : ReferenceManager<TScriptable, TManager> {
#if ODIN_INSPECTOR //TODO - ADD CUSTOM INSPECTOR
        [Sirenix.OdinInspector.ReadOnly]
#endif
        [SerializeField] List<TScriptable> scriptables;

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
        void Refresh() {
            scriptables = AssetFinder.FindAllScriptableObjectsOfType<TScriptable>().ToList();
        }
#endif

        protected virtual void OnEnable() {
#if UNITY_EDITOR
            ScriptableObjectAssetListener.OnScriptableObjectCreatedEvent += OnScriptableObjectCreated;
            ScriptableObjectAssetListener.OnScriptableObjectDeletedEvent += OnScriptableObjectDeleted;
#endif
        }

        protected virtual void OnDisable() {
#if UNITY_EDITOR
            ScriptableObjectAssetListener.OnScriptableObjectCreatedEvent -= OnScriptableObjectCreated;
            ScriptableObjectAssetListener.OnScriptableObjectDeletedEvent -= OnScriptableObjectDeleted;
#endif
        }

#if UNITY_EDITOR
        void OnScriptableObjectDeleted(ScriptableObject obj) {
            if (obj is TScriptable scriptable) {
                TryRemove(scriptable);
            }
        }

        void OnScriptableObjectCreated(ScriptableObject obj) {
            if (obj is TScriptable scriptable) {
                TryAdd(scriptable);
            }
        }


        public void TryAdd(TScriptable scriptable) {
            if (scriptables.Contains(scriptable)) return;
            scriptables.Add(scriptable);
            UnityEditor.EditorUtility.SetDirty(this);
            Debug.Log($"Added {scriptable.name.Highlight()} to {name.Highlight()}");
        }

        public void TryRemove(TScriptable scriptable) {
            if (!scriptables.Contains(scriptable)) return;
            scriptables.Remove(scriptable);
            UnityEditor.EditorUtility.SetDirty(this);
            Debug.Log($"Removed {scriptable.name.Highlight()} from {name.Highlight()}");
        }
#endif

        public static IEnumerable<TScriptable> GetAll() {
            return Instance.scriptables;
        }

        public static TScriptable GetByName(string targetName) {
            return Instance.scriptables.Find(scriptable => scriptable.name == targetName);
        }
    }
}