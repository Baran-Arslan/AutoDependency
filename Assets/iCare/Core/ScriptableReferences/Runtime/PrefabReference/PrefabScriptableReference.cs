using UnityEngine;

namespace iCare {
    [CreateAssetMenu(menuName = "iCare/ScriptableReferences/Prefab Reference", fileName = "PrefabReference", order = -900)]
    public sealed class PrefabScriptableReference : ScriptableReference<GameObject, Prefabs> { }
}