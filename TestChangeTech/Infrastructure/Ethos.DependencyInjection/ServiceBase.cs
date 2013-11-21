
namespace Ethos.DependencyInjection
{
    public abstract class ServiceBase
    {
        protected ServiceBase()
        {
        }

        public virtual T Resolve<T>()
        {
            IContainerContext context = ContainerManager.GetContainer("container");
            return context.Resolve<T>();
        }
    }
}
