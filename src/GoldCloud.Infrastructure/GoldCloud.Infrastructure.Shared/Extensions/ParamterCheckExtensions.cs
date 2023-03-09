using System;
using System.Collections.Generic;
using System.Linq;


namespace GoldCloud.Infrastructure.Shared.Extensions
{
    /// <summary>
    /// 用于参数检查的扩展方法
    /// </summary>
    public static class ParamterCheckExtensions
    {
        /// <summary>
        /// 验证指定值的断言是否为真；如果不为真则抛出指定消息message的指定类型Texception的异常
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="assertion">要验证的断言</param>
        /// <param name="message">异常消息</param>
        private static void Require<TException>(bool assertion, string message) where TException : System.Exception
        {
            if (assertion)
                return;
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");
            //创建指定类型（Texception）的对象实例
            TException exception = (TException)Activator.CreateInstance(typeof(TException), message);
#pragma warning disable CS8597 // 引发的值可为 null。
            throw exception;
#pragma warning restore CS8597 // 引发的值可为 null。
        }

        /// <summary>
        /// 验证指定值的断言表达式是否为真；如果不为真则抛出Exception异常
        /// </summary>
        /// <typeparam name="T">要判断的值的类型</typeparam>
        /// <param name="value"></param>
        /// <param name="assertionFunc">要验证的断言表达式</param>
        /// <param name="message">异常消息</param>
        public static void Required<T>(this T value, Func<T, bool> assertionFunc, string message)
        {
            if (assertionFunc == null)
            {
                throw new ArgumentException("assertionFunc");
            }
            Require<System.Exception>(assertionFunc(value), message);
        }

        /// <summary>
        /// 验证指定值的断言表达式是否为真，如果不为真则抛出异常
        /// </summary>
        /// <typeparam name="T">要判断的值的类型</typeparam>
        /// <typeparam name="TException">抛出的异常类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="assertionFunc">要验证的断言表达式</param>
        /// <param name="message">异常消息</param>
        public static void Required<T, TException>(this T value, Func<T, bool> assertionFunc, string message) where TException : System.Exception
        {
            if (assertionFunc == null)
            {
                throw new ArgumentNullException("assertionFunc");
            }
            Require<TException>(assertionFunc(value), message);
        }

        /// <summary>
        /// 检查参数不能为空引用；否则抛出ArgumentNullException异常
        /// </summary>
        /// <typeparam name="T">要检查的对象类型</typeparam>
        /// <param name="value">要检查的对象值</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNull<T>(this T value, string paramName) where T : class
        {
            Require<ArgumentNullException>(value != null, string.Format("参数“{0}”不能为空引用！", paramName));
        }

        /// <summary>
        /// 检查参数不能为空引用、空字符串、空格；否则抛出ArgumentNullException异常或ArgumentException异常
        /// </summary>
        /// <param name="value">字符串的值</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNullOrEmpty(this string value, string paramName)
        {
            value.CheckNotNull(paramName);
            Require<ArgumentException>(value.Trim().Length > 0, string.Format("参数“{0}”不能为空引用、空字符串、空格！", paramName));
        }

        /// <summary>
        /// 检查Guid的值不能为Guid.Empty；否则抛出ArgumentException异常
        /// </summary>
        /// <param name="value">Guid类型的值</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotEmpty(this Guid value, string paramName)
        {
            Require<ArgumentException>(value != Guid.Empty, string.Format("参数“{0}”的值不能为Guid.Empty ！", paramName));
        }

        /// <summary>
        /// 检查集合不能为空引用或空集合；否则抛出ArgumentNullException异常或ArgumentException异常
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">检查的集合</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNullOrEmpty<T>(this IEnumerable<T> collection, string paramName)
        {
            collection.CheckNotNull(paramName);
            Require<ArgumentException>(collection.Any(), string.Format("参数“{0}”不能为空引用或空集合！", paramName));
        }
    }
}
