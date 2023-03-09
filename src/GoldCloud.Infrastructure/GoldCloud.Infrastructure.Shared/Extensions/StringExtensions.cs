using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace GoldCloud.Infrastructure.Shared.Extensions
{
    /// <summary>
    /// 字符串<see cref="string"/>类型的扩展辅助操作类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 是否电子邮件
        /// </summary>
        public static bool IsEmail(this string value)
        {
            const string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="isContains">是否包含，否则全匹配</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string value, string pattern, bool isContains = true)
        {
            if (value == null)
            {
                return false;
            }
            return isContains
                ? Regex.IsMatch(value, pattern)
                : Regex.Match(value, pattern).Success;
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isRestrict">是否按严格格式验证</param>
        public static bool IsMobileNumber(this string value, bool isRestrict = false)
        {
            string pattern = isRestrict ? @"^[1][3-8]\d{9}$" : @"^[1]\d{10}$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 是否身份证号，验证如下3种情况：
        /// 1.身份证号码为15位数字；
        /// 2.身份证号码为18位数字；
        /// 3.身份证号码为17位数字+1个字母
        /// </summary>
        public static bool IsIdentityCardId(this string value)
        {
            if (value.Length != 15 && value.Length != 18)
            {
                return false;
            }
            Regex regex;
            string[] array;
            DateTime time;
            if (value.Length == 15)
            {
                regex = new Regex(@"^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})_");
                if (!regex.Match(value).Success)
                {
                    return false;
                }
                array = regex.Split(value);
                return DateTime.TryParse(string.Format("{0}-{1}-{2}", "19" + array[2], array[3], array[4]), out time);
            }
            regex = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9Xx])$");
            if (!regex.Match(value).Success)
            {
                return false;
            }
            array = regex.Split(value);
            if (!DateTime.TryParse(string.Format("{0}-{1}-{2}", array[2], array[3], array[4]), out time))
            {
                return false;
            }
            //校验最后一位
            string[] chars = value.ToCharArray().Select(m => m.ToString()).ToArray();
            int[] weights = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                int num = int.Parse(chars[i]);
                sum = sum + num * weights[i];
            }
            int mod = sum % 11;
            string vCode = "10X98765432";//检验码字符串
            string last = vCode.ToCharArray().ElementAt(mod).ToString();
            return chars.Last().ToUpper() == last;
        }

        /// <summary>
        /// 获取机密数据
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="digits">替换位数</param>
        /// <param name="symbol">替换符号</param>
        /// <returns></returns>
        public static string GetSecretValue(this string source, int digits = 4, char symbol = '*')
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            var count = source.Length > digits ? digits : source.Length;
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(symbol);
            }

            if (source.Length <= digits)
            {
                return sb.ToString();
            }

            var index = (int)Math.Round((source.Length - digits) / 2.0);
            return source.Substring(0, index) + sb.ToString() + source.Substring(index + digits);
        }

        /// <summary>
        /// 为指定格式的字符串填充相应对象来生成字符串
        /// </summary>
        /// <param name="format">字符串格式，占位符以{n}表示</param>
        /// <param name="args">用于填充占位符的参数</param>
        /// <returns>格式化后的字符串</returns>
        [DebuggerStepThrough]
        public static string FormatWith(this string format, params object[] args)
        {
            format.CheckNotNull("format");
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string @json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default;

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            });
        }

        /// <summary>
        /// Base65 编码
        /// </summary>
        /// <param name="plainText">需要加密的明文</param>
        /// <returns></returns>
        public static string ToBase64String(this string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                plainText = string.Empty;

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64 解码
        /// </summary>
        /// <param name="base64String">Base64密文</param>
        /// <returns></returns>
        public static string FormatBase64String(this string base64String)
        {
            if (!IsBase64String(base64String))  return base64String;

            var base64EncodedBytes = System.Convert.FromBase64String(base64String);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// 是否Base64字符串
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static bool IsBase64String(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return false;

            plainText = plainText.Trim();
            return (plainText.Length % 4 == 0) && Regex.IsMatch(plainText, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}