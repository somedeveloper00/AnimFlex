using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AnimFlex.Core.Proxy {
    /// <summary>
    /// Contains helpers to use on top of Core Proxy system
    /// </summary>
    public static class AnimFlexCoreProxyHelper {
        
        static readonly Dictionary<string, PropertyInfo> _cachedCurrentProps = new();

        /// <summary>
        /// the <see cref="Type.FullName"/> of all <see cref="AnimflexCoreProxy"/> types available in domain/app
        /// </summary>
        public static readonly List<string> AllCoreProxyTypeNames = ( from assemblyDomain in AppDomain.CurrentDomain.GetAssemblies()
            from type in assemblyDomain.GetTypes()
            where type.IsSubclassOf( typeof(AnimflexCoreProxy) ) && !type.IsAbstract
            select type.FullName ).ToList();

        /// <summary>
        /// all <see cref="AnimflexCoreProxy"/> types available in domain/app
        /// </summary>
        public static readonly List<Type> AllCoreProxyTypes = ( from assemblyDomain in AppDomain.CurrentDomain.GetAssemblies()
            from type in assemblyDomain.GetTypes()
            where type.IsSubclassOf( typeof(AnimflexCoreProxy) ) && !type.IsAbstract
            select type ).ToList();

			
        /// <summary>
        /// Returns default proxy of the given type name
        /// </summary>
        public static AnimflexCoreProxy GetDefaultCoreProxy(string typeName) {
            if (_cachedCurrentProps.TryGetValue( typeName, out var prop )) {
                if (prop != null) return (AnimflexCoreProxy)prop.GetValue( null );
            }
            return GetDefaultCoreProxy( FindProxyType( typeName ) );
        }

        /// <summary>
        /// Returns default proxy of the given type
        /// </summary>
        public static T GetDefaultCoreProxy<T>() where T : AnimflexCoreProxy {
            return (T)FindProperty_Current( typeof(T) ).GetValue( null );
        }

        /// <summary>
        /// Returns default proxy of the given type
        /// </summary>
        public static AnimflexCoreProxy GetDefaultCoreProxy(Type type) {
            return (AnimflexCoreProxy)FindProperty_Current( type ).GetValue( null );
        }
        

        static PropertyInfo FindProperty_Current(Type t) =>
            t.GetProperty( "Current", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty );

        static Type FindProxyType(string typeName) {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in assembly.GetTypes()) {
                    if (type.FullName == typeName) {
                        return type;
                    }
                }
            }

            throw new Exception( $"type with name not found: {typeName}" );
        }
    }
}