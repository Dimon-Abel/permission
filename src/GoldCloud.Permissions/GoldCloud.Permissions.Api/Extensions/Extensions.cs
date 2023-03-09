using GoldCloud.Infrastructure.Shared.ValueObjects;
using GoldCloud.Infrastructure.DataBase;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Permissions.Api.Dtos.Common;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace GoldCloud.Permissions.Api.Extensions
{
    /// <summary>
    /// 通用拓展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 转换TreeNode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">元数据</param>
        /// <param name="id">节点标识</param>
        /// <param name="name">节点名称</param>
        /// <param name="pId">上级标识</param>
        /// <param name="type">节点类型</param>
        /// <param name="isSystem">是否为系统级权限</param>
        /// <param name="system">权限归属系统</param>
        /// <param name="remark">节点说明</param>
        /// <returns></returns>
        public static TreeNode ToTreeNode<T>(this T data, long id, string name, long? pId, TreeNodeType type, bool isSystem = false, string remark = null, SystemInfo system = null) where T : class
            => new TreeNode()
            {
                Id = id.ToString(),
                Name = name,
                PId = pId.ToString(),
                IsSystem = isSystem,
                NodeType = type,
                Remark = remark,
                System = system
            };

        /// <summary>
        /// 递归组装PermissionTreeNode
        /// </summary>
        /// <param name="data">基础数据</param>
        /// <param name="pId">上级标识</param>
        /// <returns></returns>
        public static List<PermissionTreeNode> Recursion(this ICollection<PermissionTreeNode> data, long? pId = null)
        {
            var nodes = data.Where(x => x.ParentId == pId).OrderBy(x => x.Order).ToList();
            foreach (var item in nodes)
            {
                if (item.Children == null) item.Children = new List<PermissionTreeNode>();
                item.Children.AddRange(data.Recursion(item.Id));
            }
            return nodes;
        }

        /// <summary>
        /// 递归组装MenuTreeNode
        /// </summary>
        /// <param name="data">基础数据</param>
        /// <param name="pId">上级标识</param>
        /// <returns></returns>
        public static List<MenuTreeNode> Recursion(this ICollection<MenuTreeNode> data, long? pId)
        {
            var nodes = data.Where(x => x.ParentId == pId).ToList();
            foreach (var item in nodes)
            {
                if (item.Children == null) item.Children = new List<MenuTreeNode>();
                item.Children.AddRange(data.Recursion(item.Id));
            }
            return nodes;
        }

        /// <summary>
        /// 递归组装TreeNode列表
        /// </summary>
        /// <param name="data">基础数据</param>
        /// <param name="pId">上级标识</param>
        /// <returns></returns>
        public static List<TreeNode> Recursion(this IQueryable<TreeNode> data, long? pId = null)
        {
            var nodes = data.WhereIf(x => x.PId == pId.ToString(), pId.HasValue).ToList();
            foreach (var item in nodes)
                item.Children.AddRange(data.Recursion(long.Parse(item.Id)));
            return nodes;
        }

        #region 递归组装菜单TreeNode列表 带权限

        /// <summary>
        /// 递归组装菜单TreeNode列表 带权限
        /// </summary>
        /// <param name="data">基础数据</param>
        /// <param name="pId">上级标识</param>
        /// <param name="db"><see cref="GoldPermissionDB" />数据库对象</param>
        /// <returns></returns>
        public static List<TreeNode> Recursion(this IQueryable<TreeNode> data, long? pId, GoldPermissionDB db)
        {
            var nodes = data.WhereIf(x => x.PId == pId.Value.ToString(), pId.HasValue).ToList();
            foreach (var node in nodes)
                node.Children.AddRange(data.Recursion(long.Parse(node.Id), db));

            #region 获取菜单对应权限信息

            var permissions = db.MenuPermissionAssociations.LoadWith(x => x.PermissionEntity)
                .LoadWith(x => x.PermissionEntity.System)
                .Select(x => x.PermissionEntity);

            var permissionIds = permissions.Select(x => x.Id).ToList();
            var permissionCte = db.GetCte<Permission>(mCte =>
            {
                return (
                    from p in db.Permissions.LoadWith(x => x.System)
                    where permissionIds.Contains(p.ParentId.Value)
                    select p
                ).Concat(
                    from p in db.Permissions.LoadWith(x => x.System)
                    from pm in mCte.InnerJoin(pm => p.ParentId == pm.Id)
                    select p
                    );
            });

            var perNodes = (from m in permissionCte
                            select m
.ToTreeNode(m.Id, m.Name, m.ParentId, (TreeNodeType)((int)m.Type), m.IsSystem, m.Remark, new SystemInfo() { Id = m.System.Id, Name = m.System.Name, Remark = m.System.Remark })).ToList();
            foreach (var item in permissions)
            {
                var treeNode = item.ToTreeNode(item.Id, item.Name, item.ParentId, (TreeNodeType)((int)item.Type), item.IsSystem, item.Remark,
                    new SystemInfo() { Id = item.System.Id, Name = item.System.Name, Remark = item.System.Remark });

                treeNode.Children.AddRange(perNodes.AsQueryable().Recursion(item.Id));
                nodes.Add(treeNode);
            }

            #endregion

            return nodes;
        }

        #endregion
    }
}
