/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：IFlowOperate.cs
// 文件功能描述：工作流操作接口。
// 创建人：pekah.chow
// 创建时间： 2014-04-28
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using NFMT.Common;

namespace NFMT.WorkFlow
{
    public interface IFlowOperate
    {
        /// <summary>
        /// 提交审核并创建任务，返回任务的id
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="model">提交审核数据实体</param>
        /// <param name="master">模版</param>
        /// <param name="source">数据源</param>
        /// <param name="task">任务</param>
        /// <returns></returns>
        Common.ResultModel AuditAndCreateTask(Common.UserModel user, Common.IModel model, WorkFlow.Model.FlowMaster master, WorkFlow.Model.DataSource source, WorkFlow.Model.Task task);

        /// <summary>
        /// 审核工作流任务
        /// </summary>
        /// <param name="user">当前审核人</param>
        /// <param name="taskNode">任务节点</param>
        /// <param name="log">任务操作记录</param>
        /// <param name="isPass">true表示通过，false表示不通过</param>
        /// <returns></returns>
        ResultModel AuditTaskNode(Common.UserModel user, Model.TaskNode taskNode, Model.TaskOperateLog log,List<NFMT.WorkFlow.Model.TaskAttachOperateLog> taskAttachOperateLogs, bool isPass);

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="nodeLevel">节点级别</param>
        /// <returns></returns>
        ResultModel CreateTaskNodes(Common.UserModel user, Model.Task task, int nodeLevel,Model.DataSource source);

        /// <summary>
        /// 任务同级节点是否全部通过
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="nodeLevel">节点级别</param>
        /// <returns></returns>
        ResultModel IsSameLevelNodeSuccess(Common.UserModel user, Model.Task task, int nodeLevel);
    }
}
