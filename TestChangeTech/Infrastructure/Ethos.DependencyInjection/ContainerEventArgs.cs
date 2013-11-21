using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.DependencyInjection
{
    public class ContainerEventArgs : EventArgs
    {
        public ContainerEventArgs(object body, ContainerTypeEnum type)
        {
            this.Body = body;
            this.Type = type;
        }

        public object Body { get; set; }

        public ContainerTypeEnum Type { get; set; }
    }
}
