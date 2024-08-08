namespace iCare.Di {
    public class GlobalInstaller : MonoInstaller {
        protected override ContainerFrom UseContainerFrom => ContainerFrom.Global;
    }
}