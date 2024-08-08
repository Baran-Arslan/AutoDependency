using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace iCare.Editor {
    public static class ScriptableObjectEnumGenerator {
        [MenuItem("iCare/Generate All ScriptableEnums %#t")]
        public static void GenerateAll() {
            var allEnumTypes = TypeExtensions.FindAllTypesOf<IEnumTarget>();

            var enumValuesDict = new Dictionary<Type, IReadOnlyCollection<ScriptableObject>>();
            
            foreach (var enumHolderClass in allEnumTypes) {
                foreach (var enumInterface in enumHolderClass.GetInterfaces().Where(i => i.InheritsOrImplements(typeof(IEnumTarget)))) {
                    if (enumInterface.ContainsGenericParameters) continue;
                    if (!enumInterface.TryResolveGenericType(out var enumType)) continue;

                    if (!enumValuesDict.TryGetValue(enumType, out var enumValues)) {
                        enumValues = AssetFinder.FindAllScriptableObjectsOfType(enumHolderClass).ToArray();
                        enumValuesDict.Add(enumType, enumValues);
                    }
                    
                    EnumGenerator.GenerateByEnumType(enumType, enumValues.Select(e => e.name.MakeValidEnumName()).ToArray());
                }
            }
        }
    }
}