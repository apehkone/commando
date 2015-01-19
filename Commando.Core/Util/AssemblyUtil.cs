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

        public static IDictionary<string, Assembly> LoadAllKnownAssemblies(Func<Assembly, bool> isKnownAssembly) {
            IDictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories)) {
                Assembly assembly = Assembly.LoadFrom(file);
                log.DebugFormat("Registering assembly: {0}", assembly.FullName);
                if (isKnownAssembly == null || isKnownAssembly(assembly)) {
                    if(assemblies.ContainsKey(assembly.FullName)) continue;
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