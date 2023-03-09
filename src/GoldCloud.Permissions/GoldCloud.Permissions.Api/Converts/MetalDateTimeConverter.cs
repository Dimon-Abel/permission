using KingMetal.Infrastructures.ObjectType.Common;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoldCloud.Permissions.Api.Converts
{
    #region MetalDateTime Json序列化转换器

    /// <summary>
    /// MetalDateTime Json序列化转换器
    /// </summary>
    public class MetalDateTimeConverter : JsonConverter<MetalDateTime>
    {
        #region 字符串转时间类型

        /// <summary>
        /// 字符串转时间类型
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override MetalDateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetInt64();

        #endregion

        #region 时间转字符串

        /// <summary>
        /// 时间转字符串
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, MetalDateTime value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value.ToUnixTimeMilliseconds());

        #endregion
    }

    #endregion
}
