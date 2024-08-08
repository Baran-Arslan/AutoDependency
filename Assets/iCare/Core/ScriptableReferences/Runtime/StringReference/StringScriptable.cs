using UnityEngine;

namespace iCare {
    [CreateAssetMenu(menuName = "iCare/ScriptableReferences/String Reference", fileName = "StringReference", order = -900)]
    public sealed class StringScriptableReference : ScriptableReference<string, Strings> {
    }
}