using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Commando.Core.Util
{
    public static class ContainerExtensions
    {
        public static void RegisterDispatcher(this ContainerBuilder builder) {
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterMessageHandlers();
        }

        private static void RegisterMessageHandlers(this ContainerBuilder builder) {
            IDictionary<string, Assembly> assemblies = AssemblyUtil.LoadAllKnownAssemblies();
            foreach (Type type in assemblies.Select(assembly => CommandHandlerRegistry.FindMessageHandlersFrom(assembly.Value)).SelectMany(types => types)) {
                builder.RegisterType(type);
            }
        }

        private static void RegisterType(this ContainerBuilder builder, Type type) {
            Type[] interfaces = type.GetInterfaces();
            RegistrationExtensions.RegisterType(builder, type).As(interfaces[0]);
        }
    }
}