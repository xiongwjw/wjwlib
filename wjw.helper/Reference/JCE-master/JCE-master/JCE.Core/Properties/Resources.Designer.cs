﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JCE.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("JCE.Core.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 缓存功能尚未初始化，未找到可用的 ICacheProvider 实现。 的本地化字符串。
        /// </summary>
        internal static string Caching_CacheNotInitialized {
            get {
                return ResourceManager.GetString("Caching_CacheNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 标识为“{0}”的项重复定义 的本地化字符串。
        /// </summary>
        internal static string ConfigFile_ItemKeyDefineRepeated {
            get {
                return ResourceManager.GetString("ConfigFile_ItemKeyDefineRepeated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 名称为“{0}”的类型不存在 的本地化字符串。
        /// </summary>
        internal static string ConfigFile_NameToTypeIsNull {
            get {
                return ResourceManager.GetString("ConfigFile_NameToTypeIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 请先初始化依赖注入服务，再使用OSharpContext.IocRegisterServices属性 的本地化字符串。
        /// </summary>
        internal static string Context_BuildServicesFirst {
            get {
                return ResourceManager.GetString("Context_BuildServicesFirst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 上下文初始化类型“{0}”不存在 的本地化字符串。
        /// </summary>
        internal static string DbContextInitializerConfig_InitializerNotExists {
            get {
                return ResourceManager.GetString("DbContextInitializerConfig_InitializerNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 无法解析类型“{0}”的构造函数中类型为“{1}”的参数 的本地化字符串。
        /// </summary>
        internal static string Ioc_CannotResolveService {
            get {
                return ResourceManager.GetString("Ioc_CannotResolveService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 OSharp框架尚未初始化，请先初始化 的本地化字符串。
        /// </summary>
        internal static string Ioc_FrameworkNotInitialized {
            get {
                return ResourceManager.GetString("Ioc_FrameworkNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”的实现类型无法找到 的本地化字符串。
        /// </summary>
        internal static string Ioc_ImplementationTypeNotFound {
            get {
                return ResourceManager.GetString("Ioc_ImplementationTypeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”中找不到合适参数的构造函数 的本地化字符串。
        /// </summary>
        internal static string Ioc_NoConstructorMatch {
            get {
                return ResourceManager.GetString("Ioc_NoConstructorMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 实现类型不能为“{0}”，因为该类型与注册为“{1}”的其他类型无法区分 的本地化字符串。
        /// </summary>
        internal static string Ioc_TryAddIndistinguishableTypeToEnumerable {
            get {
                return ResourceManager.GetString("Ioc_TryAddIndistinguishableTypeToEnumerable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”不是仓储接口“IRepository&lt;,&gt;”的派生类。 的本地化字符串。
        /// </summary>
        internal static string IocInitializerBase_TypeNotIRepositoryType {
            get {
                return ResourceManager.GetString("IocInitializerBase_TypeNotIRepositoryType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”不是操作单元“IUnitOfWork”的派生类。 的本地化字符串。
        /// </summary>
        internal static string IocInitializerBase_TypeNotIUnitOfWorkType {
            get {
                return ResourceManager.GetString("IocInitializerBase_TypeNotIUnitOfWorkType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 MapperExtensions.Mapper不能为空，请先设置值 的本地化字符串。
        /// </summary>
        internal static string Map_MapperIsNull {
            get {
                return ResourceManager.GetString("Map_MapperIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”不是实体类型 的本地化字符串。
        /// </summary>
        internal static string QueryCacheExtensions_TypeNotEntityType {
            get {
                return ResourceManager.GetString("QueryCacheExtensions_TypeNotEntityType", resourceCulture);
            }
        }
    }
}
