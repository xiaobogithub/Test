using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.DependencyInjection
{
    public abstract class ContainerContextBase : IContainerContext
    {
        public string Name { get; private set; }

        public ContainerContextBase(string name)
        {
            this.Name = name;
        }

        public abstract void Initialize();

        public abstract T Resolve<T>();

        public abstract void TearDown();

        public event EventHandler<ContainerEventArgs> BeforeInitialized;

        public event EventHandler<ContainerEventArgs> AfterInitialized;

        public event EventHandler<ContainerEventArgs> BeforeTearDown;

        public event EventHandler<ContainerEventArgs> AfterTearDown;

        public event EventHandler<ContainerFailedEventArgs> ResolveFailed;

        protected virtual void OnBeforeInitialized(ContainerEventArgs e)
        {
            EventHandler<ContainerEventArgs> handler = BeforeInitialized;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAfterInitialized(ContainerEventArgs e)
        {
            EventHandler<ContainerEventArgs> handler = AfterInitialized;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBeforeTearDown(ContainerEventArgs e)
        {
            EventHandler<ContainerEventArgs> handler = BeforeTearDown;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAfterTearDown(ContainerEventArgs e)
        {
            EventHandler<ContainerEventArgs> handler = AfterTearDown;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnResolveFailed(ContainerFailedEventArgs e)
        {
            EventHandler<ContainerFailedEventArgs> handler = ResolveFailed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
