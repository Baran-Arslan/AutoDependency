using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
#endif

namespace iCare {
#if ODIN_INSPECTOR
    internal sealed class PathScriptableReferenceProcessor : OdinAttributeProcessor<PathScriptableReference> {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes) {
            if (member.Name == "value") {
                attributes.Add(new FolderPathAttribute());
            }
        }
    }
#endif

    [CreateAssetMenu(menuName = "iCare/ScriptableReferences/Path Reference", fileName = "PathReference", order = -900)]
    public sealed class PathScriptableReference : ScriptableReference<string, Paths> { }
}