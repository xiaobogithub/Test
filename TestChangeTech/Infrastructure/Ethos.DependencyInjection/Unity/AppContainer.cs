using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;
using System.Collections.Specialized;

namespace Ethos.DependencyInjection
{
    public class AppContainer
    {
        public IDictionary<string, IUnityContainer> items;
        //private static readonly object synlock = new object();

        public AppContainer()
        {
            items = new Dictionary<string, IUnityContainer>();
        }
    }
}
