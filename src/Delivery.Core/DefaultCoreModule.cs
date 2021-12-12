using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ToDoItemSearchService>()
            //    .As<IToDoItemSearchService>().InstancePerLifetimeScope();
        }
    }
}
