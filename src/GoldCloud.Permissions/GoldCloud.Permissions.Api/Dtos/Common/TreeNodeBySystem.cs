using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldCloud.Permissions.Api.Dtos.Common
{
    /// <summary>
    /// 系统权限树节点
    /// </summary>
    public class TreeNodeBySystem
    {
        /// <summary>
        /// 系统标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 权限树
        /// </summary>
        public List<TreeNode> Children { get; set; }
    }
}
