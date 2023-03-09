using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace GoldCloud.Permissions.Api.Extensions
{
    #region 模型状态对象扩展方法

    /// <summary>
    /// 模型状态对象扩展方法
    /// </summary>
    public static class ModelStateExtensions
    {
        #region 获取验证模型的错误消息

        /// <summary>
        /// 获取验证模型的错误消息
        /// </summary>
        /// <param name="state"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this ModelStateDictionary state, string split = "|")
        {
            var errors = state.Where(e => e.Value.Errors.Count > 0)
               .Select(e => $"{e.Value.Errors.FirstOrDefault()?.ErrorMessage}[{e.Key}]").ToArray();

            return string.Join(split, errors);
        }

        #endregion
    }

    #endregion
}
