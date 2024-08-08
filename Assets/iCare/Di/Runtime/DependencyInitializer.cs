using System.Linq;
using UnityEngine;

namespace iCare.Di {
    internal static class DependencyInitializer {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void Initialize() {
            LoopProvider.Create();

            var allDependencies = DependencyEntityProvider.GetAllDependencies().ToArray();

            foreach (var entity in allDependencies) {
                if (entity is IListenLoop)
                    LoopProvider.Instance.AddLoopListener(entity);

                if (entity is IInstaller installer)
                    installer.Install();
            }

            DependencyInjector.Inject(allDependencies.OfType<IConstruct>());
            LoopProvider.Activate();
        }
    }
}