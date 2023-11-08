using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.DependencyInjection
{
    public class AssemblyScanner : IEnumerable<AssemblyScanner.AssemblyScanResult>
    {
        readonly IEnumerable<Type> _types;
        /// <summary>
        /// 注册类型后缀名
        /// </summary>
        readonly RegisterTypeEnum _registerType;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="registerType"></param>
        public AssemblyScanner(IEnumerable<Type> type, RegisterTypeEnum registerType)
        {
            this._types = type;
            this._registerType = registerType;
        }

        /// <summary>
        /// 查找程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="registerType"></param>
        /// <param name="includeInternalTypes"></param>
        /// <returns></returns>
        public static AssemblyScanner FindAssembly(Assembly assembly,RegisterTypeEnum registerType, bool includeInternalTypes = false)
        {
            return new AssemblyScanner(includeInternalTypes ? assembly.GetTypes() : assembly.GetExportedTypes(), registerType);
        }

        /// <summary>
        /// 查找程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="registerType"></param>
        /// <param name="includeInternalTypes"></param>
        /// <returns></returns>
        public static AssemblyScanner FindAssemblies(IEnumerable<Assembly> assembly, RegisterTypeEnum registerType, bool includeInternalTypes = false)
        {
            var types = assembly.SelectMany(s => includeInternalTypes ? s.GetTypes() : s.GetExportedTypes()?.Distinct());
            return new AssemblyScanner(types, registerType);
        }
        /// <summary>
        /// 查找包含指定类型的程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registerType"></param>
        /// <returns></returns>
        public static AssemblyScanner FindAssemblyContaining<T>(RegisterTypeEnum registerType)
        {
            return FindAssembly(typeof(T).Assembly, registerType);
        }

        /// <summary>
        /// 查找包含指定类型的程序集
        /// </summary>
        /// <param name="type"></param>
        /// <param name="registerType"></param>
        /// <returns></returns>
        public static AssemblyScanner FindAssemblyContaining(Type type, RegisterTypeEnum registerType)
        {
            return FindAssembly(type.Assembly, registerType);
        }

        /// <summary>
        /// 返回符合条件的反射类型
        /// </summary>
        /// <returns></returns>
        public IEnumerator<AssemblyScanResult> GetEnumerator()
        {
            var query = from type in _types
                        where !type.IsAbstract && !type.IsGenericTypeDefinition && type.Name.EndsWith($"{_registerType}")
                        let matchingInterface = type.GetInterfaces().Where(i => i.Name.EndsWith($"{_registerType}")).FirstOrDefault()
                        where matchingInterface != null
                        select new AssemblyScanResult(matchingInterface, type);
            return query.GetEnumerator();
        }

        /// <summary>
        /// 迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 对所有程序集扫描结果执行指定的操作
        /// </summary>
        public void ForEach(Action<AssemblyScanResult> action)
        {
            foreach (var result in this)
            {
                action(result);
            }
        }


        /// <summary>
        /// 执行扫描的结果
        /// </summary>
        public class AssemblyScanResult
        {
            /// <summary>
            /// ctor
            /// </summary>
            /// <param name="serviceType"></param>
            /// <param name="implementationType"></param>
            public AssemblyScanResult(Type serviceType, Type implementationType)
            {
                this.ServiceType = serviceType;
                this.ImplementationType = implementationType;
            }

            /// <summary>
            /// 基类Type
            /// </summary>
            public Type ServiceType { get; set; }

            /// <summary>
            /// 实现类Type
            /// </summary>
            public Type ImplementationType { get; set; }
        }
    }


}
