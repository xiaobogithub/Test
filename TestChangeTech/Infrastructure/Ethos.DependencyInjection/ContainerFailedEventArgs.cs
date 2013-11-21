using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.DependencyInjection
{
    public class ContainerFailedEventArgs : ContainerEventArgs
    {

        public ContainerFailedEventArgs(object body, ContainerTypeEnum type)
            : base(body, type)
        { 
        }

        public ContainerFailedEventArgs(object body, ContainerTypeEnum type, Exception error)
            : base(body, type)
        {
            this.Error = error;
        }

        public Exception Error { get; set; }
    }
}
