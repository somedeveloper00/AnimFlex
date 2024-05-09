using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AnimFlex.Core.Proxy
{
    /// <summary>
    /// Contains helpers to use on top of Core Proxy system
    /// </summary>
    public static class AnimFlexCoreProxyHelper
    {
        private static readonly Dictionary<string, PropertyInfo> _cachedDefaultProps = new();

        /// <summary>
        /// the <see cref="Type.FullName"/> of all <see cref="AnimflexCoreProxy"/> types available in domain/app
        /// </summary>
        public static readonly List<string> AllCoreProxyTypeNames = (from assemblyDomain in AppDomain.CurrentDomain.GetAssemblies()
                                                                     from type in assemblyDomain.GetTypes()
                                                                     where type.IsSubclassOf(typeof(AnimflexCoreProxy)) && !type.IsAbstract
                                                                     select type.FullName).ToList();

        /// <summary>
        /// all <see cref="AnimflexCoreProxy"/> types available in domain/app
        /// </summary>
        public static readonly List<Type> AllCoreProxyTypes = (from assemblyDomain in AppDomain.CurrentDomain.GetAssemblies()
                                                               from type in assemblyDomain.GetTypes()
                                                               where type.IsSubclassOf(typeof(AnimflexCoreProxy)) && !type.IsAbstract
                                                               select type).ToList();


        /// <summary>
        /// Returns default proxy of the given type name
        /// </summary>
        public static AnimflexCoreProxy GetDefaultCoreProxy(string typeName)
        {
            if (_cachedDefaultProps.TryGetValue(typeName, out var prop))
            {
                if (prop != null)
                {
                    return (AnimflexCoreProxy)prop.GetValue(null);
                }
            }
            return GetDefaultCoreProxy(FindProxyType(typeName));
        }

        /// <summary>
        /// Returns default proxy of the given type
        /// </summary>
        public static T GetDefaultCoreProxy<T>() where T : AnimflexCoreProxy
        {
            return (T)FindProperty_Default(typeof(T)).GetValue(null);
        }

        /// <summary>
        /// Returns default proxy of the given type
        /// </summary>
        public static AnimflexCoreProxy GetDefaultCoreProxy(Type type)
        {
            return (AnimflexCoreProxy)FindProperty_Default(type).GetValue(null);
        }

        private static PropertyInfo FindProperty_Default(Type t) =>
            t.GetProperty("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty);

        private static Type FindProxyType(string typeName)
        {
            var ind = AllCoreProxyTypeNames.IndexOf(typeName);
            if (ind == -1)
            {
                Debug.LogError($"Core Proxy type not found: {typeName}");
            }
            return AllCoreProxyTypes[ind];
        }
    }
}