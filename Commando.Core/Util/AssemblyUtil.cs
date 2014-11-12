using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Commando.Core.Util
{
    public static class AssemblyUtil
    {
        static ILog log = LogManager.GetLogger(typeof (AssemblyUtil));

        public static IDictionary<string, Assembly> LoadAllKnownAssemblies() {
            IDictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.IsKnownAssembly())) {
                log.DebugFormat("Registering assembly: {0}", assembly.FullName);
                assemblies.Add(assembly.FullName, assembly);
            }
            return assemblies;
        }

        internal static bool IsKnownAssembly(this Assembly assembly) {
            return true;
            //return assembly.GetCustomAttributes(false).Cast<Attribute>().Any(attribute => attribute is AssemblyTrademarkAttribute && ((AssemblyTrademarkAttribute)attribute).Trademark == "OwnTradeMarkHere");
        }
    }
}