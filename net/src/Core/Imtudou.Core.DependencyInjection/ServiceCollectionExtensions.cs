using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Imtudou.Core.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="lifetime"></param>
        /// <param name="filter"></param>
        /// <param name="includeInternalTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceFromAssembly(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped, Func<AssemblyScanner.AssemblyScanResult, bool> filter = null, bool includeInternalTypes = false)
         {

            AssemblyScanner.FindAssembly(assembly, RegisterTypeEnum.Service, includeInternalTypes)
                .ForEach(scanResult => services.AddScanResult(scanResult, lifetime, filter));
            return services;
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="lifetime"></param>
        /// <param name="filter"></param>
        /// <param name="includeInternalTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceFromAssemblies(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped, Func<AssemblyScanner.AssemblyScanResult, bool> filter = null, bool includeInternalTypes = false)
        {
            foreach (var assembly in assemblies)
                services.AddServiceFromAssembly(assembly, lifetime, filter, includeInternalTypes);

            return services;
        }

        /// <summary>
        /// 在指定类型的程序集中添加
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <param name="lifetime"></param>
        /// <param name="filter"></param>
        /// <param name="includeInternalTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceFromAssemblyContaining(this IServiceCollection services, Type type, ServiceLifetime lifetime = ServiceLifetime.Scoped, Func<AssemblyScanner.AssemblyScanResult, bool> filter = null, bool includeInternalTypes = false)
            => services.AddServiceFromAssembly(type.Assembly, lifetime, filter, includeInternalTypes);

        /// <summary>
        /// 在指定类型的程序集中添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <param name="filter"></param>
        /// <param name="includeInternalTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceFromAssemblyContaining<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped, Func<AssemblyScanner.AssemblyScanResult, bool> filter = null, bool includeInternalTypes = false)
            => services.AddServiceFromAssembly(typeof(T).Assembly, lifetime, filter, includeInternalTypes);

        /// <summary>
        /// 从AssemblyScanner结果注册方法
        /// </summary>
        /// <param name="services"></param>
        /// <param name="scanResult"></param>
        /// <param name="lifetime"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static IServiceCollection AddScanResult(this IServiceCollection services,
            AssemblyScanner.AssemblyScanResult scanResult,
            ServiceLifetime lifetime,
            Func<AssemblyScanner.AssemblyScanResult, bool> filter)
        { 
            var shouldRegister = filter?.Invoke(scanResult) ?? true;
            if (shouldRegister)
            {
                // register as interface
                services.Add(new ServiceDescriptor(serviceType: scanResult.ServiceType, implementationType: scanResult.ImplementationType, lifetime: lifetime));

                // register as self
                services.Add(new ServiceDescriptor(serviceType: scanResult.ServiceType, implementationType: scanResult.ImplementationType, lifetime: lifetime));
            }

            return services;
        }

    }
}