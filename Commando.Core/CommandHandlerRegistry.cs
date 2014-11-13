using System;
using System.Collections.Generic;
using System.Reflection;
using Commando.Core.Attributes;
using Commando.Core.Util;

namespace Commando.Core
{
    public class CommandHandlerRegistry
    {
        public static IList<Type> FindMessageHandlersFrom(Assembly assembly) {
            return AttributeUtil.FindByAttribute<CommandHandlerAttribute>(assembly);
        }
    }
}