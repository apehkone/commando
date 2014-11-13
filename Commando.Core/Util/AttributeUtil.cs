using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Commando.Core.Util
{
    public class AttributeUtil
    {
        public static IList<Type> FindByAttribute<TAttribute>(Assembly assembly) {
            return (from type in assembly.GetTypes() from attribute in type.GetCustomAttributes(false) where attribute.GetType() == typeof (TAttribute) select type).ToList();
        }
    }
}