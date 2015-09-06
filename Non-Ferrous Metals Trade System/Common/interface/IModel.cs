/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：IModel.cs
// 文件功能描述：实体接口。
// 创建人：pekah.chow
// 创建时间： 2014-04-23
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public interface IModel
    {
        /// <summary>
        /// 数据序号
        /// </summary>
        int Id { get;}

        /// <summary>
        /// 数据状态
        /// </summary>
        Common.StatusEnum Status { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        string DataBaseName { get; }

        /// <summary>
        /// 表名
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// 创建人序号
        /// </summary>
        int CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人序号
        /// </summary>
        int LastModifyId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime LastModifyTime { get; set; }

        /// <summary>
        /// Dal名称
        /// </summary>
        string DalName { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        string AssName { get; set; }
    }
}
