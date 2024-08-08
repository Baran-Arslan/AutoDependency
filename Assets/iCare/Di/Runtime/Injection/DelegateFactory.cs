using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace iCare.Di {
    internal static class DelegateFactory {
        internal static Delegate CreateDelegate(MethodInfo methodInfo) {
            var parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
            var delegateType = Expression.GetActionType(new[] { methodInfo.DeclaringType }.Concat(parameterTypes).ToArray());
            return methodInfo.CreateDelegate(delegateType);
        }
    }
}