using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Commando.Core;
using Commando.Core.Util;
using log4net;

namespace Commando.Autofac
{
    public static class ContainerExtensions
    {
        static readonly ILog log = LogManager.GetLogger(typeof (ContainerExtensions));

        public static void RegisterBasicDispatcher(this ContainerBuilder builder, Func<Assembly, bool> isKnownAssembly = null) {
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterMessageHandlers(AssemblyUtil.LoadAllKnownAssemblies(isKnownAssembly));
        }

        public static void RegisterMessageHandlers(this ContainerBuilder builder, IDictionary<string, Assembly> assemblies) {
            foreach (Type type in assemblies.Select(assembly => CommandHandlerRegistry.FindMessageHandlersFrom(assembly.Value)).SelectMany(types => types)) {
                builder.RegisterType(type);
            }
        }

        static void RegisterType(this ContainerBuilder builder, Type type) {
            Type[] interfaces = type.GetInterfaces();
            log.DebugFormat("Registering command handler: {0} as {1}", type, interfaces[0]);
            RegistrationExtensions.RegisterType(builder, type).As(interfaces[0]);
        }
    }
}