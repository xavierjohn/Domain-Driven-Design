using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CWiz.DomainDrivenDesign
{
    static class TypeExtensions
    {
        static readonly TypeInfo IEnumerable = typeof(IEnumerable).GetTypeInfo();
        static readonly TypeInfo IEnumerableOfT = typeof(IEnumerable<>).GetTypeInfo();
        static readonly TypeInfo String = typeof(string).GetTypeInfo();
        static readonly Type Object = typeof(Object);

        internal static bool IsEnumerable(this Type type) => type.GetTypeInfo().IsEnumerable();

        internal static bool IsEnumerable(this TypeInfo type) =>
            !String.Equals(type) && IEnumerable.IsAssignableFrom(type);

        internal static Type GetItemType(this Type type) => type.GetTypeInfo().GetItemType();

        internal static Type GetItemType(this TypeInfo type)
        {
            if (!type.IsEnumerable())
            {
                return Object;
            }

            var elementType = from @interface in type.ImplementedInterfaces
                              let t = @interface.GetTypeInfo()
                              where t.IsGenericType && IEnumerableOfT.Equals(t.GetGenericTypeDefinition().GetTypeInfo())
                              select t.GenericTypeArguments[0];

            return elementType.FirstOrDefault() ?? typeof(object);
        }
    }
}