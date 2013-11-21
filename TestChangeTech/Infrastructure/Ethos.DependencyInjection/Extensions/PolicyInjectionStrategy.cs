using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.ObjectBuilder;

namespace Ethos.DependencyInjection
{
    public class PolicyInjectionStrategy : EnterpriseLibraryBuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            base.PreBuildUp(context);
            if (context.Policies.Get<IPolicyInjectionPolicy>(context.BuildKey) == null)
            {
                context.Policies.Set<IPolicyInjectionPolicy>(new PolicyInjectionPolicy(true), context.BuildKey);
            }
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            base.PostBuildUp(context);
            IPolicyInjectionPolicy policy = context.Policies.Get<IPolicyInjectionPolicy>(context.BuildKey);
            if ((policy != null) && policy.ApplyPolicies)
            {
                policy.SetPolicyConfigurationSource(EnterpriseLibraryBuilderStrategy.GetConfigurationSource(context));
                context.Existing = policy.ApplyProxy(context.Existing, BuildKey.GetType(context.OriginalBuildKey));
            }
        }
    }
}
