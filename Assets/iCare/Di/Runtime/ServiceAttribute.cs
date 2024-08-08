using System;
using JetBrains.Annotations;

namespace iCare.Di {
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ServiceAttribute : Attribute {
        internal readonly bool FindFromScene;
        internal readonly Type[] RegisterTypes;
        internal readonly string ResourcesPath;

        public ServiceAttribute(Type registerType) {
            RegisterTypes = new[] { registerType };
        }

        public ServiceAttribute(Type registerType, string resourcesPath) {
            RegisterTypes = new[] { registerType };
            ResourcesPath = resourcesPath;
        }

        public ServiceAttribute(Type registerType, bool findFromScene) {
            RegisterTypes = new[] { registerType };
            FindFromScene = findFromScene;
        }

        public ServiceAttribute(Type typeOne, Type typeTwo) {
            RegisterTypes = new[] { typeOne, typeTwo };
        }

        public ServiceAttribute(Type typeOne, Type typeTwo, string resourcesPath) {
            RegisterTypes = new[] { typeOne, typeTwo };
            ResourcesPath = resourcesPath;
        }

        public ServiceAttribute(Type typeOne, Type typeTwo, bool findFromScene) {
            RegisterTypes = new[] { typeOne, typeTwo };
            FindFromScene = findFromScene;
        }

        public ServiceAttribute(Type typeOne, Type typeTwo, Type typeThree) {
            RegisterTypes = new[] { typeOne, typeTwo, typeThree };
        }

        public ServiceAttribute(Type typeOne, Type typeTwo, Type typeThree, string resourcesPath) {
            RegisterTypes = new[] { typeOne, typeTwo, typeThree };
            ResourcesPath = resourcesPath;
        }

        public ServiceAttribute(Type typeOne, Type typeTwo, Type typeThree, bool findFromScene) {
            RegisterTypes = new[] { typeOne, typeTwo, typeThree };
            FindFromScene = findFromScene;
        }
    }
}