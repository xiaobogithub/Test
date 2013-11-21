using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.DependencyInjection
{
    public sealed class ContainerManager
    {
        public static IContainerContext GetContainer(string name)
        {
            return new UnityContainerContext(name);
        }
    }
}
