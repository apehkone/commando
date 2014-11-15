using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;

namespace Commando.Core.Util
{
    public static class AssemblyUtil
    {
        static readonly ILog log = LogManager.GetLogger(typeof (AssemblyUtil));

        public static IDictionary<string, Assembly> LoadAllKnownAssemblies() {
            IDictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

            foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories)) {
                var assembly = Assembly.LoadFrom(file);
                log.DebugFormat("Registering assembly: {0}", assembly.FullName);
                if (assembly.IsKnownAssembly()) {
                    assemblies.Add(assembly.FullName, assembly);
                }
            }

            return assemblies;
        }

        internal static bool IsKnownAssembly(this Assembly assembly) {
            return assembly.GetCustomAttributes(false).Cast<Attribute>().Any(attribute => attribute is AssemblyTrademarkAttribute && ((AssemblyTrademarkAttribute) attribute).Trademark == "Commando");
        }
    }
}