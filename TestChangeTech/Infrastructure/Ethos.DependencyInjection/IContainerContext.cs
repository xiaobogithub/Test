using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.DependencyInjection
{
    public interface IContainerContext
    {
        string Name { get; }

        void Initialize();

        T Resolve<T>();

        void TearDown();

        #region Event handlers

        event EventHandler<ContainerEventArgs> BeforeInitialized;

        event EventHandler<ContainerEventArgs> AfterInitialized;

        event EventHandler<ContainerEventArgs> BeforeTearDown;

        event EventHandler<ContainerEventArgs> AfterTearDown;

        event EventHandler<ContainerFailedEventArgs> ResolveFailed; 

        #endregion
    }
}
