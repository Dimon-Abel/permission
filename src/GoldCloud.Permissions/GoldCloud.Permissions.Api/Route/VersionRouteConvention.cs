using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;

namespace GoldCloud.Permissions.Api.Route
{
    #region 全局路由拦截器

    /// <summary>
    /// 全局路由拦截器
    /// </summary>
    public class VersionRouteConvention : IApplicationModelConvention
    {
        #region 私有变量

        /// <summary>
        /// 路由前缀
        /// </summary>
        private readonly AttributeRouteModel centralPrefix;

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="routeTemplateProvider"></param>
        public VersionRouteConvention(IRouteTemplateProvider routeTemplateProvider)
            => centralPrefix = new AttributeRouteModel(routeTemplateProvider);

        #endregion

        #region 遍历应用设置

        /// <summary>
        /// 遍历应用设置
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            //遍历所有的 Controller
            foreach (var controller in application.Controllers)
            {
                // 1、已经标记了 RouteAttribute 的 Controller
                //这一块需要注意，如果在控制器中已经标注有路由了，则会在路由的前面再添加指定的路由内容。
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        // 在当前路由上再添加一个 路由前缀
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(centralPrefix, selectorModel.AttributeRouteModel);
                    }
                }
                //2、没有标记 RouteAttribute 的 Controller
                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        // 添加一个 路由前缀
                        selectorModel.AttributeRouteModel = centralPrefix;
                    }
                }
            }

        }

        #endregion
    }

    #endregion
}
