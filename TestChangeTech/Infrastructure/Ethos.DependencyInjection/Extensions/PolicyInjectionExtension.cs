using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Ethos.DependencyInjection
{
    public class PolicyInjectionExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            base.Context.Strategies.AddNew<PolicyInjectionStrategy>(UnityBuildStage.Initialization);
        }
    }
}
