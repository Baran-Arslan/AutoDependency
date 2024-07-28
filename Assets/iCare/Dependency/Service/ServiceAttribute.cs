using System;
using JetBrains.Annotations;

namespace iCare
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ServiceAttribute : Attribute
    {
        public readonly bool FindFromScene;
        public readonly Type[] RegisterTypes;
        public readonly string ResourcesPath;

        public ServiceAttribute(string resourcesPath = null, bool findFromScene = false, params Type[] registerTypes)
        {
            RegisterTypes = registerTypes;
            ResourcesPath = resourcesPath;
            FindFromScene = findFromScene;
        }

        public ServiceAttribute(bool findFromScene = false, params Type[] registerTypes)
        {
            RegisterTypes = registerTypes;
            FindFromScene = findFromScene;
        }

        public ServiceAttribute(string resourcesPath = null, params Type[] registerTypes)
        {
            RegisterTypes = registerTypes;
            ResourcesPath = resourcesPath;
        }

        public ServiceAttribute(params Type[] registerTypes)
        {
            RegisterTypes = registerTypes;
        }
    }
}