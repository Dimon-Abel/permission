using GoldCloud.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;

namespace GoldCloud.Permissions.Api.Extensions
{
    #region ApplicationBuilder扩展方法

    /// <summary>
    /// ApplicationBuilder扩展方法
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        #region 配置多语言

        /// <summary>
        /// 配置多语言
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLanguage(this IApplicationBuilder builder)
        {
            var cultureList = new List<CultureInfo>();
            var options = new RequestLocalizationOptions();

            cultureList.Add(new CultureInfo("zh-hans"));
            cultureList.Add(new CultureInfo("zh-cn"));
            cultureList.Add(new CultureInfo("zh"));

            cultureList.Add(new CultureInfo("zh-hant"));

            cultureList.Add(new CultureInfo("en"));
            cultureList.Add(new CultureInfo("en-us"));

            options.DefaultRequestCulture = new RequestCulture("zh-hans", "zh-hans");

            //新建基于Query String的多语言提供程序
            var queryStringProvider = new QueryStringRequestCultureProvider
            {
                QueryStringKey = "lang",
                UIQueryStringKey = "lang",
            };
            //将提供程序插入到集合的第一个位置，这样优先使用
            options.RequestCultureProviders.Insert(0, queryStringProvider);

            return builder.UseRequestLocalization(options);
        }

        #endregion
    }

    #endregion
}
