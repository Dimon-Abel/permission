﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoldCloud.Infrastructure.Shared.Cultures.Enumerations.ErrorCode {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Language {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Language() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GoldCloud.Infrastructure.Culture.Enumerations.ErrorCode.Language", typeof(Language).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 ArgumentException 的本地化字符串。
        /// </summary>
        public static string ArgumentException {
            get {
                return ResourceManager.GetString("ArgumentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Forbidden 的本地化字符串。
        /// </summary>
        public static string Forbidden {
            get {
                return ResourceManager.GetString("Forbidden", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Invalid Argument 的本地化字符串。
        /// </summary>
        public static string InvalidArgument {
            get {
                return ResourceManager.GetString("InvalidArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 NoChange 的本地化字符串。
        /// </summary>
        public static string NoChange {
            get {
                return ResourceManager.GetString("NoChange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Object Already Exists 的本地化字符串。
        /// </summary>
        public static string ObjectAlreadyExists {
            get {
                return ResourceManager.GetString("ObjectAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ObjectCannotInitialized 的本地化字符串。
        /// </summary>
        public static string ObjectCannotInitialized {
            get {
                return ResourceManager.GetString("ObjectCannotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Out Of Range 的本地化字符串。
        /// </summary>
        public static string OutOfRange {
            get {
                return ResourceManager.GetString("OutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Resources Not Found 的本地化字符串。
        /// </summary>
        public static string ResourcesNotFound {
            get {
                return ResourceManager.GetString("ResourcesNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ServiceUnavailable 的本地化字符串。
        /// </summary>
        public static string ServiceUnavailable {
            get {
                return ResourceManager.GetString("ServiceUnavailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Success 的本地化字符串。
        /// </summary>
        public static string Success {
            get {
                return ResourceManager.GetString("Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Unauthorized 的本地化字符串。
        /// </summary>
        public static string Unauthorized {
            get {
                return ResourceManager.GetString("Unauthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Unknown 的本地化字符串。
        /// </summary>
        public static string Unknown {
            get {
                return ResourceManager.GetString("Unknown", resourceCulture);
            }
        }
    }
}