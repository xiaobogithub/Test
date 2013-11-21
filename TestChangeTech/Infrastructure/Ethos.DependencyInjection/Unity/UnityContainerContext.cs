using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace Ethos.DependencyInjection
{
    public class UnityContainerContext : ContainerContextBase
    {
        public UnityContainerContext(string name)
            : base(name)
        {
        }

        public override void Initialize()
        {
            OnBeforeInitialized(new ContainerEventArgs(this, ContainerTypeEnum.Unity));

            //if (CurrentContext.Application[this.Name] == null)
            //{
            //    lock (synlock)
            //    {
            //        if (CurrentContext.Application[this.Name] == null)
            //        {
            //            IUnityContainer container = new UnityContainer();
            //            UnityConfigurationSection sectioin = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            //            sectioin.Containers[this.Name].Configure(container);
            //            CurrentContext.Application[this.Name] = container;
            //        }
            //    }
            //}

            if (!AppContainer.items.ContainsKey(this.Name))
            {
                IUnityContainer container = new UnityContainer();
                UnityConfigurationSection sectioin = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
                sectioin.Containers[this.Name].Configure(container);
                lock (synlock)
                {
                    if (!AppContainer.items.ContainsKey(this.Name))
                    {
                        AppContainer.items.Add(this.Name, container);
                    }
                }
            }

            OnAfterInitialized(new ContainerEventArgs(this, ContainerTypeEnum.Unity));
        }

        public override T Resolve<T>()
        {
            try
            {
                this.Initialize();

                //return ((IUnityContainer)CurrentContext.Application[this.Name]).Resolve<T>();
                return ((IUnityContainer)AppContainer.items[this.Name]).Resolve<T>();
            }
            catch
            {
                OnResolveFailed(new ContainerFailedEventArgs(this, ContainerTypeEnum.Unity));
                return default(T);
            }
        }

        public override void TearDown()
        {
            OnBeforeTearDown(new ContainerEventArgs(this, ContainerTypeEnum.Unity));
            //CurrentContext.Application[this.Name] = null;
            AppContainer.items[this.Name] = null;
            OnAfterTearDown(new ContainerEventArgs(this, ContainerTypeEnum.Unity));
        }

        //private HttpContext CurrentContext
        //{
        //    get
        //    {
        //        HttpContext context = HttpContext.Current;
        //        if (context == null)
        //        {
        //            throw new Exception("Null HttpContext.");
        //        }

        //        return context;
        //    }
        //}

        private static AppContainer appContainer;

        private AppContainer AppContainer
        {
            get
            {
                if (appContainer == null)
                {
                    lock (synlock)
                    {
                        if (appContainer == null)
                        {
                            appContainer = new AppContainer();
                        }
                    }
                }
                return appContainer;
            }
            set
            {
                appContainer = value;
            }
        }

        private static readonly object synlock = new object();
    }
}
