// -------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Prakrishta Technologies">
//      Copyright © 2019 Prakrishta Technologies All Right Reserved
// </copyright>
// <Author>Arul Sengottaiyan</Author>
// <date>6/21/2019</date>
// <summary>
//      Service Collection Extensions to find classes that has implemented generic 
//      interfaces and add all of those classes as implemented interface type
// </summary>
// --------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;    

    /// <summary>
    /// Defines the <see cref="ServiceCollectionExtensions" /> class
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region |Methods|

        /// <summary>
        /// The GetTypesAssignableTo
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <returns>The <see cref="List{TypeInfo}"/> that has implemented classes type info</returns>
        public static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
        {
            var typeInfoList = new List<TypeInfo>();
            foreach (var type in assembly.DefinedTypes)
            {
                bool isAssignableTo = type.GetInterfaces()
                                        .Any(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == compareType);
                if (isAssignableTo && compareType != type)
                {
                    typeInfoList.Add(type);
                }
            }

            return typeInfoList;
        }

        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assemblyString">The assembly name where the implementation resides</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="lifetime">The service lifetime information, default it will be scoped</param>
        public static void AddClassesAsImplementedInterface(
            this IServiceCollection services, 
            string assemblyString, 
            Type compareType,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            AddClassesAsImplementedInterface(services, Assembly.Load(assemblyString), compareType, lifetime);
        }

        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assembly">The assembly that has implementations detail</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="lifetime">The service lifetime information, default it will be scoped</param>
        public static void AddClassesAsImplementedInterface(
            this IServiceCollection services, 
            Assembly assembly, 
            Type compareType,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            assembly.GetTypesAssignableTo(compareType).ForEach((t) =>
            {
                foreach (var implementedInterface in t.ImplementedInterfaces)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(implementedInterface, t);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(implementedInterface, t);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(implementedInterface, t);
                            break;
                    }
                }
            });
        }

        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assemblyString">The assembly name where the implementation resides</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="action">The action<see cref="Action{Type, Type}"/> that has to be executed</param>
        public static void AddClassesAsImplementedInterface(this IServiceCollection services, string assemblyString, Type compareType, Action<Type, Type> action)
        {
            AddClassesAsImplementedInterface(services, Assembly.Load(assemblyString), compareType, action);
        }

        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assembly">The assembly that has implementations detail</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="action">The action<see cref="Action{Type, Type}"/> that has to be executed</param>
        public static void AddClassesAsImplementedInterface(this IServiceCollection services, Assembly assembly, Type compareType, Action<Type, Type> action)
        {
            assembly.GetTypesAssignableTo(compareType).ForEach((t) =>
            {
                foreach (var implementedInterface in t.ImplementedInterfaces)
                {
                    action(implementedInterface, t);
                }
            });
        }

        #endregion
    }
}
